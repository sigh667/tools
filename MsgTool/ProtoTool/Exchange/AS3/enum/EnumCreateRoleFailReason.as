
			
package enum
{
	//
	public class EnumCreateRoleFailReason extends Enum
	{

			
		// 名字已被占用
		[Enum]
		public static const NAME_SAME : int = 0;

		
			
		// 名字包含不合法字符
		[Enum]
		public static const NAME_ILLEGAL_CHAR : int = 1;

		
			
		// 名字太长
		[Enum]
		public static const NAME_TOO_LONG : int = 2;

		
			
		// 名字太短
		[Enum]
		public static const NAME_TOO_SHORT : int = 3;

		
			
		// 该体型尚未开放
		[Enum]
		public static const BODY_NOT_OPEN : int = 4;

		
			
		// 该门派尚未开放
		[Enum]
		public static const MENPAI_NOT_OPEN : int = 5;

		
		public function EnumCreateRoleFailReason()
		{
			super();
		}
	}
}

		