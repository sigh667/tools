using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace SocketProtoGenerationTool
{
    class Program
    {
        private static Dictionary<string, DataTypeDefine> dataTypeDefines;
        private static Dictionary<string, TemplateDefine> templateDefines;
        private static Dictionary<string, EnumDefine> enumDefines;
        private static Dictionary<string, CommonDataDefine> commonDataDefines;
        private static Dictionary<string, ProtoDefine> protoDefines;

        private enum Language
        {
            AS3,
            CSharp,
            CPlusPlus,
            Java
        };

        private struct DataTypeDefine
        {
            public string Name;
            public string Desc;
            public string Input;
            public string Output;
            public string Bit;
            public string AS3;
            public string CSharp;
            public string CPlusPlus;
            public string Java;
            public string Type;
        }

        private enum TempateType
        {
            Enum,
            Struct,
            Proto
        }

        private struct TemplateDefine
        {
            public string name;
            public Language language;
            public string desc;
            public TempateType type;
            public string content;
        }

        private struct EnumDefine
        {
            public string name;
            public string desc;
            public List<string> itemNames;
            public List<string> itemDescs;
        }

        private struct CommonDataDefine
        {
            public string name;
            public string desc;
            public List<string> itemNames;
            public List<string> itemTypes;
            public List<string> itemElementTypes;
            public List<string> itemDescs;
            public List<bool> itemIsExtends;
        }

        private struct ProtoDefine
        {
            public string name;
            public string desc;
            public List<Connections> froms;
            public List<Connections> dests;
            public List<string> itemNames;
            public List<string> itemTypes;
            public List<string> itemElementTypes;
            public List<string> ItemImportPathAS3;
            public List<string> ItemImportPathCSharp;
            public List<string> ItemImportPathCPlusPlus;
            public List<string> ItemImportPathJava;
            public List<string> itemDescs;
            public List<bool> itemIsExtends;
        }

        private enum Connections
        {
            Login,
            Agent,
            Alert,
            Console,
            Bill,
            DB,
            FlashSecurityProxy,
            GM,
            Hall,
            Logger,
            Room,
            Route,
            Client
        }

        static DirectoryInfo exchangeDir;

        private static void run(ref string[] args)
        {
            string protoFilePath;
            if (args.Length > 1)
            {
                protoFilePath = args[0];
            }
            else
            {
                protoFilePath = Directory.GetCurrentDirectory() + "\\Protocol.xml";
            }

            if (File.Exists(protoFilePath))
            {
                exchangeDir = new DirectoryInfo(protoFilePath.Replace("Protocol.xml", "Exchange"));
                if (exchangeDir.Exists)
                {
                    exchangeDir.Delete(true);
                }
                
                exchangeDir.Create();

                Directory.CreateDirectory(exchangeDir.FullName + "\\AS3\\commonData");
                Directory.CreateDirectory(exchangeDir.FullName + "\\CPlusPlus\\commonData");
                //Directory.CreateDirectory(exchangeDir.FullName + "\\CSharp\\CommonData");
                Directory.CreateDirectory(exchangeDir.FullName + "\\Java\\commonData");

                Directory.CreateDirectory(exchangeDir.FullName + "\\AS3\\proto");
                Directory.CreateDirectory(exchangeDir.FullName + "\\CPlusPlus\\proto");
                //Directory.CreateDirectory(exchangeDir.FullName + "\\CSharp\\Proto");
                Directory.CreateDirectory(exchangeDir.FullName + "\\Java\\proto");

                Directory.CreateDirectory(exchangeDir.FullName + "\\Java\\enumeration");
                Directory.CreateDirectory(exchangeDir.FullName + "\\AS3\\enum");

                exchangeXML(protoFilePath);

                generationEnumDefines(Language.AS3);
                //generationEnumDefines(Language.CSharp);
                generationEnumDefines(Language.CPlusPlus);
                generationEnumDefines(Language.Java);

                generationCommonDataDefines(Language.AS3);
                generationCommonDataDefines(Language.Java);

                generationProto(Language.AS3);
                //generationProto(Language.CSharp);
                generationProto(Language.CPlusPlus);
                generationProto(Language.Java);
            }
            else
            {
                Console.WriteLine("Protocol File Not Exsit");
            }
        }

        static void Main(string[] args)
        {
            try
            {
                run(ref args);

            }
            catch (System.Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                Console.WriteLine("\nBuild failed! \nPress any key to continue...");
                Console.ReadKey();
                return;
            }
            Console.WriteLine("\nBuild successful! \nPress any key to continue...");
            Console.ReadKey();
        }

        static void exchangeXML(string protoFilePath)
        {
            exchangeDataType(protoFilePath);
            exchangeTemplate(protoFilePath);
            exchangeEnum(protoFilePath);
            exchangeCommonData(protoFilePath);
            exchangeProto(protoFilePath);
        }

        static void exchangeTemplate(string protoFilePath)
        {
            XmlReader xmlReader = XmlReader.Create(@protoFilePath);
            templateDefines = new Dictionary<string, TemplateDefine>();
            if (xmlReader.ReadToFollowing("Template"))
            {
                xmlReader.Read();
                while (xmlReader.Name == "TemplateDefine" || xmlReader.Name == "")
                {
                    if (xmlReader.Name != "")
                    {
                        TemplateDefine templateDefine = new TemplateDefine();
                        string itemName = xmlReader.GetAttribute("Name");
                        string itemDesc = xmlReader.GetAttribute("desc");
                        string itemLanguage = xmlReader.GetAttribute("language");
                        string itemType = xmlReader.GetAttribute("type");
                        string itemContent = xmlReader.ReadInnerXml().Replace("<![CDATA[", "").Replace("]]>", "");
                        templateDefine.name = itemName;
                        templateDefine.desc = itemDesc;
                        switch (itemLanguage)
                        {
                            case "AS3":
                                {
                                    templateDefine.language = Language.AS3;
                                    break;
                                }
                            case "CSharp":
                                {
                                    templateDefine.language = Language.CSharp;
                                    break;
                                }
                            case "CPlusPlus":
                                {
                                    templateDefine.language = Language.CPlusPlus;
                                    break;
                                }
                            case "Java":
                                {
                                    templateDefine.language = Language.Java;
                                    break;
                                }
                        }
                        switch (itemType)
                        {
                            case "Enum":
                                {
                                    templateDefine.type = TempateType.Enum;
                                    break;
                                }
                            case "Struct":
                                {
                                    templateDefine.type = TempateType.Struct;
                                    break;
                                }
                            case "Proto":
                                {
                                    templateDefine.type = TempateType.Proto;
                                    break;
                                }
                        }
                        templateDefine.content = itemContent;
                        templateDefines.Add(templateDefine.name, templateDefine);
                    }
                    xmlReader.Read();
                }
            }
        }

        static void exchangeDataType(string protoFilePath)
        {
            XmlReader xmlReader = XmlReader.Create(@protoFilePath);
            dataTypeDefines = new Dictionary<string, DataTypeDefine>();
            if (xmlReader.ReadToFollowing("TypeDefine"))
            {
                xmlReader.Read();
                while (xmlReader.Name == "Type" || xmlReader.Name == "")
                {
                    if (xmlReader.Name != "")
                    {
                        string itemName = xmlReader.GetAttribute("name");
                        string itemDesc = xmlReader.GetAttribute("desc");
                        string itemInput = xmlReader.GetAttribute("input");
                        string itemOutput = xmlReader.GetAttribute("output");
                        string itemBit = xmlReader.GetAttribute("bit");
                        string itemAS3 = xmlReader.GetAttribute("AS3");
                        string itemCSharp = xmlReader.GetAttribute("CSharp");
                        string itemCPlusPlus = xmlReader.GetAttribute("CPlusPlus");
                        string itemJava = xmlReader.GetAttribute("Java");
                        string itemType = xmlReader.GetAttribute("Type");
                        DataTypeDefine typeDefine = new DataTypeDefine();
                        typeDefine.Name = itemName;
                        typeDefine.Desc = itemDesc;
                        typeDefine.Input = itemInput;
                        typeDefine.Output = itemOutput;
                        typeDefine.Bit = itemBit;
                        typeDefine.AS3 = itemAS3;
                        typeDefine.CSharp = itemCSharp;
                        typeDefine.CPlusPlus = itemCPlusPlus;
                        typeDefine.Java = itemJava;
                        typeDefine.Type = itemType;
                        dataTypeDefines.Add(typeDefine.Name, typeDefine);
                    }
                    xmlReader.Read();
                }
            }
        }

        static void exchangeEnum(string protoFilePath)
        {
            XmlReader xmlReader = XmlReader.Create(@protoFilePath);
            enumDefines = new Dictionary<string, EnumDefine>();
            if (xmlReader.ReadToFollowing("EnumDefine"))
            {
                while (xmlReader.ReadToFollowing("Enum"))
                {
                    EnumDefine enumDefine = new EnumDefine();
                    string name = xmlReader.GetAttribute("name");
                    string desc = xmlReader.GetAttribute("desc");
                    enumDefine.name = name;
                    enumDefine.desc = desc;
                    enumDefine.itemNames = new List<string>();
                    enumDefine.itemDescs = new List<string>();
                    xmlReader.Read();
                    while (xmlReader.Name == "EnumMember" || xmlReader.Name == "")
                    {
                        if (xmlReader.Name != "")
                        {
                            string itemName = xmlReader.GetAttribute("name");
                            string itemDesc = xmlReader.GetAttribute("desc");
                            enumDefine.itemNames.Add(itemName);
                            enumDefine.itemDescs.Add(itemDesc);
                        }
                        xmlReader.Read();
                    }
                    enumDefines[name] = enumDefine;
                }
            }
        }

        static void exchangeCommonData(string protoFilePath)
        {
            XmlReader xmlReader = XmlReader.Create(@protoFilePath);
            commonDataDefines = new Dictionary<string, CommonDataDefine>();
            if (xmlReader.ReadToFollowing("CommonDataDefine"))
            {
                while (xmlReader.ReadToFollowing("CommonData"))
                {
                    CommonDataDefine commonDataDefine = new CommonDataDefine();
                    commonDataDefine.name = xmlReader.GetAttribute("name");
                    commonDataDefine.desc = xmlReader.GetAttribute("desc");
                    commonDataDefine.itemNames = new List<string>();
                    commonDataDefine.itemTypes = new List<string>();
                    commonDataDefine.itemElementTypes = new List<string>();
                    commonDataDefine.itemIsExtends = new List<bool>();
                    commonDataDefine.itemDescs = new List<string>();
                    xmlReader.Read();
                    while (xmlReader.Name == "CommonDataMember" || xmlReader.Name == "")
                    {
                        if (xmlReader.Name != "")
                        {
                            string itemName = xmlReader.GetAttribute("name");
                            string itemType = xmlReader.GetAttribute("type");
                            string elementType = xmlReader.GetAttribute("elementType");
                            string isExtends = xmlReader.GetAttribute("extends");
                            string itemDesc = xmlReader.GetAttribute("desc");
                            commonDataDefine.itemNames.Add(itemName);
                            commonDataDefine.itemTypes.Add(itemType);
                            commonDataDefine.itemElementTypes.Add(string.IsNullOrEmpty(elementType) ? string.Empty : elementType);
                            commonDataDefine.itemDescs.Add(itemDesc);
                            commonDataDefine.itemIsExtends.Add(isExtends == "true" ? true : false);
                        }
                        xmlReader.Read();
                    }
                    commonDataDefines[commonDataDefine.name] = commonDataDefine;
                }
            }
        }

        static void exchangeProto(string protoFilePath)
        {
            XmlReader xmlReader = XmlReader.Create(@protoFilePath);
            protoDefines = new Dictionary<string, ProtoDefine>();
            if (xmlReader.ReadToFollowing("ProtoDefine"))
            {
                while (xmlReader.ReadToFollowing("Proto"))
                {
                    ProtoDefine protoDefine = new ProtoDefine();
                    string name = xmlReader.GetAttribute("name");
                    string froms = xmlReader.GetAttribute("from");
                    string dests = xmlReader.GetAttribute("dest");
                    string desc = xmlReader.GetAttribute("desc");
                    protoDefine.name = name;
                    string[] tempFroms = froms.Split('|');
                    protoDefine.froms = new List<Connections>();
                    foreach (string tempFrom in tempFroms)
                    {
                        protoDefine.froms.Add((Connections)Enum.Parse(typeof(Connections), tempFrom));
                    }
                    string[] tempdests = dests.Split('|');
                    protoDefine.dests = new List<Connections>();
                    foreach (string tempDest in tempdests)
                    {
                        protoDefine.dests.Add((Connections)Enum.Parse(typeof(Connections), tempDest));
                    }
                    protoDefine.desc = desc;
                    protoDefine.itemNames = new List<string>();
                    protoDefine.itemTypes = new List<string>();
                    protoDefine.itemElementTypes = new List<string>();
                    protoDefine.ItemImportPathAS3 = new List<string>();
                    protoDefine.ItemImportPathCSharp = new List<string>();
                    protoDefine.ItemImportPathCPlusPlus = new List<string>();
                    protoDefine.ItemImportPathJava = new List<string>();
                    protoDefine.itemIsExtends = new List<bool>();
                    protoDefine.itemDescs = new List<string>();
                    xmlReader.Read();
                    while (xmlReader.Name == "ProtoMember" || xmlReader.Name == "")
                    {
                        if (xmlReader.Name != "")
                        {
                            string itemName = xmlReader.GetAttribute("name");
                            string itemType = xmlReader.GetAttribute("type");
                            string importPathAS3 = xmlReader.GetAttribute("importPathAS3");
                            string importPathCSharp = xmlReader.GetAttribute("importPathCSharp");
                            string importPathCPlusPlus = xmlReader.GetAttribute("importPathCPlusPlus");
                            string importPathJava = xmlReader.GetAttribute("importPathJava");
                            string elementType = xmlReader.GetAttribute("elementType");
                            string isExtends = xmlReader.GetAttribute("extends");
                            string itemDesc = xmlReader.GetAttribute("desc");
                            protoDefine.itemNames.Add(itemName);
                            protoDefine.itemTypes.Add(itemType);
                            protoDefine.itemElementTypes.Add(string.IsNullOrEmpty(elementType) ? string.Empty : elementType);
                            protoDefine.ItemImportPathAS3.Add(string.IsNullOrEmpty(importPathAS3) ? string.Empty : importPathAS3);
                            protoDefine.ItemImportPathCSharp.Add(string.IsNullOrEmpty(importPathCSharp) ? string.Empty : importPathCSharp);
                            protoDefine.ItemImportPathCPlusPlus.Add(string.IsNullOrEmpty(importPathCPlusPlus) ? string.Empty : importPathCPlusPlus);
                            protoDefine.ItemImportPathJava.Add(string.IsNullOrEmpty(importPathJava) ? string.Empty : importPathJava);
                            protoDefine.itemDescs.Add(itemDesc);
                            protoDefine.itemIsExtends.Add(isExtends == "true" ? true : false);
                        }
                        xmlReader.Read();
                    }
                    protoDefines[name] = protoDefine;
                }
            }
        }

        static void generationEnumDefines(Language language)
        {
            string cppDefineTotal = "";
            foreach (EnumDefine enumDefine in enumDefines.Values)
            {
                switch (language)
                {
                    case Language.AS3:
                        {
                            string as3EnumClassDefine = templateDefines["EnumAS3"].content;
                            string as3EnumItemsDefine = "";
                            for (int i = 0; i < enumDefine.itemNames.Count; i++)
                            {
                                as3EnumItemsDefine += templateDefines["EnumPropertyDefineAS3"].content.Replace("{@EnumItemDesc}", enumDefine.itemDescs[i]).Replace("{@EnumItemName}", enumDefine.itemNames[i]).Replace("{@EnumID}", i.ToString());
                            }
                            as3EnumClassDefine = as3EnumClassDefine.Replace("{@EnumName}", enumDefine.name);
                            as3EnumClassDefine = as3EnumClassDefine.Replace("{@EnumDesc}", enumDefine.desc);
                            as3EnumClassDefine = as3EnumClassDefine.Replace("{@EnumItems}", as3EnumItemsDefine);
                            File.WriteAllText(exchangeDir.FullName + "\\AS3\\enum\\" + enumDefine.name + ".as", as3EnumClassDefine);
                            break;
                        }
                    case Language.CSharp:
                        {
                            string csharpEnumDefine = templateDefines["EnumCSharp"].content;
                            string csharpEnumItemsDefine = "";
                            for (int i = 0; i < enumDefine.itemNames.Count; i++)
                            {
                                string enumItemDefine = templateDefines["EnumPropertyDefineCSharp"].content.Replace("{@EnumItemDesc}", enumDefine.itemDescs[i]).Replace("{@EnumItemName}", enumDefine.itemNames[i]).Replace("{@EnumID}", i.ToString());
                                csharpEnumItemsDefine += enumItemDefine;
                            }
                            csharpEnumItemsDefine = csharpEnumItemsDefine.Remove(csharpEnumItemsDefine.Length - 5);
                            csharpEnumItemsDefine += "\r\n";
                            csharpEnumDefine = csharpEnumDefine.Replace("{@EnumName}", enumDefine.name);
                            csharpEnumDefine = csharpEnumDefine.Replace("{@EnumDesc}", enumDefine.desc);
                            csharpEnumDefine = csharpEnumDefine.Replace("{@EnumItems}", csharpEnumItemsDefine);
                            File.WriteAllText(exchangeDir.FullName + "\\CSharp\\CommonData\\" + enumDefine.name + ".cs", csharpEnumDefine);
                            break;
                        }
                    case Language.CPlusPlus:
                        {
                            string cplusplusEnumDefine = templateDefines["EnumCPlusPlus"].content;
                            string cplusplusEnumItemsDefine = "";
                            for (int i = 0; i < enumDefine.itemNames.Count; i++)
                            {
                                string enumItemDefine = templateDefines["EnumPropertyDefineCPlusPlus"].content.Replace("{@EnumItemDesc}", enumDefine.itemDescs[i]).Replace("{@EnumItemName}", enumDefine.itemNames[i]).Replace("{@EnumID}", i.ToString());
                                cplusplusEnumItemsDefine += enumItemDefine;
                            }
                            cplusplusEnumItemsDefine = cplusplusEnumItemsDefine.Remove(cplusplusEnumItemsDefine.Length - 5);
                            cplusplusEnumItemsDefine += "\r\n";
                            cplusplusEnumDefine = cplusplusEnumDefine.Replace("{@EnumName}", enumDefine.name);
                            cplusplusEnumDefine = cplusplusEnumDefine.Replace("{@EnumDesc}", enumDefine.desc);
                            cplusplusEnumDefine = cplusplusEnumDefine.Replace("{@EnumItems}", cplusplusEnumItemsDefine);
                            cppDefineTotal += cplusplusEnumDefine;
                            break;
                        }
                    case Language.Java:
                        {
                            string tempEnumDefine = templateDefines["EnumJava"].content;
                            string tempEnumItemsDefine = "";
                            for (int i = 0; i < enumDefine.itemNames.Count; i++)
                            {
                                string enumItemDefine = templateDefines["EnumPropertyDefineJava"].content.Replace("{@EnumItemDesc}", enumDefine.itemDescs[i]).Replace("{@EnumItemName}", enumDefine.itemNames[i]).Replace("{@EnumID}", i.ToString());
                                tempEnumItemsDefine += enumItemDefine;
                            }
                            tempEnumItemsDefine = tempEnumItemsDefine.Remove(tempEnumItemsDefine.Length - 5);
                            tempEnumItemsDefine += "\r\n";
                            tempEnumDefine = tempEnumDefine.Replace("{@EnumName}", enumDefine.name);
                            tempEnumDefine = tempEnumDefine.Replace("{@EnumDesc}", enumDefine.desc);
                            tempEnumDefine = tempEnumDefine.Replace("{@EnumItems}", tempEnumItemsDefine);
                            File.WriteAllText(exchangeDir.FullName + "\\Java\\enumeration\\" + enumDefine.name + ".java", tempEnumDefine);
                            break;
                        }
                }

            }
            if (Language.CPlusPlus == language)
            {
                cppDefineTotal = "#ifndef __PROTOCOL_ENUMS__\n#define __PROTOCOL_ENUMS__\n"
                    + cppDefineTotal
                    + "\n#endif\n";
                File.WriteAllText(exchangeDir.FullName + "\\CPlusPlus\\CommonData\\" + "ProtocolEnum.h", cppDefineTotal);

            }
        }

        // 生成通用结构体
        static void generationCommonDataDefines(Language language) {
            string csharpProtoSeriailizationDefine = templateDefines["ProtoSerializationDefineCSharp"].content;
            string cplusplusProtoSeriailizationDefine = templateDefines["ProtoSerializationDefineCPlusPlus"].content;
            string javaProtoSeriailizationDefine = templateDefines["ProtoSerializationDefineJava"].content;
            string cplusplusProtoSeriailizationDefineTotal = "";
            string addProtoSerializationIDDefineToMapCSharp = "";
            string addProtoSerializationIDDefineToMapCPlusPlus = "";
            string addProtoSerializationIDDefineToMapJava = "";

            foreach (CommonDataDefine commonDataDefine in commonDataDefines.Values)
            {
                switch (language)
                {
                    case Language.AS3:
                        {
                            string as3CommonDataDefine = templateDefines["CommonDataAS3"].content;
                            string as3CommonDataItemsDefine = "";
                            string as3CommonDataItemsGetSet = "";
                            string toBytes = "";
                            string fromBytes = "";
                            for (int i = 0; i < commonDataDefine.itemNames.Count; i++)
                            {
                                string typeString = dataTypeDefines.ContainsKey(commonDataDefine.itemTypes[i]) ? dataTypeDefines[commonDataDefine.itemTypes[i]].AS3 : commonDataDefine.itemTypes[i];
                                string typeElementString = commonDataDefine.itemElementTypes[i];
                                typeString = typeString.Replace("{@ObjectType}", typeElementString);
                                as3CommonDataItemsDefine += templateDefines["ObjectDefineAS3"].content.Replace("{@ObjectName}", commonDataDefine.itemNames[i]).Replace("{@ObjectType}", typeString).Replace("{@ObjectDesc}", commonDataDefine.itemDescs[i]);
                                as3CommonDataItemsGetSet += templateDefines["ObjectGetSetDefineAS3"].content.Replace("{@ObjectName}", commonDataDefine.itemNames[i]).Replace("{@ObjectType}", typeString).Replace("{@index}", "0x" + (i + 1).ToString("X"));

                                if (commonDataDefine.itemTypes[i] == "VectorCustomObject")
                                {
                                    toBytes += templateDefines["CustomVectorTypeSerializationDefineAS3"].content.Replace("{@ObjectName}", commonDataDefine.itemNames[i]).Replace("{@Output}", dataTypeDefines[commonDataDefine.itemTypes[i]].Output).Replace("{@ObjectType}", typeString).Replace("{@ObjectElementType}", typeElementString).Replace("{@index}", "0x" + (i + 1).ToString("X"));
                                    fromBytes += templateDefines["CustomVectorTypeDeserializationDefineAS3"].content.Replace("{@ObjectName}", commonDataDefine.itemNames[i]).Replace("{@Input}", dataTypeDefines[commonDataDefine.itemTypes[i]].Input).Replace("{@ObjectType}", typeString).Replace("{@ObjectElementType}", typeElementString).Replace("{@index}", "0x" + (i + 1).ToString("X"));
                                }
                                else if (enumDefines.ContainsKey(commonDataDefine.itemTypes[i]))
                                {
                                    toBytes += templateDefines["EnumSerializationDefineAS3"].content.Replace("{@ObjectName}", commonDataDefine.itemNames[i]).Replace("{@index}", "0x" + (i + 1).ToString("X"));
                                    fromBytes += templateDefines["EnumDeserializationDefineAS3"].content.Replace("{@ObjectName}", commonDataDefine.itemNames[i]).Replace("{@ObjectType}", typeString).Replace("{@index}", "0x" + (i + 1).ToString("X"));
                                }
                                else if (dataTypeDefines.ContainsKey(commonDataDefine.itemTypes[i]))
                                {
                                    toBytes += templateDefines["BaseTypeSerializationDefineAS3"].content.Replace("{@ObjectName}", commonDataDefine.itemNames[i]).Replace("{@Output}", dataTypeDefines[commonDataDefine.itemTypes[i]].Output).Replace("{@ObjectType}", typeString).Replace("{@index}", "0x" + (i + 1).ToString("X"));
                                    fromBytes += templateDefines["BaseTypeDeserializationDefineAS3"].content.Replace("{@ObjectName}", commonDataDefine.itemNames[i]).Replace("{@Input}", dataTypeDefines[commonDataDefine.itemTypes[i]].Input).Replace("{@ObjectType}", typeString).Replace("{@index}", "0x" + (i + 1).ToString("X"));
                                }
                                else
                                {
                                    if (commonDataDefine.itemIsExtends[i])
                                    {
                                        toBytes += templateDefines["CustomSerializationExtendDefineAS3"].content.Replace("{@ObjectName}", commonDataDefine.itemNames[i]).Replace("{@ObjectType}", typeString).Replace("{@index}", "0x" + (i + 1).ToString("X"));
                                        fromBytes += templateDefines["CustomDeserializationExtendDefineAS3"].content.Replace("{@ObjectName}", commonDataDefine.itemNames[i]).Replace("{@ObjectType}", typeString).Replace("{@index}", "0x" + (i + 1).ToString("X"));
                                    }
                                    else
                                    {
                                        toBytes += templateDefines["CustomSerializationDefineAS3"].content.Replace("{@ObjectName}", commonDataDefine.itemNames[i]).Replace("{@ObjectType}", typeString).Replace("{@index}", "0x" + (i + 1).ToString("X"));
                                        fromBytes += templateDefines["CustomDeserializationDefineAS3"].content.Replace("{@ObjectName}", commonDataDefine.itemNames[i]).Replace("{@ObjectType}", typeString).Replace("{@index}", "0x" + (i + 1).ToString("X"));
                                    }
                                }
                            }
                            as3CommonDataDefine = as3CommonDataDefine.Replace("{@commonDataName}", commonDataDefine.name);
                            as3CommonDataDefine = as3CommonDataDefine.Replace("{@commonDataDesc}", commonDataDefine.desc);
                            as3CommonDataDefine = as3CommonDataDefine.Replace("{@ProtoPropertyItems}", as3CommonDataItemsDefine);
                            as3CommonDataDefine = as3CommonDataDefine.Replace("{@ProtoPropertyItemsGetSet}", as3CommonDataItemsGetSet);
                            as3CommonDataDefine = as3CommonDataDefine.Replace("{@ToBytes}", toBytes);
                            as3CommonDataDefine = as3CommonDataDefine.Replace("{@FromBytes}", fromBytes);
                            File.WriteAllText(exchangeDir.FullName + "\\AS3\\commonData\\" + commonDataDefine.name + ".as", as3CommonDataDefine);
                            break;
                        }
                    case Language.Java:
                        {
                            string javaCommonDataDefine = templateDefines["CommonDataJava"].content;
                            string javaCommonDataItemsDefine = "";
                            string javaCommonDataItemsGetSet = "";
                            string toBytes = "";
                            string fromBytes = "";
                            for (int i = 0; i < commonDataDefine.itemNames.Count; i++)
                            {
                                string typeString = dataTypeDefines.ContainsKey(commonDataDefine.itemTypes[i]) ? dataTypeDefines[commonDataDefine.itemTypes[i]].Java : commonDataDefine.itemTypes[i];
                                string typeElementString = commonDataDefine.itemElementTypes[i];
                                typeString = typeString.Replace("{@ObjectType}", typeElementString);
                                javaCommonDataItemsDefine += templateDefines["ObjectDefineJava"].content.Replace("{@ObjectName}", commonDataDefine.itemNames[i]).Replace("{@ObjectType}", typeString).Replace("{@ObjectDesc}", commonDataDefine.itemDescs[i]);
                                javaCommonDataItemsGetSet += templateDefines["ObjectGetSetDefineJava"].content.Replace("{@ObjectName}", commonDataDefine.itemNames[i]).Replace("{@ObjectType}", typeString).Replace("{@index}", "0x" + (i + 1).ToString("X"));

                                if (commonDataDefine.itemTypes[i] == "VectorCustomObject")
                                {
                                    toBytes += templateDefines["CustomVectorTypeSerializationDefineJava"].content.Replace("{@ObjectName}", commonDataDefine.itemNames[i]).Replace("{@Output}", dataTypeDefines[commonDataDefine.itemTypes[i]].Output).Replace("{@ObjectType}", typeString).Replace("{@ObjectElementType}", typeElementString).Replace("{@index}", "0x" + (i + 1).ToString("X"));
                                    fromBytes += templateDefines["CustomVectorTypeDeserializationDefineJava"].content.Replace("{@ObjectName}", commonDataDefine.itemNames[i]).Replace("{@Input}", dataTypeDefines[commonDataDefine.itemTypes[i]].Input).Replace("{@ObjectType}", typeString).Replace("{@ObjectElementType}", typeElementString).Replace("{@index}", "0x" + (i + 1).ToString("X"));
                                }
                                else if (enumDefines.ContainsKey(commonDataDefine.itemTypes[i]))
                                {
                                    toBytes += templateDefines["EnumSerializationDefineJava"].content.Replace("{@ObjectName}", commonDataDefine.itemNames[i]).Replace("{@index}", "0x" + (i + 1).ToString("X"));
                                    fromBytes += templateDefines["EnumDeserializationDefineJava"].content.Replace("{@ObjectName}", commonDataDefine.itemNames[i]).Replace("{@ObjectType}", typeString).Replace("{@index}", "0x" + (i + 1).ToString("X"));
                                }
                                else if (dataTypeDefines.ContainsKey(commonDataDefine.itemTypes[i]))
                                {
                                    toBytes += templateDefines["BaseTypeSerializationDefineJava"].content.Replace("{@ObjectName}", commonDataDefine.itemNames[i]).Replace("{@Output}", dataTypeDefines[commonDataDefine.itemTypes[i]].Output).Replace("{@ObjectType}", typeString).Replace("{@index}", "0x" + (i + 1).ToString("X"));
                                    fromBytes += templateDefines["BaseTypeDeserializationDefineJava"].content.Replace("{@ObjectName}", commonDataDefine.itemNames[i]).Replace("{@Input}", dataTypeDefines[commonDataDefine.itemTypes[i]].Input).Replace("{@ObjectType}", typeString).Replace("{@index}", "0x" + (i + 1).ToString("X"));
                                }
                                else
                                {
                                    if (commonDataDefine.itemIsExtends[i])
                                    {
                                        toBytes += templateDefines["CustomSerializationExtendDefineJava"].content.Replace("{@ObjectName}", commonDataDefine.itemNames[i]).Replace("{@ObjectType}", typeString).Replace("{@index}", "0x" + (i + 1).ToString("X"));
                                        fromBytes += templateDefines["CustomDeserializationExtendDefineJava"].content.Replace("{@ObjectName}", commonDataDefine.itemNames[i]).Replace("{@ObjectType}", typeString).Replace("{@index}", "0x" + (i + 1).ToString("X"));
                                    }
                                    else
                                    {
                                        toBytes += templateDefines["CustomSerializationDefineJava"].content.Replace("{@ObjectName}", commonDataDefine.itemNames[i]).Replace("{@ObjectType}", typeString).Replace("{@index}", "0x" + (i + 1).ToString("X"));
                                        fromBytes += templateDefines["CustomDeserializationDefineJava"].content.Replace("{@ObjectName}", commonDataDefine.itemNames[i]).Replace("{@ObjectType}", typeString).Replace("{@index}", "0x" + (i + 1).ToString("X"));
                                    }
                                }
                            }
                            javaCommonDataDefine = javaCommonDataDefine.Replace("{@commonDataName}", commonDataDefine.name);
                            javaCommonDataDefine = javaCommonDataDefine.Replace("{@commonDataDesc}", commonDataDefine.desc);
                            javaCommonDataDefine = javaCommonDataDefine.Replace("{@ProtoPropertyItems}", javaCommonDataItemsDefine);
                            javaCommonDataDefine = javaCommonDataDefine.Replace("{@ProtoPropertyItemsGetSet}", javaCommonDataItemsGetSet);
                            javaCommonDataDefine = javaCommonDataDefine.Replace("{@ToBytes}", toBytes);
                            javaCommonDataDefine = javaCommonDataDefine.Replace("{@FromBytes}", fromBytes);
                            javaCommonDataDefine = javaCommonDataDefine.Replace("{@ItemsCount}", "" + commonDataDefine.itemNames.Count);
                            
                            File.WriteAllText(exchangeDir.FullName + "\\Java\\commonData\\" + commonDataDefine.name + ".java", javaCommonDataDefine);
                            break;
                        }
                }

            }
        }

        // 生成消息类
        static void generationProto(Language language)
        {
            string csharpProtoSeriailizationDefine = templateDefines["ProtoSerializationDefineCSharp"].content;
            string as3ProtoSeriailizationDefine = templateDefines["ProtoSerializationDefineAS3"].content;
            string cplusplusProtoSeriailizationDefine = templateDefines["ProtoSerializationDefineCPlusPlus"].content;
            string javaProtoSeriailizationDefine = templateDefines["ProtoSerializationDefineJava"].content;
            string cplusplusProtoSeriailizationDefineTotal = "";
            string protoSerializationIDDefineCSharp = "";
            string protoSerializationIDDefineTemp = "";
            string addProtoSerializationIDDefineToMapCSharp = "";
            string addProtoSerializationIDDefineToMapTemp = "";
            string protoSerializationIDDefineAS3 = "";
            string addProtoSerializationIDDefineToMapAS3 = "";
            string protoSerializationIDDefineCPlusPlus = "";
            string addProtoSerializationIDDefineToMapCPlusPlus = "";
            string protoSerializationIDDefineJava = "";
            string addProtoSerializationIDDefineToMapJava = "";

            int seriailizationID = 10; //1-10消息号暂空
            foreach (ProtoDefine protoDefine in protoDefines.Values)
            {
                seriailizationID++;
                switch (language)
                {
                    case Language.AS3:
                        {
                            if (!(protoDefine.froms.Contains(Connections.Client) || protoDefine.dests.Contains(Connections.Client)))
                            {
                                continue;
                            }
                            string as3ProtoClassDefine = templateDefines["ProtoAS3"].content;
                            string structPropertyItemsString = "";
                            string structPropertyItemsGetSet = "";
                            string toBytes = "";
                            string fromBytes = "";
                            string import = "";
                            protoSerializationIDDefineAS3 += templateDefines["ProtoSerializationIDDefineAS3"].content.Replace("{@ProtoName_ToUpper}", protoDefine.name.ToUpper()).Replace("{@ProtoID}", seriailizationID.ToString());
                            addProtoSerializationIDDefineToMapAS3 += templateDefines["AddProtoSerializationIDDefineToMapAS3"].content.Replace("{@ProtoName_ToUpper}", protoDefine.name.ToUpper()).Replace("{@ProtoName}", protoDefine.name);
                            for (int i = 0; i < protoDefine.itemNames.Count; i++)
                            {
                                string typeString = dataTypeDefines.ContainsKey(protoDefine.itemTypes[i]) ? dataTypeDefines[protoDefine.itemTypes[i]].AS3 : protoDefine.itemTypes[i];
                                string typeImportPath = protoDefine.ItemImportPathAS3[i];
                                string typeElementString = protoDefine.itemElementTypes[i];
                                typeString = typeString.Replace("{@ObjectType}", typeElementString);
                                if (!string.IsNullOrEmpty(typeImportPath))
                                {
                                    import += templateDefines["ObjectImportAS3"].content.Replace("{@ObjectTypePath}", typeImportPath);
                                }
                                structPropertyItemsString += templateDefines["ObjectDefineAS3"].content.Replace("{@ObjectName}", protoDefine.itemNames[i]).Replace("{@ObjectType}", typeString).Replace("{@ObjectDesc}", protoDefine.itemDescs[i]);
                                structPropertyItemsGetSet += templateDefines["ObjectGetSetDefineAS3"].content.Replace("{@ObjectName}", protoDefine.itemNames[i]).Replace("{@ObjectType}", typeString).Replace("{@index}", "0x" + (i + 1).ToString("X"));

                                if (protoDefine.itemTypes[i] == "VectorCustomObject")
                                {
                                    toBytes += templateDefines["CustomVectorTypeSerializationDefineAS3"].content.Replace("{@ObjectName}", protoDefine.itemNames[i]).Replace("{@Output}", dataTypeDefines[protoDefine.itemTypes[i]].Output).Replace("{@ObjectType}", typeString).Replace("{@ObjectElementType}", typeElementString).Replace("{@index}", "0x" + (i + 1).ToString("X"));
                                    fromBytes += templateDefines["CustomVectorTypeDeserializationDefineAS3"].content.Replace("{@ObjectName}", protoDefine.itemNames[i]).Replace("{@Input}", dataTypeDefines[protoDefine.itemTypes[i]].Input).Replace("{@ObjectType}", typeString).Replace("{@ObjectElementType}", typeElementString).Replace("{@index}", "0x" + (i + 1).ToString("X"));
                                }
                                else if (enumDefines.ContainsKey(protoDefine.itemTypes[i])) {
                                    toBytes += templateDefines["EnumSerializationDefineAS3"].content.Replace("{@ObjectName}", protoDefine.itemNames[i]).Replace("{@index}", "0x" + (i + 1).ToString("X"));
                                    fromBytes += templateDefines["EnumDeserializationDefineAS3"].content.Replace("{@ObjectName}", protoDefine.itemNames[i]).Replace("{@ObjectType}", typeString).Replace("{@index}", "0x" + (i + 1).ToString("X"));
                                }
                                else if (dataTypeDefines.ContainsKey(protoDefine.itemTypes[i]))
                                {
                                    toBytes += templateDefines["BaseTypeSerializationDefineAS3"].content.Replace("{@ObjectName}", protoDefine.itemNames[i]).Replace("{@Output}", dataTypeDefines[protoDefine.itemTypes[i]].Output).Replace("{@ObjectType}", typeString).Replace("{@index}", "0x" + (i + 1).ToString("X"));
                                    fromBytes += templateDefines["BaseTypeDeserializationDefineAS3"].content.Replace("{@ObjectName}", protoDefine.itemNames[i]).Replace("{@Input}", dataTypeDefines[protoDefine.itemTypes[i]].Input).Replace("{@ObjectType}", typeString).Replace("{@index}", "0x" + (i + 1).ToString("X"));
                                }
                                else
                                {
                                    if (protoDefine.itemIsExtends[i])
                                    {
                                        toBytes += templateDefines["CustomSerializationExtendDefineAS3"].content.Replace("{@ObjectName}", protoDefine.itemNames[i]).Replace("{@ObjectType}", typeString).Replace("{@index}", "0x" + (i + 1).ToString("X"));
                                        fromBytes += templateDefines["CustomDeserializationExtendDefineAS3"].content.Replace("{@ObjectName}", protoDefine.itemNames[i]).Replace("{@ObjectType}", typeString).Replace("{@index}", "0x" + (i + 1).ToString("X"));
                                    }
                                    else
                                    {
                                        toBytes += templateDefines["CustomSerializationDefineAS3"].content.Replace("{@ObjectName}", protoDefine.itemNames[i]).Replace("{@ObjectType}", typeString).Replace("{@index}", "0x" + (i + 1).ToString("X"));
                                        fromBytes += templateDefines["CustomDeserializationDefineAS3"].content.Replace("{@ObjectName}", protoDefine.itemNames[i]).Replace("{@ObjectType}", typeString).Replace("{@index}", "0x" + (i + 1).ToString("X"));
                                    }
                                }
                            }
                            as3ProtoClassDefine = as3ProtoClassDefine.Replace("{@import}", import);
                            as3ProtoClassDefine = as3ProtoClassDefine.Replace("{@ProtoName}", protoDefine.name);
                            as3ProtoClassDefine = as3ProtoClassDefine.Replace("{@ProtoName_ToUpper}", protoDefine.name.ToUpper());
                            as3ProtoClassDefine = as3ProtoClassDefine.Replace("{@ProtoDesc}", protoDefine.desc);
                            as3ProtoClassDefine = as3ProtoClassDefine.Replace("{@ProtoPropertyItems}", structPropertyItemsString);
                            as3ProtoClassDefine = as3ProtoClassDefine.Replace("{@ProtoPropertyItemsGetSet}", structPropertyItemsGetSet);
                            as3ProtoClassDefine = as3ProtoClassDefine.Replace("{@ToBytes}", toBytes);
                            as3ProtoClassDefine = as3ProtoClassDefine.Replace("{@FromBytes}", fromBytes);
                            File.WriteAllText(exchangeDir.FullName + "\\AS3\\proto\\" + protoDefine.name + ".as", as3ProtoClassDefine);
                            break;
                        }
                    case Language.CSharp:
                        {
                            string csharpProtoClassDefine = templateDefines["ProtoCSharp"].content;

                            string protoPropertyItemsString = "";
                            string protoPropertyItemsGetSet = "";
                            string toBytes = "";
                            string fromBytes = "";
                            string import = "";
                            protoSerializationIDDefineTemp += templateDefines["ProtoSerializationIDDefineCSharp"].content.Replace("{@ProtoName_ToUpper}", protoDefine.name.ToUpper()).Replace("{@ProtoID}", seriailizationID.ToString());
                            addProtoSerializationIDDefineToMapTemp += templateDefines["AddProtoSerializationIDDefineToMapCSharp"].content.Replace("{@ProtoName_ToUpper}", protoDefine.name.ToUpper()).Replace("{@ProtoName}", protoDefine.name);
                            for (int i = 0; i < protoDefine.itemNames.Count; i++)
                            {
                                string typeString = dataTypeDefines.ContainsKey(protoDefine.itemTypes[i]) ? dataTypeDefines[protoDefine.itemTypes[i]].CSharp : protoDefine.itemTypes[i];
                                string typeImportPath = protoDefine.ItemImportPathCSharp[i];
                                string typeElementString = protoDefine.itemElementTypes[i];
                                typeString = typeString.Replace("{@ObjectType}", typeElementString);
                                if (!string.IsNullOrEmpty(typeImportPath))
                                {
                                    import += templateDefines["ObjectImportCSharp"].content.Replace("{@ObjectTypePath}", typeImportPath);
                                }

                                protoPropertyItemsString += templateDefines["ObjectDefineCSharp"].content.Replace("{@ObjectName}", protoDefine.itemNames[i]).Replace("{@ObjectType}", typeString).Replace("{@ObjectDesc}", protoDefine.itemDescs[i]);
                                protoPropertyItemsGetSet += templateDefines["ObjectGetSetDefineCSharp"].content.Replace("{@ObjectName}", protoDefine.itemNames[i]).Replace("{@ObjectType}", typeString);

                                if (protoDefine.itemTypes[i] == "VectorCustomObject")
                                {
                                    toBytes += templateDefines["CustomVectorTypeSerializationDefineCSharp"].content.Replace("{@ObjectName}", protoDefine.itemNames[i]).Replace("{@Output}", dataTypeDefines[protoDefine.itemTypes[i]].Output).Replace("{@ObjectType}", typeString).Replace("{@ObjectElementType}", typeElementString);
                                    fromBytes += templateDefines["CustomVectorTypeDeserializationDefineCSharp"].content.Replace("{@ObjectName}", protoDefine.itemNames[i]).Replace("{@Input}", dataTypeDefines[protoDefine.itemTypes[i]].Input).Replace("{@ObjectType}", typeString).Replace("{@ObjectElementType}", typeElementString);
                                }
                                else if (dataTypeDefines.ContainsKey(protoDefine.itemTypes[i]))
                                {
                                    toBytes += templateDefines["BaseTypeSerializationDefineCSharp"].content.Replace("{@ObjectName}", protoDefine.itemNames[i]).Replace("{@Output}", dataTypeDefines[protoDefine.itemTypes[i]].Output).Replace("{@ObjectType}", typeString);
                                    fromBytes += templateDefines["BaseTypeDeserializationDefineCSharp"].content.Replace("{@ObjectName}", protoDefine.itemNames[i]).Replace("{@Input}", dataTypeDefines[protoDefine.itemTypes[i]].Input).Replace("{@ObjectType}", typeString);
                                }
                                else
                                {
                                    if (protoDefine.itemIsExtends[i])
                                    {
                                        toBytes += templateDefines["CustomSerializationExtendDefineCSharp"].content.Replace("{@ObjectName}", protoDefine.itemNames[i]).Replace("{@ObjectType}", typeString);
                                        fromBytes += templateDefines["CustomDeserializationExtendDefineCSharp"].content.Replace("{@ObjectName}", protoDefine.itemNames[i]).Replace("{@ObjectType}", typeString);
                                    }
                                    else
                                    {
                                        toBytes += templateDefines["CustomSerializationDefineCSharp"].content.Replace("{@ObjectName}", protoDefine.itemNames[i]).Replace("{@ObjectType}", typeString);
                                        fromBytes += templateDefines["CustomDeserializationDefineCSharp"].content.Replace("{@ObjectName}", protoDefine.itemNames[i]).Replace("{@ObjectType}", typeString);
                                    }
                                }
                            }
                            csharpProtoClassDefine = csharpProtoClassDefine.Replace("{@import}", import);
                            csharpProtoClassDefine = csharpProtoClassDefine.Replace("{@ProtoName}", protoDefine.name);
                            csharpProtoClassDefine = csharpProtoClassDefine.Replace("{@ProtoName_ToUpper}", protoDefine.name.ToUpper());
                            csharpProtoClassDefine = csharpProtoClassDefine.Replace("{@ProtoDesc}", protoDefine.desc);
                            csharpProtoClassDefine = csharpProtoClassDefine.Replace("{@ProtoPropertyItems}", protoPropertyItemsString);
                            csharpProtoClassDefine = csharpProtoClassDefine.Replace("{@ProtoPropertyItemsGetSet}", protoPropertyItemsGetSet);
                            csharpProtoClassDefine = csharpProtoClassDefine.Replace("{@ToBytes}", toBytes);
                            csharpProtoClassDefine = csharpProtoClassDefine.Replace("{@FromBytes}", fromBytes);
                            File.WriteAllText(exchangeDir.FullName + "\\CSharp\\Proto\\" + protoDefine.name + ".cs", csharpProtoClassDefine);
                            break;
                        }
                    case Language.Java:
                        {
                            string javaProtoClassDefine = templateDefines["ProtoJava"].content;
                            string structPropertyItemsString = "";
                            string structPropertyItemsGetSet = "";
                            string toBytes = "";
                            string fromBytes = "";
                            string import = "";
                            protoSerializationIDDefineJava += templateDefines["ProtoSerializationIDDefineJava"].content.Replace("{@ProtoName_ToUpper}", protoDefine.name.ToUpper()).Replace("{@ProtoID}", seriailizationID.ToString());
                            addProtoSerializationIDDefineToMapJava += templateDefines["AddProtoSerializationIDDefineToMapJava"].content.Replace("{@ProtoName_ToUpper}", protoDefine.name.ToUpper()).Replace("{@ProtoName}", protoDefine.name);
                            for (int i = 0; i < protoDefine.itemNames.Count; i++)
                            {
                                string typeString = dataTypeDefines.ContainsKey(protoDefine.itemTypes[i]) ? dataTypeDefines[protoDefine.itemTypes[i]].Java : protoDefine.itemTypes[i];
                                string typeImportPath = protoDefine.ItemImportPathJava[i];
                                string typeElementString = protoDefine.itemElementTypes[i];
                                typeString = typeString.Replace("{@ObjectType}", typeElementString);
                                if (!string.IsNullOrEmpty(typeImportPath))
                                {
                                    import += templateDefines["ObjectImportJava"].content.Replace("{@ObjectTypePath}", typeImportPath);
                                }
                                structPropertyItemsString += templateDefines["ObjectDefineJava"].content.Replace("{@ObjectName}", protoDefine.itemNames[i]).Replace("{@ObjectType}", typeString).Replace("{@ObjectDesc}", protoDefine.itemDescs[i]);
                                structPropertyItemsGetSet += templateDefines["ObjectGetSetDefineJava"].content.Replace("{@ObjectName}", protoDefine.itemNames[i]).Replace("{@ObjectType}", typeString).Replace("{@index}", "0x" + (i + 1).ToString("X"));

                                if (protoDefine.itemTypes[i] == "VectorCustomObject")
                                {
                                    toBytes += templateDefines["CustomVectorTypeSerializationDefineJava"].content.Replace("{@ObjectName}", protoDefine.itemNames[i]).Replace("{@Output}", dataTypeDefines[protoDefine.itemTypes[i]].Output).Replace("{@ObjectType}", typeString).Replace("{@ObjectElementType}", typeElementString).Replace("{@index}", "0x" + (i + 1).ToString("X"));
                                    fromBytes += templateDefines["CustomVectorTypeDeserializationDefineJava"].content.Replace("{@ObjectName}", protoDefine.itemNames[i]).Replace("{@Input}", dataTypeDefines[protoDefine.itemTypes[i]].Input).Replace("{@ObjectType}", typeString).Replace("{@ObjectElementType}", typeElementString).Replace("{@index}", "0x" + (i + 1).ToString("X"));
                                }
                                else if (enumDefines.ContainsKey(protoDefine.itemTypes[i]))
                                {
                                    toBytes += templateDefines["EnumSerializationDefineJava"].content.Replace("{@ObjectName}", protoDefine.itemNames[i]).Replace("{@index}", "0x" + (i + 1).ToString("X"));
                                    fromBytes += templateDefines["EnumDeserializationDefineJava"].content.Replace("{@ObjectName}", protoDefine.itemNames[i]).Replace("{@ObjectType}", typeString).Replace("{@index}", "0x" + (i + 1).ToString("X"));
                                }
                                else if (dataTypeDefines.ContainsKey(protoDefine.itemTypes[i]))
                                {
                                    toBytes += templateDefines["BaseTypeSerializationDefineJava"].content.Replace("{@ObjectName}", protoDefine.itemNames[i]).Replace("{@Output}", dataTypeDefines[protoDefine.itemTypes[i]].Output).Replace("{@ObjectType}", typeString).Replace("{@index}", "0x" + (i + 1).ToString("X"));
                                    fromBytes += templateDefines["BaseTypeDeserializationDefineJava"].content.Replace("{@ObjectName}", protoDefine.itemNames[i]).Replace("{@Input}", dataTypeDefines[protoDefine.itemTypes[i]].Input).Replace("{@ObjectType}", typeString).Replace("{@index}", "0x" + (i + 1).ToString("X"));
                                }
                                else
                                {
                                    if (protoDefine.itemIsExtends[i])
                                    {
                                        toBytes += templateDefines["CustomSerializationExtendDefineJava"].content.Replace("{@ObjectName}", protoDefine.itemNames[i]).Replace("{@ObjectType}", typeString).Replace("{@index}", "0x" + (i + 1).ToString("X"));
                                        fromBytes += templateDefines["CustomDeserializationExtendDefineJava"].content.Replace("{@ObjectName}", protoDefine.itemNames[i]).Replace("{@ObjectType}", typeString).Replace("{@index}", "0x" + (i + 1).ToString("X"));
                                    }
                                    else
                                    {
                                        toBytes += templateDefines["CustomSerializationDefineJava"].content.Replace("{@ObjectName}", protoDefine.itemNames[i]).Replace("{@ObjectType}", typeString).Replace("{@index}", "0x" + (i + 1).ToString("X"));
                                        fromBytes += templateDefines["CustomDeserializationDefineJava"].content.Replace("{@ObjectName}", protoDefine.itemNames[i]).Replace("{@ObjectType}", typeString).Replace("{@index}", "0x" + (i + 1).ToString("X"));
                                    }
                                }
                            }
                            javaProtoClassDefine = javaProtoClassDefine.Replace("{@import}", import);
                            javaProtoClassDefine = javaProtoClassDefine.Replace("{@ProtoName}", protoDefine.name);
                            javaProtoClassDefine = javaProtoClassDefine.Replace("{@ProtoName_ToUpper}", protoDefine.name.ToUpper());
                            javaProtoClassDefine = javaProtoClassDefine.Replace("{@ProtoDesc}", protoDefine.desc);
                            javaProtoClassDefine = javaProtoClassDefine.Replace("{@ProtoPropertyItems}", structPropertyItemsString);
                            javaProtoClassDefine = javaProtoClassDefine.Replace("{@ProtoPropertyItemsGetSet}", structPropertyItemsGetSet);
                            javaProtoClassDefine = javaProtoClassDefine.Replace("{@ToBytes}", toBytes);
                            javaProtoClassDefine = javaProtoClassDefine.Replace("{@FromBytes}", fromBytes);
                            javaProtoClassDefine = javaProtoClassDefine.Replace("{@ItemsCount}", "" + protoDefine.itemNames.Count);
                            File.WriteAllText(exchangeDir.FullName + "\\Java\\proto\\" + protoDefine.name + ".java", javaProtoClassDefine);
                            break;
                        }
                    case Language.CPlusPlus:
                        {
                            string cplusplusProtoClassDefine = templateDefines["ProtoCPlusPlus"].content;

                            string protoPropertyItemsString = "";
                            string protoPropertyItemsGetSet = "";
                            string toBytes = "";
                            string fromBytes = "";
                            string import = "";
                            protoSerializationIDDefineCPlusPlus += templateDefines["ProtoSerializationIDDefineCPlusPlus"].content.Replace("{@ProtoName_ToUpper}", protoDefine.name.ToUpper()).Replace("{@ProtoID}", seriailizationID.ToString());
                            addProtoSerializationIDDefineToMapCPlusPlus += templateDefines["AddProtoSerializationIDDefineToMapCPlusPlus"].content.Replace("{@ProtoName_ToUpper}", protoDefine.name.ToUpper()).Replace("{@ProtoName}", protoDefine.name);
                            for (int i = 0; i < protoDefine.itemNames.Count; i++)
                            {
                                string typeString = dataTypeDefines.ContainsKey(protoDefine.itemTypes[i]) ? dataTypeDefines[protoDefine.itemTypes[i]].CPlusPlus : protoDefine.itemTypes[i];
                                string typeImportPath = protoDefine.ItemImportPathCPlusPlus[i];
                                string typeElementString = protoDefine.itemElementTypes[i];
                                typeString = typeString.Replace("{@ObjectType}", typeElementString);
                                if (!string.IsNullOrEmpty(typeImportPath))
                                {
                                    import += templateDefines["ObjectImportCPlusPlus"].content.Replace("{@ObjectTypePath}", typeImportPath);
                                }

                                protoPropertyItemsString += templateDefines["ObjectDefineCPlusPlus"].content.Replace("{@ObjectName}", protoDefine.itemNames[i]).Replace("{@ObjectType}", typeString).Replace("{@ObjectDesc}", protoDefine.itemDescs[i]);
                                protoPropertyItemsGetSet += templateDefines["ObjectGetSetDefineCPlusPlus"].content.Replace("{@ObjectName}", protoDefine.itemNames[i]).Replace("{@ObjectType}", typeString);

                                if (protoDefine.itemTypes[i] == "VectorCustomObject")
                                {
                                    toBytes += templateDefines["CustomVectorTypeSerializationDefineCPlusPlus"].content.Replace("{@ObjectName}", protoDefine.itemNames[i]).Replace("{@Output}", dataTypeDefines[protoDefine.itemTypes[i]].Output).Replace("{@ObjectType}", typeString).Replace("{@ObjectElementType}", typeElementString);
                                    fromBytes += templateDefines["CustomVectorTypeDeserializationDefineCPlusPlus"].content.Replace("{@ObjectName}", protoDefine.itemNames[i]).Replace("{@Input}", dataTypeDefines[protoDefine.itemTypes[i]].Input).Replace("{@ObjectType}", typeString).Replace("{@ObjectElementType}", typeElementString);
                                }
                                else if (dataTypeDefines.ContainsKey(protoDefine.itemTypes[i]))
                                {
                                    toBytes += templateDefines["BaseTypeSerializationDefineCPlusPlus"].content.Replace("{@ObjectName}", protoDefine.itemNames[i]).Replace("{@Output}", dataTypeDefines[protoDefine.itemTypes[i]].Output).Replace("{@ObjectType}", typeString);
                                    fromBytes += templateDefines["BaseTypeDeserializationDefineCPlusPlus"].content.Replace("{@ObjectName}", protoDefine.itemNames[i]).Replace("{@Input}", dataTypeDefines[protoDefine.itemTypes[i]].Input).Replace("{@ObjectType}", typeString);
                                }
                                else
                                {
                                    if (protoDefine.itemIsExtends[i])
                                    {
                                        toBytes += templateDefines["CustomSerializationExtendDefineCPlusPlus"].content.Replace("{@ObjectName}", protoDefine.itemNames[i]).Replace("{@ObjectType}", typeString);
                                        fromBytes += templateDefines["CustomDeserializationExtendDefineCPlusPlus"].content.Replace("{@ObjectName}", protoDefine.itemNames[i]).Replace("{@ObjectType}", typeString);
                                    }
                                    else
                                    {
                                        toBytes += templateDefines["CustomSerializationDefineCPlusPlus"].content.Replace("{@ObjectName}", protoDefine.itemNames[i]).Replace("{@ObjectType}", typeString);
                                        fromBytes += templateDefines["CustomDeserializationDefineCPlusPlus"].content.Replace("{@ObjectName}", protoDefine.itemNames[i]).Replace("{@ObjectType}", typeString);
                                    }
                                }
                            }
                            cplusplusProtoClassDefine = cplusplusProtoClassDefine.Replace("{@import}", import);
                            cplusplusProtoClassDefine = cplusplusProtoClassDefine.Replace("{@ProtoName}", protoDefine.name);
                            cplusplusProtoClassDefine = cplusplusProtoClassDefine.Replace("{@ProtoName_ToUpper}", protoDefine.name.ToUpper());
                            cplusplusProtoClassDefine = cplusplusProtoClassDefine.Replace("{@ProtoDesc}", protoDefine.desc);
                            cplusplusProtoClassDefine = cplusplusProtoClassDefine.Replace("{@ProtoPropertyItems}", protoPropertyItemsString);
                            cplusplusProtoClassDefine = cplusplusProtoClassDefine.Replace("{@ProtoPropertyItemsGetSet}", protoPropertyItemsGetSet);
                            cplusplusProtoClassDefine = cplusplusProtoClassDefine.Replace("{@ToBytes}", toBytes);
                            cplusplusProtoClassDefine = cplusplusProtoClassDefine.Replace("{@FromBytes}", fromBytes);
                            cplusplusProtoSeriailizationDefineTotal += cplusplusProtoClassDefine;
                            break;
                        }
                }
            }

            // 生成消息号定义文件
            switch (language)
            {
                case Language.AS3:
                    {
                        as3ProtoSeriailizationDefine = as3ProtoSeriailizationDefine.Replace("{@ProtoSerializationIDDefineAS3}", protoSerializationIDDefineAS3).Replace("{@AddProtoSerializationIDDefineToMapAS3}", addProtoSerializationIDDefineToMapAS3);
                        File.WriteAllText(exchangeDir.FullName + "\\AS3\\proto\\ProtoSerializationDefine.as", as3ProtoSeriailizationDefine);
                        break;
                    }
                case Language.CSharp:
                    {
                        csharpProtoSeriailizationDefine = csharpProtoSeriailizationDefine.Replace("{@ProtoSerializationIDDefineCSharp}", protoSerializationIDDefineTemp).Replace("{@AddProtoSerializationIDDefineToMapCSharp}", addProtoSerializationIDDefineToMapTemp);
                        File.WriteAllText(exchangeDir.FullName + "\\CSharp\\Proto\\ProtoSerializationDefine.cs", csharpProtoSeriailizationDefine);
                        break;
                    }
                case Language.CPlusPlus:
                    {
                        cplusplusProtoSeriailizationDefine = cplusplusProtoSeriailizationDefine.Replace("{@ProtoSerializationIDDefineCPlusPlus}", protoSerializationIDDefineCPlusPlus).Replace("{@AddProtoSerializationIDDefineToMapCPlusPlus}", addProtoSerializationIDDefineToMapCPlusPlus);
                        cplusplusProtoSeriailizationDefine = "#ifndef __PROTOCOLS_ID__\n#define __PROTOCOLS_ID__\n \n"
    + cplusplusProtoSeriailizationDefine
    + "\n#endif\n";

                        File.WriteAllText(exchangeDir.FullName + "\\CPlusPlus\\Proto\\ProtoSerializationDefine.h", cplusplusProtoSeriailizationDefine);
                        break;
                    }
                case Language.Java:
                    {
                        javaProtoSeriailizationDefine = javaProtoSeriailizationDefine.Replace("{@ProtoSerializationIDDefineJava}", protoSerializationIDDefineJava).Replace("{@AddProtoSerializationIDDefineToMapJava}", addProtoSerializationIDDefineToMapJava);
                        File.WriteAllText(exchangeDir.FullName + "\\Java\\ProtoSerializationDefine.java", javaProtoSeriailizationDefine);
                        break;
                    }
            }

            if (Language.CPlusPlus == language)
            {
                cplusplusProtoSeriailizationDefineTotal = "#ifndef __PROTOCOLS__\n#define __PROTOCOLS__\n#include \"ProtoSerializationDefine.h\"\n#include \"../CPPByteArray.h\"\n"
                    + cplusplusProtoSeriailizationDefineTotal
                    + "\n#endif\n";

                File.WriteAllText(exchangeDir.FullName + "\\CPlusPlus\\Proto\\" + "Protocols" + ".h", cplusplusProtoSeriailizationDefineTotal);

            }
        }
    }
}
