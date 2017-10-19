
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
	
//角色信息数据
public class RoleProto implements ISerializationDataBase{
    BitArray __mask__ = new BitArray(5);

	// 名字
	private String _name;
		
	// 性别
	private EnumSex _sex;
		
	// 门派
	private EnumMenPai _menpai;
		
	// 捏脸数据
	private ByteBuf _avatarFace;
		
	// 外观数据，除捏脸以外的
	private ByteBuf _avatar;
		

	public String getname() {
		return this._name;
	}
	public void setname(String __name) {
      SerializationHelper.writeMask(__mask__, 0x1);
		this._name = __name;
	}
		
	public EnumSex getsex() {
		return this._sex;
	}
	public void setsex(EnumSex __sex) {
      SerializationHelper.writeMask(__mask__, 0x2);
		this._sex = __sex;
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
		
	public ByteBuf getavatar() {
		return this._avatar;
	}
	public void setavatar(ByteBuf __avatar) {
      SerializationHelper.writeMask(__mask__, 0x5);
		this._avatar = __avatar;
	}
		
	public ByteBuf toBytes(ByteBuf __targetBytes) throws UnsupportedEncodingException, NotSupportedException {
        SerializationHelper.writeBitArray(__targetBytes, this.__mask__);

		if(SerializationHelper.readMask(this.__mask__, 0x1) == true)
            SerializationHelper.writeUTF(__targetBytes, this._name);
		
		if(SerializationHelper.readMask(this.__mask__, 0x2) == true)
            SerializationHelper.writeU29Int(__targetBytes, this._sex.getValue());
		
		if(SerializationHelper.readMask(this.__mask__, 0x3) == true)
            SerializationHelper.writeU29Int(__targetBytes, this._menpai.getValue());
		
		if(SerializationHelper.readMask(this.__mask__, 0x4) == true)
            SerializationHelper.writeByteArray(__targetBytes, this._avatarFace);
		
		if(SerializationHelper.readMask(this.__mask__, 0x5) == true)
            SerializationHelper.writeByteArray(__targetBytes, this._avatar);
		
		return __targetBytes;
	}

	public void fromBytes(ByteBuf __serializationBytes) throws UnsupportedEncodingException, InstantiationException, IllegalAccessException, NotSupportedException {
        this.__mask__ = SerializationHelper.readBitArray(__serializationBytes);

		if(SerializationHelper.readMask(this.__mask__, 0x1) == true)
            this._name = (String)SerializationHelper.readUTF(__serializationBytes);
		
		if(SerializationHelper.readMask(this.__mask__, 0x2) == true)
            this._sex = EnumSex.valueOf(SerializationHelper.readU29Int(__serializationBytes));
		
		if(SerializationHelper.readMask(this.__mask__, 0x3) == true)
            this._menpai = EnumMenPai.valueOf(SerializationHelper.readU29Int(__serializationBytes));
		
		if(SerializationHelper.readMask(this.__mask__, 0x4) == true)
            this._avatarFace = (ByteBuf)SerializationHelper.readByteArray(__serializationBytes);
		
		if(SerializationHelper.readMask(this.__mask__, 0x5) == true)
            this._avatar = (ByteBuf)SerializationHelper.readByteArray(__serializationBytes);
		
	}
}
    