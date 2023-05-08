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
		private static string _userImage;
		public static string USER_IMAGE
		{
			get
			{
				if (string.IsNullOrEmpty(_userImage))
				{
					return "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcScJXWdvwDZC0RF_VSzzP8aXSX9Sc_VPAtuew&usqp=CAU";
				}
					return _userImage;
			}
			set
			{
				_userImage = value;
			}
		}
	}
}
