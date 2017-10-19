
			
package proto
{
	import flash.utils.ByteArray;
	import flash.utils.IDataInput;
	import flash.utils.IDataOutput;
	
	import commonData.*;
	import enum.*;
	
	import navigate.atom.define.IWeightData;
	import navigate.communication.CommunicationDataBase;
	import navigate.communication.ICommunication;
	import navigate.communication.Session;
	import navigate.event.Issuer;
	import navigate.helper.serialization.SerializationHelper;

	//服务器通知，你当前没有角色，请创建角色，这是服务器帮你随机到的角色名，基本能够保证不重复
	public class SC_CreateRole extends CommunicationDataBase
	{	
		private var __mask__:ByteArray = new ByteArray();
		public static var isser : Issuer = new Issuer();

			
		// 姓名，男
		private var _nameMale : String;

		
			
		// 姓名，女
		private var _nameFemale : String;

		
		public function SC_CreateRole()
		{
		}
		
		public override function getSerializationID():int
		{
			return ProtoSerializationDefine.SC_CREATEROLE;
		}


			
		public function get nameMale():String
		{
			return this._nameMale;
		}
			
		public function set nameMale(__nameMale : String):void
		{
			SerializationHelper.getInstance().writeMask(__mask__, 0x1);
			this._nameMale = __nameMale;
		}

		
			
		public function get nameFemale():String
		{
			return this._nameFemale;
		}
			
		public function set nameFemale(__nameFemale : String):void
		{
			SerializationHelper.getInstance().writeMask(__mask__, 0x2);
			this._nameFemale = __nameFemale;
		}

		
		public override function toBytes(__targetBytes:IDataOutput=null):IDataOutput
		{
			SerializationHelper.getInstance().writeByteArray(__targetBytes, this.__mask__);

			
			if(SerializationHelper.getInstance().readMask(this.__mask__, 0x1) == 1)
				SerializationHelper.getInstance().writeUTF(__targetBytes, this._nameMale);

		
			
			if(SerializationHelper.getInstance().readMask(this.__mask__, 0x2) == 1)
				SerializationHelper.getInstance().writeUTF(__targetBytes, this._nameFemale);

		
			return __targetBytes;
		}

		public override function fromBytes(__serializationBytes : IDataInput, __weight:IWeightData = null):void
		{
			this.__mask__ = SerializationHelper.getInstance().readByteArray(__serializationBytes);

			
			if(SerializationHelper.getInstance().readMask(this.__mask__, 0x1) == 1)
				this._nameMale = SerializationHelper.getInstance().readUTF(__serializationBytes) as String;

		
			
			if(SerializationHelper.getInstance().readMask(this.__mask__, 0x2) == 1)
				this._nameFemale = SerializationHelper.getInstance().readUTF(__serializationBytes) as String;

		
		}
		
		override public function fireDataReciveEvent(sender:ICommunication, __session:Session):void
		{
			isser.issue("DataRecive", sender, __session, this); 
		}
	}
}

		