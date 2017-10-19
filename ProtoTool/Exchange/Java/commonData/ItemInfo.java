
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
	
//物品信息数据
public class ItemInfo implements ISerializationDataBase{
    BitArray __mask__ = new BitArray(2);

	// 模板id
	private int _id;
		
	// 索引位置
	private int _index;
		

	public int getid() {
		return this._id;
	}
	public void setid(int __id) {
      SerializationHelper.writeMask(__mask__, 0x1);
		this._id = __id;
	}
		
	public int getindex() {
		return this._index;
	}
	public void setindex(int __index) {
      SerializationHelper.writeMask(__mask__, 0x2);
		this._index = __index;
	}
		
	public ByteBuf toBytes(ByteBuf __targetBytes) throws UnsupportedEncodingException, NotSupportedException {
        SerializationHelper.writeBitArray(__targetBytes, this.__mask__);

		if(SerializationHelper.readMask(this.__mask__, 0x1) == true)
            SerializationHelper.customSerialization(__targetBytes, this._id);
		
		if(SerializationHelper.readMask(this.__mask__, 0x2) == true)
            SerializationHelper.customSerialization(__targetBytes, this._index);
		
		return __targetBytes;
	}

	public void fromBytes(ByteBuf __serializationBytes) throws UnsupportedEncodingException, InstantiationException, IllegalAccessException, NotSupportedException {
        this.__mask__ = SerializationHelper.readBitArray(__serializationBytes);

		if(SerializationHelper.readMask(this.__mask__, 0x1) == true)
            this._id = (int)SerializationHelper.customDeserialization(__serializationBytes, int.class);
		
		if(SerializationHelper.readMask(this.__mask__, 0x2) == true)
            this._index = (int)SerializationHelper.customDeserialization(__serializationBytes, int.class);
		
	}
}
    