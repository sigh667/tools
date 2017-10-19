
			
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

	//Client请求创建角色，并进入游戏
	public class CS_CreateRole extends CommunicationDataBase
	{	
		private var __mask__:ByteArray = new ByteArray();
		public static var isser : Issuer = new Issuer();

			
		// 名字
		private var _name : String;

		
			
		// 体型
		private var _body : EnumBody;

		
			
		// 门派
		private var _menpai : EnumMenPai;

		
			
		// 捏脸数据
		private var _avatarFace : ByteArray;

		
		public function CS_CreateRole()
		{
		}
		
		public override function getSerializationID():int
		{
			return ProtoSerializationDefine.CS_CREATEROLE;
		}


			
		public function get name():String
		{
			return this._name;
		}
			
		public function set name(__name : String):void
		{
			SerializationHelper.getInstance().writeMask(__mask__, 0x1);
			this._name = __name;
		}

		
			
		public function get body():EnumBody
		{
			return this._body;
		}
			
		public function set body(__body : EnumBody):void
		{
			SerializationHelper.getInstance().writeMask(__mask__, 0x2);
			this._body = __body;
		}

		
			
		public function get menpai():EnumMenPai
		{
			return this._menpai;
		}
			
		public function set menpai(__menpai : EnumMenPai):void
		{
			SerializationHelper.getInstance().writeMask(__mask__, 0x3);
			this._menpai = __menpai;
		}

		
			
		public function get avatarFace():ByteArray
		{
			return this._avatarFace;
		}
			
		public function set avatarFace(__avatarFace : ByteArray):void
		{
			SerializationHelper.getInstance().writeMask(__mask__, 0x4);
			this._avatarFace = __avatarFace;
		}

		
		public override function toBytes(__targetBytes:IDataOutput=null):IDataOutput
		{
			SerializationHelper.getInstance().writeByteArray(__targetBytes, this.__mask__);

			
			if(SerializationHelper.getInstance().readMask(this.__mask__, 0x1) == 1)
				SerializationHelper.getInstance().writeUTF(__targetBytes, this._name);

		
			
			if(SerializationHelper.getInstance().readMask(this.__mask__, 0x2) == 1)
				SerializationHelper.getInstance().writeEnum(__targetBytes, this._body.value);

		
			
			if(SerializationHelper.getInstance().readMask(this.__mask__, 0x3) == 1)
				SerializationHelper.getInstance().writeEnum(__targetBytes, this._menpai.value);

		
			
			if(SerializationHelper.getInstance().readMask(this.__mask__, 0x4) == 1)
				SerializationHelper.getInstance().writeByteArray(__targetBytes, this._avatarFace);

		
			return __targetBytes;
		}

		public override function fromBytes(__serializationBytes : IDataInput, __weight:IWeightData = null):void
		{
			this.__mask__ = SerializationHelper.getInstance().readByteArray(__serializationBytes);

			
			if(SerializationHelper.getInstance().readMask(this.__mask__, 0x1) == 1)
				this._name = SerializationHelper.getInstance().readUTF(__serializationBytes) as String;

		
			
			if(SerializationHelper.getInstance().readMask(this.__mask__, 0x2) == 1)
				this._body = SerializationHelper.getInstance().readEnum(__serializationBytes, EnumBody) as EnumBody;

		
			
			if(SerializationHelper.getInstance().readMask(this.__mask__, 0x3) == 1)
				this._menpai = SerializationHelper.getInstance().readEnum(__serializationBytes, EnumMenPai) as EnumMenPai;

		
			
			if(SerializationHelper.getInstance().readMask(this.__mask__, 0x4) == 1)
				this._avatarFace = SerializationHelper.getInstance().readByteArray(__serializationBytes) as ByteArray;

		
		}
		
		override public function fireDataReciveEvent(sender:ICommunication, __session:Session):void
		{
			isser.issue("DataRecive", sender, __session, this); 
		}
	}
}

		