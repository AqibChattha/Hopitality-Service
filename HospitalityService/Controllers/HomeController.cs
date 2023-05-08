using HospitalityService.Data;
using HospitalityService.Models;
using HospitalityService.Models.Domain;
using HospitalityService.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.VisualBasic.Syntax;
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
		
		public async Task<IActionResult> Index(string searchedText)
		{
			if (string.IsNullOrWhiteSpace(searchedText))
			{
				searchedText = "";
			}
			if (GlobalVariables.IS_LOGGED_IN)
			{
				List<News> news = await _dbContext.AllNews.ToListAsync();
				List<HolidayRequest> holidayRequests = new List<HolidayRequest>();
				foreach (var item in news)
				{
					var user = await _dbContext.Users.FindAsync(item.UserId);
					if (user != null)
					{
						item.Title = "(" + user.Name + ") : " + item.Title;
					}
				}
				news = news.Where(o => o.Title.Contains(searchedText) || o.Description.Contains(searchedText) ||
				o.Date.ToString().Contains(searchedText)).ToList();

				if (GlobalVariables.IS_ADMIN_LOGGED_IN)
				{
					holidayRequests = await _dbContext.Holidays.ToListAsync();
					foreach (var item in holidayRequests)
					{
						var user = await _dbContext.Users.FindAsync(item.UserId);
						if (user != null)
						{
							item.Title = "(" + user.Name + ") : " + item.Title;
						}
					}
					holidayRequests = holidayRequests.Where(o => o.Title.Contains(searchedText) || o.Description.Contains(searchedText) ||
				o.Date.ToString().Contains(searchedText)).ToList();
				}
				else
				{
					holidayRequests = await _dbContext.Holidays.Where(a => a.UserId == GlobalVariables.USER_ID && (a.Title.Contains(searchedText) || a.Description.Contains(searchedText) || a.Date.ToString().Contains(searchedText))).ToListAsync();
				}
				var UserHomeVM = new UserHomeVM
				{
					news = news,
					tasks = await _dbContext.UserTasks.Where(o => o.UserId == GlobalVariables.USER_ID && (o.Title.Contains(searchedText) || o.Description.Contains(searchedText) || o.Deadline.ToString().Contains(searchedText))).ToListAsync(),
					holidays = holidayRequests
				};
				return View(UserHomeVM);
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