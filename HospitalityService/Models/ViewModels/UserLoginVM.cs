using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace HospitalityService.Models.ViewModels
{
	public class UserLoginVM
	{
		
		public string Email { get; set; }

		public string Password { get; set; }
	}
}
