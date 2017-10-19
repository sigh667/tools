
			
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

	//服务器通知，角色列表
	public class SC_RoleList extends CommunicationDataBase
	{	
		private var __mask__:ByteArray = new ByteArray();
		public static var isser : Issuer = new Issuer();

			
		// 角色列表
		private var _roleInfos : Vector.<RoleProto>;

		
		public function SC_RoleList()
		{
		}
		
		public override function getSerializationID():int
		{
			return ProtoSerializationDefine.SC_ROLELIST;
		}


			
		public function get roleInfos():Vector.<RoleProto>
		{
			return this._roleInfos;
		}
			
		public function set roleInfos(__roleInfos : Vector.<RoleProto>):void
		{
			SerializationHelper.getInstance().writeMask(__mask__, 0x1);
			this._roleInfos = __roleInfos;
		}

		
		public override function toBytes(__targetBytes:IDataOutput=null):IDataOutput
		{
			SerializationHelper.getInstance().writeByteArray(__targetBytes, this.__mask__);

			
			if(SerializationHelper.getInstance().readMask(this.__mask__, 0x1) == 1)
				SerializationHelper.getInstance().customSerializationVector(__targetBytes, this._roleInfos);

		
			return __targetBytes;
		}

		public override function fromBytes(__serializationBytes : IDataInput, __weight:IWeightData = null):void
		{
			this.__mask__ = SerializationHelper.getInstance().readByteArray(__serializationBytes);

			
			if(SerializationHelper.getInstance().readMask(this.__mask__, 0x1) == 1)
				this._roleInfos = SerializationHelper.getInstance().customDeserializationVector(__serializationBytes, RoleProto) as Vector.<RoleProto>;

		
		}
		
		override public function fireDataReciveEvent(sender:ICommunication, __session:Session):void
		{
			isser.issue("DataRecive", sender, __session, this); 
		}
	}
}

		