
			
package commonData
{
	import flash.utils.ByteArray;
	import flash.utils.IDataInput;
	import flash.utils.IDataOutput;
	
	import commonData.*;
	import enum.*;
	import navigate.atom.define.ISerializable;	
	import navigate.helper.serialization.SerializationHelper;
	import navigate.atom.define.IWeightData;

	//角色信息数据
	public class RoleProto implements ISerializable
	{
		private var __mask__:ByteArray = new ByteArray();
		

			
		// 名字
		private var _name : String;

		
			
		// 性别
		private var _sex : EnumSex;

		
			
		// 门派
		private var _menpai : EnumMenPai;

		
			
		// 捏脸数据
		private var _avatarFace : ByteArray;

		
			
		// 外观数据，除捏脸以外的
		private var _avatar : ByteArray;

		
		public function RoleProto()
		{
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

		
			
		public function get sex():EnumSex
		{
			return this._sex;
		}
			
		public function set sex(__sex : EnumSex):void
		{
			SerializationHelper.getInstance().writeMask(__mask__, 0x2);
			this._sex = __sex;
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

		
			
		public function get avatar():ByteArray
		{
			return this._avatar;
		}
			
		public function set avatar(__avatar : ByteArray):void
		{
			SerializationHelper.getInstance().writeMask(__mask__, 0x5);
			this._avatar = __avatar;
		}

		
		
		public function toBytes(__targetBytes:IDataOutput=null):IDataOutput
		{
			SerializationHelper.getInstance().writeByteArray(__targetBytes, this.__mask__);
			

			
			if(SerializationHelper.getInstance().readMask(this.__mask__, 0x1) == 1)
				SerializationHelper.getInstance().writeUTF(__targetBytes, this._name);

		
			
			if(SerializationHelper.getInstance().readMask(this.__mask__, 0x2) == 1)
				SerializationHelper.getInstance().writeEnum(__targetBytes, this._sex.value);

		
			
			if(SerializationHelper.getInstance().readMask(this.__mask__, 0x3) == 1)
				SerializationHelper.getInstance().writeEnum(__targetBytes, this._menpai.value);

		
			
			if(SerializationHelper.getInstance().readMask(this.__mask__, 0x4) == 1)
				SerializationHelper.getInstance().writeByteArray(__targetBytes, this._avatarFace);

		
			
			if(SerializationHelper.getInstance().readMask(this.__mask__, 0x5) == 1)
				SerializationHelper.getInstance().writeByteArray(__targetBytes, this._avatar);

		
			return __targetBytes;
		}

		public function fromBytes(__serializationBytes : IDataInput, __weight:IWeightData = null):void
		{
			this.__mask__ = SerializationHelper.getInstance().readByteArray(__serializationBytes);

			
			if(SerializationHelper.getInstance().readMask(this.__mask__, 0x1) == 1)
				this._name = SerializationHelper.getInstance().readUTF(__serializationBytes) as String;

		
			
			if(SerializationHelper.getInstance().readMask(this.__mask__, 0x2) == 1)
				this._sex = SerializationHelper.getInstance().readEnum(__serializationBytes, EnumSex) as EnumSex;

		
			
			if(SerializationHelper.getInstance().readMask(this.__mask__, 0x3) == 1)
				this._menpai = SerializationHelper.getInstance().readEnum(__serializationBytes, EnumMenPai) as EnumMenPai;

		
			
			if(SerializationHelper.getInstance().readMask(this.__mask__, 0x4) == 1)
				this._avatarFace = SerializationHelper.getInstance().readByteArray(__serializationBytes) as ByteArray;

		
			
			if(SerializationHelper.getInstance().readMask(this.__mask__, 0x5) == 1)
				this._avatar = SerializationHelper.getInstance().readByteArray(__serializationBytes) as ByteArray;

		
		}
	}
}

		