using HospitalityService.Data;
using HospitalityService.Models.Domain;
using HospitalityService.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Drawing;

namespace HospitalityService.Controllers
{
	public class AccountController : Controller
	{
		private readonly HopitalityAppDbContext _dbContext;

		public AccountController(HopitalityAppDbContext dbContext)
		{
			_dbContext = dbContext;
		}

		[HttpGet]
		public IActionResult LogOut()
		{
			GlobalVariables.IS_LOGGED_IN = false;
			return RedirectToAction("Login", "Account");
        }

        [HttpGet]
        public async Task<IActionResult> Profile()
        {
            var existingUser = await _dbContext.Users.FirstOrDefaultAsync(o => o.Id == GlobalVariables.USER_ID);
            if (GlobalVariables.IS_LOGGED_IN && existingUser != null)
            {
                var user = new UserProfileVm
                {
                    Id = existingUser.Id,
                    Name = existingUser.Name,
                    Email = existingUser.Email,
                    UserType = existingUser.UserType,
                    ImgURL = existingUser.ImgURL
				};
                return View(user);
            }
            return RedirectToAction("Login", "Account");
        }

		//[HttpPost]
		//[ValidateAntiForgeryToken]
		//public async Task<IActionResult> Profile(User user)
		//{
		//	if (ModelState.IsValid)
		//	{
  //              var existingUser = await _dbContext.Users.FirstOrDefaultAsync(o => o.Id == user.Id);

		//		// Update the user's information with the edited values
		//		existingUser.Name = user.Name;
		//		existingUser.Email = user.Email;
		//		existingUser.Password = user.Password;

		//		if (user.UserImageData != null && user.UserImageData.Count > 0)
		//		{
		//			using (var reader = new System.IO.BinaryReader(UserImageData.InputStream))
		//			{
		//				existingUser.UserImageData = reader.ReadBytes(UserImageData.ContentLength);
		//			}
		//		}

  //              await _dbContext.Update();
  //              await _dbContext.SaveChangesAsync();

  //              // Redirect the
  //          }
		//}


        [HttpGet]
		public IActionResult Login()
		{
			UserLoginVM _model = new UserLoginVM();
			return View(_model);
		}

		[HttpPost]
		public async Task<IActionResult> Login(UserLoginVM _model)
		{
			var find = await _dbContext.Users.FirstOrDefaultAsync(o => o.Email == _model.Email && o.Password == _model.Password);
			if (find != null)
			{
				GlobalVariables.IS_LOGGED_IN = true;
				GlobalVariables.IS_ADMIN_LOGGED_IN = find.UserType.Equals(GlobalVariables.ADMIN);
				GlobalVariables.USER_ID = find.Id;
				GlobalVariables.USER_NAME = find.Name;
				GlobalVariables.USER_TYPE = find.UserType;
				GlobalVariables.USER_IMAGE = find.ImgURL;
				return RedirectToAction("Index", "Home");
			} 
				TempData["ErrorMessage"] = "Incorrect email or password.";
				return View(_model);
		}
	}
}
