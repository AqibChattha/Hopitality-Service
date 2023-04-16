using HospitalityService.Data;
using HospitalityService.Models.Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HospitalityService.Controllers
{
    public class NewsController : Controller
    {
        private readonly HopitalityAppDbContext _dbContext;

        public NewsController(HopitalityAppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        
        // GET: NewsController
        public IActionResult Index()
        {
            if (GlobalVariables.IS_LOGGED_IN)
            {
                var userlist = _dbContext.AllNews.ToList();
                return View(userlist);
            }
            return RedirectToAction("Login", "Account");
        }

        // GET: NewsController/Create
        public IActionResult Create()
        {
            if (GlobalVariables.IS_LOGGED_IN)
            {
                return View();
            }
            return RedirectToAction("Login", "Account");
        }

        // POST: NewsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(IFormCollection collection)
        {
            if (GlobalVariables.IS_LOGGED_IN)
            {
                try
                {
                    if (!String.IsNullOrEmpty(collection["Title"]) &&
                        !String.IsNullOrEmpty(collection["Description"]))
                    {
                        News news = new()
                        {
                            Id = Guid.NewGuid(),
                            Title = collection["Title"],
                            Description = collection["Description"],
                            Date = String.IsNullOrEmpty(collection["Date"]) ? DateTime.Now : Convert.ToDateTime(collection["Date"]),
                        UserId = GlobalVariables.USER_ID
                        };
                        _dbContext.AllNews.Add(news);
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

        // GET: NewsController/Edit/5
        public IActionResult Edit(Guid id)
        {
            if (GlobalVariables.IS_LOGGED_IN)
            {
                var find = _dbContext.AllNews.Where(o => o.Id.Equals(id)).FirstOrDefault();
                return View(find);
            }
            return RedirectToAction("Login", "Account");
        }

        // POST: NewsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Guid id, IFormCollection collection)
        {
            if (GlobalVariables.IS_LOGGED_IN)
            {
                try
                {
                    var find = _dbContext.AllNews.Where(o => o.Id.Equals(id)).FirstOrDefault();
                    if (find != null)
                    {
                        find.Title = collection["Title"].ToString();
                        find.Description = collection["Description"].ToString();
                        find.Date = Convert.ToDateTime(collection["Date"]);
                        find.UserId = GlobalVariables.USER_ID;
                        _dbContext.Update(find);
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

        // GET: NewsController/Delete/5
        public IActionResult Delete(Guid id)
        {
            if (GlobalVariables.IS_LOGGED_IN)
            {
                var find = _dbContext.AllNews.Where(o => o.Id.Equals(id)).FirstOrDefault();
                return View(find);
            }
            return RedirectToAction("Login", "Account");
        }

        // POST: NewsController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(Guid id, IFormCollection collection)
        {
            if (GlobalVariables.IS_LOGGED_IN)
            {
                try
                {
                    var find = _dbContext.AllNews.Where(o => o.Id.Equals(id)).FirstOrDefault();
                    if (find != null)
                    {
                        _dbContext.AllNews.Remove(find);
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
