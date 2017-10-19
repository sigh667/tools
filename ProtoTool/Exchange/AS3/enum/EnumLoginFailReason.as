
			
package enum
{
	//
	public class EnumLoginFailReason extends Enum
	{

			
		// 版本与服务器不一致
		[Enum]
		public static const VERSION_ERROR : int = 0;

		
			
		// 渠道不存在
		[Enum]
		public static const CHANNEL_NOT_EXIST : int = 1;

		
			
		// 账号包含不合法字符
		[Enum]
		public static const ACCOUNT_NOT_ALLOW : int = 2;

		
			
		// 服务器尚未开放
		[Enum]
		public static const NOT_OPEN : int = 3;

		
			
		// 服务器人数已满
		[Enum]
		public static const FULL : int = 4;

		
			
		// 账号未激活
		[Enum]
		public static const ACCOUNT_NOT_ACTIVE : int = 5;

		
			
		// 验证失败
		[Enum]
		public static const AUTH_ERROR : int = 6;

		
			
		// 加载角色失败
		[Enum]
		public static const LOAD_ROLE_FAIL : int = 7;

		
		public function EnumLoginFailReason()
		{
			super();
		}
	}
}

		