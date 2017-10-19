#ifndef __PROTOCOLS__
#define __PROTOCOLS__
#include "ProtoSerializationDefine.h"
#include "../CPPByteArray.h"


namespace Proto
{
	//Communication Data Recive Event Desegate
	//public delegate void CS_Login_DataReciveEventHandler(ICommunication sender, CS_Login_Data_Event_Args e); 
	//Client请求登陆。若在本地验证模式下，不验证密码
	class CS_Login : CObjectBase
	{
		//Communication Data Recive Event Handler
		//public static event CS_Login_DataReciveEventHandler communicationDataReciveEvent;
		


		// 服务器ID。联服的几个Server共用一个LoginServer，因此登陆时需要选择serverId
		//private int829 _serverId;

		

		// 渠道ID
		//private string _channel;

		

		// 账号
		//private string _account;

		

		// 登录秘钥
		//private string _key;

		
		public:
		CS_Login()
		{
		}
    
		int getProtoDefineID()
		{
		return ProtoSerializationDefine::CS_LOGIN;
		}



		int829 serverId;
		/*{
			get
			{
				return this._serverId;
			}
			set
			{
				this._serverId = value;
			}
		}*/

		

		string channel;
		/*{
			get
			{
				return this._channel;
			}
			set
			{
				this._channel = value;
			}
		}*/

		

		string account;
		/*{
			get
			{
				return this._account;
			}
			set
			{
				this._account = value;
			}
		}*/

		

		string key;
		/*{
			get
			{
				return this._key;
			}
			set
			{
				this._key = value;
			}
		}*/

		
		bool write2ByteArray(CPPByteArray &__targetBytes )
		{


			//SerializationHelper.getInstance().writeU29Int(__targetBytes, this._serverId);

		

			//SerializationHelper.getInstance().writeUTF(__targetBytes, this._channel);

		

			//SerializationHelper.getInstance().writeUTF(__targetBytes, this._account);

		

			//SerializationHelper.getInstance().writeUTF(__targetBytes, this._key);

		
			return false;
		}

		bool readFromByteArray(CPPByteArray & __serializationBytes)
		{


			//this._serverId = (int829)SerializationHelper.getInstance().readU29Int(__serializationBytes);

		

			//this._channel = (string)SerializationHelper.getInstance().readUTF(__serializationBytes);

		

			//this._account = (string)SerializationHelper.getInstance().readUTF(__serializationBytes);

		

			//this._key = (string)SerializationHelper.getInstance().readUTF(__serializationBytes);

		
			return false;
		}
		
		/*public override void fireDataReciveEvent(ICommunication __sender, Session __session)
    {
		if(communicationDataReciveEvent != null)
		{
			communicationDataReciveEvent(__sender, new CS_Login_Data_Event_Args(__session, this));
		}
    }*/
	}
	
	/*public class CS_Login_Data_Event_Args : CommunicationDataEventArgs
    {
        public CS_Login_Data_Event_Args(Session __session, CS_Login __data):base(__session, __data)
        {

        }
    }*/
}

		

namespace Proto
{
	//Communication Data Recive Event Desegate
	//public delegate void CS_CreateRole_DataReciveEventHandler(ICommunication sender, CS_CreateRole_Data_Event_Args e); 
	//Client请求创建角色，并进入游戏
	class CS_CreateRole : CObjectBase
	{
		//Communication Data Recive Event Handler
		//public static event CS_CreateRole_DataReciveEventHandler communicationDataReciveEvent;
		


		// 名字
		//private string _name;

		

		// 体型
		//private EnumBody _body;

		

		// 门派
		//private EnumMenPai _menpai;

		

		// 捏脸数据
		//private string _avatarFace;

		
		public:
		CS_CreateRole()
		{
		}
    
		int getProtoDefineID()
		{
		return ProtoSerializationDefine::CS_CREATEROLE;
		}



		string name;
		/*{
			get
			{
				return this._name;
			}
			set
			{
				this._name = value;
			}
		}*/

		

		EnumBody body;
		/*{
			get
			{
				return this._body;
			}
			set
			{
				this._body = value;
			}
		}*/

		

		EnumMenPai menpai;
		/*{
			get
			{
				return this._menpai;
			}
			set
			{
				this._menpai = value;
			}
		}*/

		

		string avatarFace;
		/*{
			get
			{
				return this._avatarFace;
			}
			set
			{
				this._avatarFace = value;
			}
		}*/

		
		bool write2ByteArray(CPPByteArray &__targetBytes )
		{


			//SerializationHelper.getInstance().writeUTF(__targetBytes, this._name);

		

			return __targetBytes.write2ByteArray(this._body);

		

			return __targetBytes.write2ByteArray(this._menpai);

		

			//SerializationHelper.getInstance().writeByteArray(__targetBytes, this._avatarFace);

		
			return false;
		}

		bool readFromByteArray(CPPByteArray & __serializationBytes)
		{


			//this._name = (string)SerializationHelper.getInstance().readUTF(__serializationBytes);

		

			return __serializationBytes.readFromByteArray(this._body);

		

			return __serializationBytes.readFromByteArray(this._menpai);

		

			//this._avatarFace = (string)SerializationHelper.getInstance().readByteArray(__serializationBytes);

		
			return false;
		}
		
		/*public override void fireDataReciveEvent(ICommunication __sender, Session __session)
    {
		if(communicationDataReciveEvent != null)
		{
			communicationDataReciveEvent(__sender, new CS_CreateRole_Data_Event_Args(__session, this));
		}
    }*/
	}
	
	/*public class CS_CreateRole_Data_Event_Args : CommunicationDataEventArgs
    {
        public CS_CreateRole_Data_Event_Args(Session __session, CS_CreateRole __data):base(__session, __data)
        {

        }
    }*/
}

		

namespace Proto
{
	//Communication Data Recive Event Desegate
	//public delegate void SC_LoginFail_DataReciveEventHandler(ICommunication sender, SC_LoginFail_Data_Event_Args e); 
	//服务器通知，登录失败
	class SC_LoginFail : CObjectBase
	{
		//Communication Data Recive Event Handler
		//public static event SC_LoginFail_DataReciveEventHandler communicationDataReciveEvent;
		


		// 渠道ID
		//private string _channel;

		

		// 账号
		//private string _account;

		

		// 失败原因
		//private EnumLoginFailReason _failReason;

		
		public:
		SC_LoginFail()
		{
		}
    
		int getProtoDefineID()
		{
		return ProtoSerializationDefine::SC_LOGINFAIL;
		}



		string channel;
		/*{
			get
			{
				return this._channel;
			}
			set
			{
				this._channel = value;
			}
		}*/

		

		string account;
		/*{
			get
			{
				return this._account;
			}
			set
			{
				this._account = value;
			}
		}*/

		

		EnumLoginFailReason failReason;
		/*{
			get
			{
				return this._failReason;
			}
			set
			{
				this._failReason = value;
			}
		}*/

		
		bool write2ByteArray(CPPByteArray &__targetBytes )
		{


			//SerializationHelper.getInstance().writeUTF(__targetBytes, this._channel);

		

			//SerializationHelper.getInstance().writeUTF(__targetBytes, this._account);

		

			return __targetBytes.write2ByteArray(this._failReason);

		
			return false;
		}

		bool readFromByteArray(CPPByteArray & __serializationBytes)
		{


			//this._channel = (string)SerializationHelper.getInstance().readUTF(__serializationBytes);

		

			//this._account = (string)SerializationHelper.getInstance().readUTF(__serializationBytes);

		

			return __serializationBytes.readFromByteArray(this._failReason);

		
			return false;
		}
		
		/*public override void fireDataReciveEvent(ICommunication __sender, Session __session)
    {
		if(communicationDataReciveEvent != null)
		{
			communicationDataReciveEvent(__sender, new SC_LoginFail_Data_Event_Args(__session, this));
		}
    }*/
	}
	
	/*public class SC_LoginFail_Data_Event_Args : CommunicationDataEventArgs
    {
        public SC_LoginFail_Data_Event_Args(Session __session, SC_LoginFail __data):base(__session, __data)
        {

        }
    }*/
}

		

namespace Proto
{
	//Communication Data Recive Event Desegate
	//public delegate void SC_CreateRole_DataReciveEventHandler(ICommunication sender, SC_CreateRole_Data_Event_Args e); 
	//服务器通知，你当前没有角色，请创建角色，这是服务器帮你随机到的角色名，基本能够保证不重复
	class SC_CreateRole : CObjectBase
	{
		//Communication Data Recive Event Handler
		//public static event SC_CreateRole_DataReciveEventHandler communicationDataReciveEvent;
		


		// 姓名，男
		//private string _nameMale;

		

		// 姓名，女
		//private string _nameFemale;

		
		public:
		SC_CreateRole()
		{
		}
    
		int getProtoDefineID()
		{
		return ProtoSerializationDefine::SC_CREATEROLE;
		}



		string nameMale;
		/*{
			get
			{
				return this._nameMale;
			}
			set
			{
				this._nameMale = value;
			}
		}*/

		

		string nameFemale;
		/*{
			get
			{
				return this._nameFemale;
			}
			set
			{
				this._nameFemale = value;
			}
		}*/

		
		bool write2ByteArray(CPPByteArray &__targetBytes )
		{


			//SerializationHelper.getInstance().writeUTF(__targetBytes, this._nameMale);

		

			//SerializationHelper.getInstance().writeUTF(__targetBytes, this._nameFemale);

		
			return false;
		}

		bool readFromByteArray(CPPByteArray & __serializationBytes)
		{


			//this._nameMale = (string)SerializationHelper.getInstance().readUTF(__serializationBytes);

		

			//this._nameFemale = (string)SerializationHelper.getInstance().readUTF(__serializationBytes);

		
			return false;
		}
		
		/*public override void fireDataReciveEvent(ICommunication __sender, Session __session)
    {
		if(communicationDataReciveEvent != null)
		{
			communicationDataReciveEvent(__sender, new SC_CreateRole_Data_Event_Args(__session, this));
		}
    }*/
	}
	
	/*public class SC_CreateRole_Data_Event_Args : CommunicationDataEventArgs
    {
        public SC_CreateRole_Data_Event_Args(Session __session, SC_CreateRole __data):base(__session, __data)
        {

        }
    }*/
}

		

namespace Proto
{
	//Communication Data Recive Event Desegate
	//public delegate void SC_RoleList_DataReciveEventHandler(ICommunication sender, SC_RoleList_Data_Event_Args e); 
	//服务器通知，角色列表
	class SC_RoleList : CObjectBase
	{
		//Communication Data Recive Event Handler
		//public static event SC_RoleList_DataReciveEventHandler communicationDataReciveEvent;
		


		// 角色列表
		//private vector<CASObject> _roleInfos;

		
		public:
		SC_RoleList()
		{
		}
    
		int getProtoDefineID()
		{
		return ProtoSerializationDefine::SC_ROLELIST;
		}



		vector<CASObject> roleInfos;
		/*{
			get
			{
				return this._roleInfos;
			}
			set
			{
				this._roleInfos = value;
			}
		}*/

		
		bool write2ByteArray(CPPByteArray &__targetBytes )
		{

			
			//SerializationHelper.getInstance().customSerializationVector(__targetBytes, this._roleInfos);

		
			return false;
		}

		bool readFromByteArray(CPPByteArray & __serializationBytes)
		{

			
			//this._roleInfos = SerializationHelper.getInstance().customDeserializationVector(__serializationBytes, RoleProto) as vector<CASObject>;

		
			return false;
		}
		
		/*public override void fireDataReciveEvent(ICommunication __sender, Session __session)
    {
		if(communicationDataReciveEvent != null)
		{
			communicationDataReciveEvent(__sender, new SC_RoleList_Data_Event_Args(__session, this));
		}
    }*/
	}
	
	/*public class SC_RoleList_Data_Event_Args : CommunicationDataEventArgs
    {
        public SC_RoleList_Data_Event_Args(Session __session, SC_RoleList __data):base(__session, __data)
        {

        }
    }*/
}

		

namespace Proto
{
	//Communication Data Recive Event Desegate
	//public delegate void SC_CreateRoleFail_DataReciveEventHandler(ICommunication sender, SC_CreateRoleFail_Data_Event_Args e); 
	//服务器通知，角色创建失败
	class SC_CreateRoleFail : CObjectBase
	{
		//Communication Data Recive Event Handler
		//public static event SC_CreateRoleFail_DataReciveEventHandler communicationDataReciveEvent;
		


		// 失败原因
		//private EnumCreateRoleFailReason _failReason;

		
		public:
		SC_CreateRoleFail()
		{
		}
    
		int getProtoDefineID()
		{
		return ProtoSerializationDefine::SC_CREATEROLEFAIL;
		}



		EnumCreateRoleFailReason failReason;
		/*{
			get
			{
				return this._failReason;
			}
			set
			{
				this._failReason = value;
			}
		}*/

		
		bool write2ByteArray(CPPByteArray &__targetBytes )
		{


			return __targetBytes.write2ByteArray(this._failReason);

		
			return false;
		}

		bool readFromByteArray(CPPByteArray & __serializationBytes)
		{


			return __serializationBytes.readFromByteArray(this._failReason);

		
			return false;
		}
		
		/*public override void fireDataReciveEvent(ICommunication __sender, Session __session)
    {
		if(communicationDataReciveEvent != null)
		{
			communicationDataReciveEvent(__sender, new SC_CreateRoleFail_Data_Event_Args(__session, this));
		}
    }*/
	}
	
	/*public class SC_CreateRoleFail_Data_Event_Args : CommunicationDataEventArgs
    {
        public SC_CreateRoleFail_Data_Event_Args(Session __session, SC_CreateRoleFail __data):base(__session, __data)
        {

        }
    }*/
}

		
#endif
