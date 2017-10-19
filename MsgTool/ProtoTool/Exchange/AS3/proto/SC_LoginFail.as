
			
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

	//服务器通知，登录失败
	public class SC_LoginFail extends CommunicationDataBase
	{	
		private var __mask__:ByteArray = new ByteArray();
		public static var isser : Issuer = new Issuer();

			
		// 渠道ID
		private var _channel : String;

		
			
		// 账号
		private var _account : String;

		
			
		// 失败原因
		private var _failReason : EnumLoginFailReason;

		
		public function SC_LoginFail()
		{
		}
		
		public override function getSerializationID():int
		{
			return ProtoSerializationDefine.SC_LOGINFAIL;
		}


			
		public function get channel():String
		{
			return this._channel;
		}
			
		public function set channel(__channel : String):void
		{
			SerializationHelper.getInstance().writeMask(__mask__, 0x1);
			this._channel = __channel;
		}

		
			
		public function get account():String
		{
			return this._account;
		}
			
		public function set account(__account : String):void
		{
			SerializationHelper.getInstance().writeMask(__mask__, 0x2);
			this._account = __account;
		}

		
			
		public function get failReason():EnumLoginFailReason
		{
			return this._failReason;
		}
			
		public function set failReason(__failReason : EnumLoginFailReason):void
		{
			SerializationHelper.getInstance().writeMask(__mask__, 0x3);
			this._failReason = __failReason;
		}

		
		public override function toBytes(__targetBytes:IDataOutput=null):IDataOutput
		{
			SerializationHelper.getInstance().writeByteArray(__targetBytes, this.__mask__);

			
			if(SerializationHelper.getInstance().readMask(this.__mask__, 0x1) == 1)
				SerializationHelper.getInstance().writeUTF(__targetBytes, this._channel);

		
			
			if(SerializationHelper.getInstance().readMask(this.__mask__, 0x2) == 1)
				SerializationHelper.getInstance().writeUTF(__targetBytes, this._account);

		
			
			if(SerializationHelper.getInstance().readMask(this.__mask__, 0x3) == 1)
				SerializationHelper.getInstance().writeEnum(__targetBytes, this._failReason.value);

		
			return __targetBytes;
		}

		public override function fromBytes(__serializationBytes : IDataInput, __weight:IWeightData = null):void
		{
			this.__mask__ = SerializationHelper.getInstance().readByteArray(__serializationBytes);

			
			if(SerializationHelper.getInstance().readMask(this.__mask__, 0x1) == 1)
				this._channel = SerializationHelper.getInstance().readUTF(__serializationBytes) as String;

		
			
			if(SerializationHelper.getInstance().readMask(this.__mask__, 0x2) == 1)
				this._account = SerializationHelper.getInstance().readUTF(__serializationBytes) as String;

		
			
			if(SerializationHelper.getInstance().readMask(this.__mask__, 0x3) == 1)
				this._failReason = SerializationHelper.getInstance().readEnum(__serializationBytes, EnumLoginFailReason) as EnumLoginFailReason;

		
		}
		
		override public function fireDataReciveEvent(sender:ICommunication, __session:Session):void
		{
			isser.issue("DataRecive", sender, __session, this); 
		}
	}
}

		