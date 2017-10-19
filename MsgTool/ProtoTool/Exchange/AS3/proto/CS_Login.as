
			
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

	//Client请求登陆。若在本地验证模式下，不验证密码
	public class CS_Login extends CommunicationDataBase
	{	
		private var __mask__:ByteArray = new ByteArray();
		public static var isser : Issuer = new Issuer();

			
		// 服务器ID。联服的几个Server共用一个LoginServer，因此登陆时需要选择serverId
		private var _serverId : int;

		
			
		// 渠道ID
		private var _channel : String;

		
			
		// 账号
		private var _account : String;

		
			
		// 登录秘钥
		private var _key : String;

		
		public function CS_Login()
		{
		}
		
		public override function getSerializationID():int
		{
			return ProtoSerializationDefine.CS_LOGIN;
		}


			
		public function get serverId():int
		{
			return this._serverId;
		}
			
		public function set serverId(__serverId : int):void
		{
			SerializationHelper.getInstance().writeMask(__mask__, 0x1);
			this._serverId = __serverId;
		}

		
			
		public function get channel():String
		{
			return this._channel;
		}
			
		public function set channel(__channel : String):void
		{
			SerializationHelper.getInstance().writeMask(__mask__, 0x2);
			this._channel = __channel;
		}

		
			
		public function get account():String
		{
			return this._account;
		}
			
		public function set account(__account : String):void
		{
			SerializationHelper.getInstance().writeMask(__mask__, 0x3);
			this._account = __account;
		}

		
			
		public function get key():String
		{
			return this._key;
		}
			
		public function set key(__key : String):void
		{
			SerializationHelper.getInstance().writeMask(__mask__, 0x4);
			this._key = __key;
		}

		
		public override function toBytes(__targetBytes:IDataOutput=null):IDataOutput
		{
			SerializationHelper.getInstance().writeByteArray(__targetBytes, this.__mask__);

			
			if(SerializationHelper.getInstance().readMask(this.__mask__, 0x1) == 1)
				SerializationHelper.getInstance().writeU29Int(__targetBytes, this._serverId);

		
			
			if(SerializationHelper.getInstance().readMask(this.__mask__, 0x2) == 1)
				SerializationHelper.getInstance().writeUTF(__targetBytes, this._channel);

		
			
			if(SerializationHelper.getInstance().readMask(this.__mask__, 0x3) == 1)
				SerializationHelper.getInstance().writeUTF(__targetBytes, this._account);

		
			
			if(SerializationHelper.getInstance().readMask(this.__mask__, 0x4) == 1)
				SerializationHelper.getInstance().writeUTF(__targetBytes, this._key);

		
			return __targetBytes;
		}

		public override function fromBytes(__serializationBytes : IDataInput, __weight:IWeightData = null):void
		{
			this.__mask__ = SerializationHelper.getInstance().readByteArray(__serializationBytes);

			
			if(SerializationHelper.getInstance().readMask(this.__mask__, 0x1) == 1)
				this._serverId = SerializationHelper.getInstance().readU29Int(__serializationBytes) as int;

		
			
			if(SerializationHelper.getInstance().readMask(this.__mask__, 0x2) == 1)
				this._channel = SerializationHelper.getInstance().readUTF(__serializationBytes) as String;

		
			
			if(SerializationHelper.getInstance().readMask(this.__mask__, 0x3) == 1)
				this._account = SerializationHelper.getInstance().readUTF(__serializationBytes) as String;

		
			
			if(SerializationHelper.getInstance().readMask(this.__mask__, 0x4) == 1)
				this._key = SerializationHelper.getInstance().readUTF(__serializationBytes) as String;

		
		}
		
		override public function fireDataReciveEvent(sender:ICommunication, __session:Session):void
		{
			isser.issue("DataRecive", sender, __session, this); 
		}
	}
}

		