
			
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

	//物品信息数据
	public class ItemInfo implements ISerializable
	{
		private var __mask__:ByteArray = new ByteArray();
		

			
		// 模板id
		private var _id : int;

		
			
		// 索引位置
		private var _index : int;

		
		public function ItemInfo()
		{
		}
		
		
			
		public function get id():int
		{
			return this._id;
		}
			
		public function set id(__id : int):void
		{
			SerializationHelper.getInstance().writeMask(__mask__, 0x1);
			this._id = __id;
		}

		
			
		public function get index():int
		{
			return this._index;
		}
			
		public function set index(__index : int):void
		{
			SerializationHelper.getInstance().writeMask(__mask__, 0x2);
			this._index = __index;
		}

		
		
		public function toBytes(__targetBytes:IDataOutput=null):IDataOutput
		{
			SerializationHelper.getInstance().writeByteArray(__targetBytes, this.__mask__);
			

			
			if(SerializationHelper.getInstance().readMask(this.__mask__, 0x1) == 1)
				SerializationHelper.getInstance().customSerialization(__targetBytes, this._id);

		
			
			if(SerializationHelper.getInstance().readMask(this.__mask__, 0x2) == 1)
				SerializationHelper.getInstance().customSerialization(__targetBytes, this._index);

		
			return __targetBytes;
		}

		public function fromBytes(__serializationBytes : IDataInput, __weight:IWeightData = null):void
		{
			this.__mask__ = SerializationHelper.getInstance().readByteArray(__serializationBytes);

			
			if(SerializationHelper.getInstance().readMask(this.__mask__, 0x1) == 1)
				this._id = SerializationHelper.getInstance().customDeserialization(__serializationBytes, int) as int;

		
			
			if(SerializationHelper.getInstance().readMask(this.__mask__, 0x2) == 1)
				this._index = SerializationHelper.getInstance().customDeserialization(__serializationBytes, int) as int;

		
		}
	}
}

		