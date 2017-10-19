
			
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

	//服务器通知，角色创建失败
	public class SC_CreateRoleFail extends CommunicationDataBase
	{	
		private var __mask__:ByteArray = new ByteArray();
		public static var isser : Issuer = new Issuer();

			
		// 失败原因
		private var _failReason : EnumCreateRoleFailReason;

		
		public function SC_CreateRoleFail()
		{
		}
		
		public override function getSerializationID():int
		{
			return ProtoSerializationDefine.SC_CREATEROLEFAIL;
		}


			
		public function get failReason():EnumCreateRoleFailReason
		{
			return this._failReason;
		}
			
		public function set failReason(__failReason : EnumCreateRoleFailReason):void
		{
			SerializationHelper.getInstance().writeMask(__mask__, 0x1);
			this._failReason = __failReason;
		}

		
		public override function toBytes(__targetBytes:IDataOutput=null):IDataOutput
		{
			SerializationHelper.getInstance().writeByteArray(__targetBytes, this.__mask__);

			
			if(SerializationHelper.getInstance().readMask(this.__mask__, 0x1) == 1)
				SerializationHelper.getInstance().writeEnum(__targetBytes, this._failReason.value);

		
			return __targetBytes;
		}

		public override function fromBytes(__serializationBytes : IDataInput, __weight:IWeightData = null):void
		{
			this.__mask__ = SerializationHelper.getInstance().readByteArray(__serializationBytes);

			
			if(SerializationHelper.getInstance().readMask(this.__mask__, 0x1) == 1)
				this._failReason = SerializationHelper.getInstance().readEnum(__serializationBytes, EnumCreateRoleFailReason) as EnumCreateRoleFailReason;

		
		}
		
		override public function fireDataReciveEvent(sender:ICommunication, __session:Session):void
		{
			isser.issue("DataRecive", sender, __session, this); 
		}
	}
}

		