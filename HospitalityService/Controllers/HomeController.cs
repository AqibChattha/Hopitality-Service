using HospitalityService.Data;
using HospitalityService.Models;
using HospitalityService.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace HospitalityService.Controllers
{
	public class HomeController : Controller
    {
        private readonly HopitalityAppDbContext _dbContext;

        public HomeController(HopitalityAppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IActionResult> Index()
		{
			if (GlobalVariables.IS_LOGGED_IN)
			{
				if (GlobalVariables.USER_TYPE.Equals(GlobalVariables.ADMIN))
                {
                    var UserHomeVM = new UserHomeVM
                    {
                        news = await _dbContext.AllNews.ToListAsync(),
                        tasks = await _dbContext.UserTasks.Where(o => o.UserId == GlobalVariables.USER_ID).ToListAsync(),
                        holidays = await _dbContext.Holidays.Where(o => (o.Id == GlobalVariables.USER_ID && o.Status == false) || GlobalVariables.IS_ADMIN_LOGGED_IN).ToListAsync()
                    };
                    return View(UserHomeVM);
				}
				else
                {
                    var UserHomeVM = new UserHomeVM
                    {
                        news = await _dbContext.AllNews.ToListAsync(),
                        tasks = await _dbContext.UserTasks.Where(o => o.UserId == GlobalVariables.USER_ID).ToListAsync(),
                        holidays = await _dbContext.Holidays.Where(o => o.UserId == GlobalVariables.USER_ID).ToListAsync()
                    };
                    return View(UserHomeVM);
                }
			}
			else
			{
				// Redirect to login page in AccountController folder
				return RedirectToAction("Login", "Account");
			}
		}

		public IActionResult Privacy()
		{
			return View();
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}