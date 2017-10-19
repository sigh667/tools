#ifndef __PROTOCOL_ENUMS__
#define __PROTOCOL_ENUMS__


namespace CommonData
{
	//
	enum SERVER_TYPE
	{

		// LoginServer
		Login = 0, 
		
		// AgentServer
		Agent = 1, 
		
		// ConsoleServer
		Console = 2, 
		
		// LogServer
		Log = 3, 
		
		// RouteServer
		Route = 4, 
		
		// HallServer
		Hall = 5, 
		
		// RoomServer
		Room = 6, 
		
		// BillServer
		Bill = 7, 
		
		// AlertServer
		Alert = 8, 
		
		// DBServer
		DB = 9, 
		
		// DBRouteServer
		DBRoute = 10, 
		
		// GMServer
		GM = 11

	}
}

		

namespace CommonData
{
	//
	enum EnumLoginFailReason
	{

		// 版本与服务器不一致
		VERSION_ERROR = 0, 
		
		// 渠道不存在
		CHANNEL_NOT_EXIST = 1, 
		
		// 账号包含不合法字符
		ACCOUNT_NOT_ALLOW = 2, 
		
		// 服务器尚未开放
		NOT_OPEN = 3, 
		
		// 服务器人数已满
		FULL = 4, 
		
		// 账号未激活
		ACCOUNT_NOT_ACTIVE = 5, 
		
		// 验证失败
		AUTH_ERROR = 6, 
		
		// 加载角色失败
		LOAD_ROLE_FAIL = 7

	}
}

		

namespace CommonData
{
	//
	enum EnumCreateRoleFailReason
	{

		// 名字已被占用
		NAME_SAME = 0, 
		
		// 名字包含不合法字符
		NAME_ILLEGAL_CHAR = 1, 
		
		// 名字太长
		NAME_TOO_LONG = 2, 
		
		// 名字太短
		NAME_TOO_SHORT = 3, 
		
		// 该体型尚未开放
		BODY_NOT_OPEN = 4, 
		
		// 该门派尚未开放
		MENPAI_NOT_OPEN = 5

	}
}

		

namespace CommonData
{
	//
	enum EnumBody
	{

		// 成年男
		MALE_ADULT = 0, 
		
		// 成年女
		FEMALE_ADULT = 1

	}
}

		

namespace CommonData
{
	//
	enum EnumSex
	{

		// 无性别
		NONE = 0, 
		
		// 男
		MALE = 1, 
		
		// 女
		FEMALE = 2

	}
}

		

namespace CommonData
{
	//
	enum EnumMenPai
	{

		// 无职业
		NONE = 0, 
		
		// 真武
		ZHEN_WU = 1, 
		
		// 丐帮
		GAI_BANG = 2, 
		
		// 神威
		SHEN_WEI = 3, 
		
		// 太白
		TAI_BAI = 4, 
		
		// 唐门
		TANG_MEN = 5, 
		
		// 天香
		TIAN_XIANG = 6, 
		
		// 五毒
		WU_DU = 7, 
		
		// 神刀
		SHEN_DAO = 8

	}
}

		
#endif
