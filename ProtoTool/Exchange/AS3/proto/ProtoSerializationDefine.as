
			
package proto
{
  import flash.utils.ByteArray;
	
	import navigate.communication.CommunicationDataBase;
	import navigate.communication.ICommunicationDataDefine;
	import navigate.data.Bimap;
	// Proto Seriialization Define IDs
	public class ProtoSerializationDefine implements ICommunicationDataDefine
	{
		//Serialization ID Define in Bimap
		private var _serializationDefine : Bimap;

			
		public static const CS_LOGIN : int = 11;

		
			
		public static const CS_CREATEROLE : int = 12;

		
			
		public static const SC_LOGINFAIL : int = 13;

		
			
		public static const SC_CREATEROLE : int = 14;

		
			
		public static const SC_ROLELIST : int = 15;

		
			
		public static const SC_CREATEROLEFAIL : int = 16;

		
		public function ProtoSerializationDefine()
		{
			this._serializationDefine = new Bimap();
			this._serializationDefine.init();
			
			
			
			this._serializationDefine.addPair(CS_LOGIN, CS_Login);

		
			
			this._serializationDefine.addPair(CS_CREATEROLE, CS_CreateRole);

		
			
			this._serializationDefine.addPair(SC_LOGINFAIL, SC_LoginFail);

		
			
			this._serializationDefine.addPair(SC_CREATEROLE, SC_CreateRole);

		
			
			this._serializationDefine.addPair(SC_ROLELIST, SC_RoleList);

		
			
			this._serializationDefine.addPair(SC_CREATEROLEFAIL, SC_CreateRoleFail);

		
		}
    
		public function getCommunicationData(__bytes : ByteArray, __messageID : int):CommunicationDataBase
		{
			if(_serializationDefine.containKey(__messageID))
			{
				var __type : Class = _serializationDefine.getValue(__messageID);
				var __data : CommunicationDataBase = new __type as CommunicationDataBase;
				__data.fromBytes(__bytes);
				return __data;
			}
			return null;
		}
		
		public function dispose():void
		{
			this._serializationDefine.dispose();
			this._serializationDefine = null;
		}
	}
}

		