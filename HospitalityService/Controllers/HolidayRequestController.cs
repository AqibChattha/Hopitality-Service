using HospitalityService.Data;
using HospitalityService.Models.Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HospitalityService.Controllers
{
    public class HolidayRequestController : Controller
    {
        private readonly HopitalityAppDbContext _dbContext;

        public HolidayRequestController(HopitalityAppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        // GET: HolidayRequestController
        public ActionResult Index()
        {
            if (GlobalVariables.IS_LOGGED_IN)
            {
                var userlist = _dbContext.Holidays.Where(o=>o.UserId.Equals(GlobalVariables.USER_ID) ||
                GlobalVariables.IS_ADMIN_LOGGED_IN).ToList();
                return View(userlist);
            }
            return RedirectToAction("Login", "Account");
        }

        // GET: HolidayRequestController/Create
        public ActionResult Create()
        {
            if (GlobalVariables.IS_LOGGED_IN)
            {
                return View();
            }
            return RedirectToAction("Login", "Account");
        }

        // POST: HolidayRequestController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            if (GlobalVariables.IS_LOGGED_IN)
            {
                try
                {
                    if (!String.IsNullOrEmpty(collection["Title"]) &&
                        !String.IsNullOrEmpty(collection["Description"]))
                    {
                        HolidayRequest holiday = new()
                        {
                            Id = Guid.NewGuid(),
                            Title = collection["Title"],
                            Description = collection["Description"],
                            Date = String.IsNullOrEmpty(collection["Date"]) ? DateTime.Now : Convert.ToDateTime(collection["Date"]),
                            Status = false,
                            IsAccepted = false,
                            UserId = GlobalVariables.USER_ID
                        };
                        _dbContext.Holidays.Add(holiday);
                        _dbContext.SaveChanges();
                        return RedirectToAction(nameof(Index));
                    }
                    TempData["ErrorMessage"] = "Please fill all the text boxes.";
                }
                catch
                {
                }
                return View();
            }
            return RedirectToAction("Login", "Account");
        }

        // GET: HolidayRequestController/Edit/5
        public ActionResult Edit(Guid id)
        {
            if (GlobalVariables.IS_LOGGED_IN)
            {
                var find = _dbContext.Holidays.Where(o => o.Id.Equals(id)).FirstOrDefault();
                return View(find);
            }
            return RedirectToAction("Login", "Account");
        }

        // POST: HolidayRequestController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Guid id, IFormCollection collection)
        {
            if (GlobalVariables.IS_LOGGED_IN)
            {
                try
                {
                    var find = _dbContext.Holidays.Where(o => o.Id.Equals(id)).FirstOrDefault();
                    if (find != null)
                    {
                        find.Title = collection["Title"].ToString();
                        find.Description = collection["Description"].ToString();
                        find.Date = Convert.ToDateTime(collection["Date"]);
                        find.UserId = GlobalVariables.USER_ID;
                        find.Status = Convert.ToBoolean(collection["Status"][0]);
                        find.IsAccepted = Convert.ToBoolean(collection["IsAccepted"][0]);
                        _dbContext.Holidays.Update(find);
                        _dbContext.SaveChanges();
                    }
                    return RedirectToAction(nameof(Index));
                }
                catch
                {
                    return View();
                }
            }
            return RedirectToAction("Login", "Account");
        }

        // GET: HolidayRequestController/Delete/5
        public ActionResult Delete(Guid id)
        {
            if (GlobalVariables.IS_LOGGED_IN)
            {
                var find = _dbContext.Holidays.Where(o => o.Id.Equals(id)).FirstOrDefault();
                return View(find);
            }
            return RedirectToAction("Login", "Account");
        }

        // POST: HolidayRequestController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(Guid id, IFormCollection collection)
        {
            if (GlobalVariables.IS_LOGGED_IN)
            {
                try
                {
                    var find = _dbContext.Holidays.Where(o => o.Id.Equals(id)).FirstOrDefault();
                    if (find != null)
                    {
                        _dbContext.Holidays.Remove(find);
                        _dbContext.SaveChanges();
                    }
                    return RedirectToAction(nameof(Index));
                }
                catch
                {
                    return View();
                }
            }
            return RedirectToAction("Login", "Account");
        }
    }
}
