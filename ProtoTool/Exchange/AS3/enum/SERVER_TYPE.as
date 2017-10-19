
			
package enum
{
	//
	public class SERVER_TYPE extends Enum
	{

			
		// LoginServer
		[Enum]
		public static const Login : int = 0;

		
			
		// AgentServer
		[Enum]
		public static const Agent : int = 1;

		
			
		// ConsoleServer
		[Enum]
		public static const Console : int = 2;

		
			
		// LogServer
		[Enum]
		public static const Log : int = 3;

		
			
		// RouteServer
		[Enum]
		public static const Route : int = 4;

		
			
		// HallServer
		[Enum]
		public static const Hall : int = 5;

		
			
		// RoomServer
		[Enum]
		public static const Room : int = 6;

		
			
		// BillServer
		[Enum]
		public static const Bill : int = 7;

		
			
		// AlertServer
		[Enum]
		public static const Alert : int = 8;

		
			
		// DBServer
		[Enum]
		public static const DB : int = 9;

		
			
		// DBRouteServer
		[Enum]
		public static const DBRoute : int = 10;

		
			
		// GMServer
		[Enum]
		public static const GM : int = 11;

		
		public function SERVER_TYPE()
		{
			super();
		}
	}
}

		