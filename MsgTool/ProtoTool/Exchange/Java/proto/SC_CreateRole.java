
// Generated by the SocketProtoGenerationTool.  DO NOT EDIT!
package com.mokylin.td.clientmsg.proto;

import io.netty.buffer.ByteBuf;
import java.util.List;
import java.io.UnsupportedEncodingException;
import javax.transaction.NotSupportedException;
import com.mokylin.bleach.core.collection.BitArray;
import com.mokylin.td.clientmsg.enumeration.*;
import com.mokylin.td.clientmsg.commondata.*;
import com.mokylin.td.clientmsg.core.ICommunicationDataBase;
import com.mokylin.td.clientmsg.core.SerializationHelper;
import com.mokylin.td.clientmsg.ProtoSerializationDefine;


//服务器通知，你当前没有角色，请创建角色，这是服务器帮你随机到的角色名，基本能够保证不重复
public class SC_CreateRole implements ICommunicationDataBase{	
    BitArray __mask__ = new BitArray(2);

	// 姓名，男
	private String _nameMale;
		
	// 姓名，女
	private String _nameFemale;
		
	public SC_CreateRole(){
	}
	
	public int getSerializationID(){
		return ProtoSerializationDefine.SC_CREATEROLE;
	}


	public String getnameMale() {
		return this._nameMale;
	}
	public void setnameMale(String __nameMale) {
      SerializationHelper.writeMask(__mask__, 0x1);
		this._nameMale = __nameMale;
	}
		
	public String getnameFemale() {
		return this._nameFemale;
	}
	public void setnameFemale(String __nameFemale) {
      SerializationHelper.writeMask(__mask__, 0x2);
		this._nameFemale = __nameFemale;
	}
		
	public ByteBuf toBytes(ByteBuf __targetBytes) throws UnsupportedEncodingException, NotSupportedException {
        SerializationHelper.writeBitArray(__targetBytes, this.__mask__);

		if(SerializationHelper.readMask(this.__mask__, 0x1) == true)
            SerializationHelper.writeUTF(__targetBytes, this._nameMale);
		
		if(SerializationHelper.readMask(this.__mask__, 0x2) == true)
            SerializationHelper.writeUTF(__targetBytes, this._nameFemale);
		
		return __targetBytes;
	}

	public void fromBytes(ByteBuf __serializationBytes) throws UnsupportedEncodingException, InstantiationException, IllegalAccessException, NotSupportedException {
        this.__mask__ = SerializationHelper.readBitArray(__serializationBytes);

		if(SerializationHelper.readMask(this.__mask__, 0x1) == true)
            this._nameMale = (String)SerializationHelper.readUTF(__serializationBytes);
		
		if(SerializationHelper.readMask(this.__mask__, 0x2) == true)
            this._nameFemale = (String)SerializationHelper.readUTF(__serializationBytes);
		
	}

}

		