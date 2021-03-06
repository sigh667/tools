
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


//Client请求创建角色，并进入游戏
public class CS_CreateRole implements ICommunicationDataBase{	
    BitArray __mask__ = new BitArray(4);

	// 名字
	private String _name;
		
	// 体型
	private EnumBody _body;
		
	// 门派
	private EnumMenPai _menpai;
		
	// 捏脸数据
	private ByteBuf _avatarFace;
		
	public CS_CreateRole(){
	}
	
	public int getSerializationID(){
		return ProtoSerializationDefine.CS_CREATEROLE;
	}


	public String getname() {
		return this._name;
	}
	public void setname(String __name) {
      SerializationHelper.writeMask(__mask__, 0x1);
		this._name = __name;
	}
		
	public EnumBody getbody() {
		return this._body;
	}
	public void setbody(EnumBody __body) {
      SerializationHelper.writeMask(__mask__, 0x2);
		this._body = __body;
	}
		
	public EnumMenPai getmenpai() {
		return this._menpai;
	}
	public void setmenpai(EnumMenPai __menpai) {
      SerializationHelper.writeMask(__mask__, 0x3);
		this._menpai = __menpai;
	}
		
	public ByteBuf getavatarFace() {
		return this._avatarFace;
	}
	public void setavatarFace(ByteBuf __avatarFace) {
      SerializationHelper.writeMask(__mask__, 0x4);
		this._avatarFace = __avatarFace;
	}
		
	public ByteBuf toBytes(ByteBuf __targetBytes) throws UnsupportedEncodingException, NotSupportedException {
        SerializationHelper.writeBitArray(__targetBytes, this.__mask__);

		if(SerializationHelper.readMask(this.__mask__, 0x1) == true)
            SerializationHelper.writeUTF(__targetBytes, this._name);
		
		if(SerializationHelper.readMask(this.__mask__, 0x2) == true)
            SerializationHelper.writeU29Int(__targetBytes, this._body.getValue());
		
		if(SerializationHelper.readMask(this.__mask__, 0x3) == true)
            SerializationHelper.writeU29Int(__targetBytes, this._menpai.getValue());
		
		if(SerializationHelper.readMask(this.__mask__, 0x4) == true)
            SerializationHelper.writeByteArray(__targetBytes, this._avatarFace);
		
		return __targetBytes;
	}

	public void fromBytes(ByteBuf __serializationBytes) throws UnsupportedEncodingException, InstantiationException, IllegalAccessException, NotSupportedException {
        this.__mask__ = SerializationHelper.readBitArray(__serializationBytes);

		if(SerializationHelper.readMask(this.__mask__, 0x1) == true)
            this._name = (String)SerializationHelper.readUTF(__serializationBytes);
		
		if(SerializationHelper.readMask(this.__mask__, 0x2) == true)
            this._body = EnumBody.valueOf(SerializationHelper.readU29Int(__serializationBytes));
		
		if(SerializationHelper.readMask(this.__mask__, 0x3) == true)
            this._menpai = EnumMenPai.valueOf(SerializationHelper.readU29Int(__serializationBytes));
		
		if(SerializationHelper.readMask(this.__mask__, 0x4) == true)
            this._avatarFace = (ByteBuf)SerializationHelper.readByteArray(__serializationBytes);
		
	}

}

		