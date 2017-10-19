
			
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

	//背包信息数据
	public class PackInfo implements ISerializable
	{
		private var __mask__:ByteArray = new ByteArray();
		

			
		// 背包格子数
		private var _gridCnt : int;

		
			
		// 背包物品列表
		private var _items : Vector.<ItemInfo>;

		
		public function PackInfo()
		{
		}
		
		
			
		public function get gridCnt():int
		{
			return this._gridCnt;
		}
			
		public function set gridCnt(__gridCnt : int):void
		{
			SerializationHelper.getInstance().writeMask(__mask__, 0x1);
			this._gridCnt = __gridCnt;
		}

		
			
		public function get items():Vector.<ItemInfo>
		{
			return this._items;
		}
			
		public function set items(__items : Vector.<ItemInfo>):void
		{
			SerializationHelper.getInstance().writeMask(__mask__, 0x2);
			this._items = __items;
		}

		
		
		public function toBytes(__targetBytes:IDataOutput=null):IDataOutput
		{
			SerializationHelper.getInstance().writeByteArray(__targetBytes, this.__mask__);
			

			
			if(SerializationHelper.getInstance().readMask(this.__mask__, 0x1) == 1)
				SerializationHelper.getInstance().customSerialization(__targetBytes, this._gridCnt);

		
			
			if(SerializationHelper.getInstance().readMask(this.__mask__, 0x2) == 1)
				SerializationHelper.getInstance().customSerializationVector(__targetBytes, this._items);

		
			return __targetBytes;
		}

		public function fromBytes(__serializationBytes : IDataInput, __weight:IWeightData = null):void
		{
			this.__mask__ = SerializationHelper.getInstance().readByteArray(__serializationBytes);

			
			if(SerializationHelper.getInstance().readMask(this.__mask__, 0x1) == 1)
				this._gridCnt = SerializationHelper.getInstance().customDeserialization(__serializationBytes, int) as int;

		
			
			if(SerializationHelper.getInstance().readMask(this.__mask__, 0x2) == 1)
				this._items = SerializationHelper.getInstance().customDeserializationVector(__serializationBytes, ItemInfo) as Vector.<ItemInfo>;

		
		}
	}
}

		