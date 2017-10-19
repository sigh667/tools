#ifndef __PROTOCOLS_ID__
#define __PROTOCOLS_ID__
 


//#include <map>
//using namespace std;
namespace Proto
{
	// Proto Seriialization Define IDs
	class ProtoSerializationDefine /*: ICommunicationDataDefine*/
	{
		public:
		//Serialization ID Define in Bimap
		//map<int, Type> _serializationDefine;




		static const int CS_LOGIN = 11;

		

		static const int CS_CREATEROLE = 12;

		

		static const int SC_LOGINFAIL = 13;

		

		static const int SC_CREATEROLE = 14;

		

		static const int SC_ROLELIST = 15;

		

		static const int SC_CREATEROLEFAIL = 16;

		

		/*ProtoSerializationDefine()
		{
			//this._serializationDefine = new map<int, Type>();
			
			

			//this._serializationDefine.Add(CS_LOGIN, typeof(CS_Login));

		

			//this._serializationDefine.Add(CS_CREATEROLE, typeof(CS_CreateRole));

		

			//this._serializationDefine.Add(SC_LOGINFAIL, typeof(SC_LoginFail));

		

			//this._serializationDefine.Add(SC_CREATEROLE, typeof(SC_CreateRole));

		

			//this._serializationDefine.Add(SC_ROLELIST, typeof(SC_RoleList));

		

			//this._serializationDefine.Add(SC_CREATEROLEFAIL, typeof(SC_CreateRoleFail));

		
		}*/
    
	    /*CommunicationDataBase getCommunicationData(ByteArray __bytes, int __messageID)
	    {
	        if(_serializationDefine.find(__messageID) != _serializationDefine.end())
	        {
	            Type __type = _serializationDefine[__messageID];
	            CommunicationDataBase __data = (CommunicationDataBase)Activator.CreateInstance(__type);
	            __data.fromBytes(__bytes);
	            return __data;
	        }
	        return null;
	    }*/
		
		/*public void Dispose()
		{
			this._serializationDefine = null;
		}*/
	}
}

		
#endif
