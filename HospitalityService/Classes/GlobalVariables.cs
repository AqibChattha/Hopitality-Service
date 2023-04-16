using System.Drawing;

namespace HospitalityService
{
	public class GlobalVariables
	{
		public static bool IS_LOGGED_IN = false;
		public static bool IS_ADMIN_LOGGED_IN = false;

		public static string ADMIN = "ADMIN";
		public static string CONNECTION_STRING = @"Data Source=(local);Initial Catalog=HospitalityService;Integrated Security=True";


		public static string MAIL = "#";
		
		public static string USER_NAME;
		public static Guid USER_ID;
		public static string USER_TYPE;
		public static string USER_IMAGE;
	}
}
