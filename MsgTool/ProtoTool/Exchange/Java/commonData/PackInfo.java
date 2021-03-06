
// Generated by the SocketProtoGenerationTool.  DO NOT EDIT!
package com.mokylin.td.clientmsg.commondata;

import io.netty.buffer.ByteBuf;
import java.util.List;
import java.io.UnsupportedEncodingException;
import javax.transaction.NotSupportedException;
import com.mokylin.bleach.core.collection.BitArray;
import com.mokylin.td.clientmsg.core.ISerializationDataBase;
import com.mokylin.td.clientmsg.enumeration.*;
import com.mokylin.td.clientmsg.core.SerializationHelper;
	
//背包信息数据
public class PackInfo implements ISerializationDataBase{
    BitArray __mask__ = new BitArray(2);

	// 背包格子数
	private int _gridCnt;
		
	// 背包物品列表
	private List<ItemInfo> _items;
		

	public int getgridCnt() {
		return this._gridCnt;
	}
	public void setgridCnt(int __gridCnt) {
      SerializationHelper.writeMask(__mask__, 0x1);
		this._gridCnt = __gridCnt;
	}
		
	public List<ItemInfo> getitems() {
		return this._items;
	}
	public void setitems(List<ItemInfo> __items) {
      SerializationHelper.writeMask(__mask__, 0x2);
		this._items = __items;
	}
		
	public ByteBuf toBytes(ByteBuf __targetBytes) throws UnsupportedEncodingException, NotSupportedException {
        SerializationHelper.writeBitArray(__targetBytes, this.__mask__);

		if(SerializationHelper.readMask(this.__mask__, 0x1) == true)
            SerializationHelper.customSerialization(__targetBytes, this._gridCnt);
		
		if(SerializationHelper.readMask(this.__mask__, 0x2) == true)
            SerializationHelper.customSerializationVector(__targetBytes, this._items);
		
		return __targetBytes;
	}

	public void fromBytes(ByteBuf __serializationBytes) throws UnsupportedEncodingException, InstantiationException, IllegalAccessException, NotSupportedException {
        this.__mask__ = SerializationHelper.readBitArray(__serializationBytes);

		if(SerializationHelper.readMask(this.__mask__, 0x1) == true)
            this._gridCnt = (int)SerializationHelper.customDeserialization(__serializationBytes, int.class);
		
		if(SerializationHelper.readMask(this.__mask__, 0x2) == true)
            this._items = (List<ItemInfo>)SerializationHelper.customDeserializationVector(__serializationBytes, ItemInfo.class);
		
	}
}
    