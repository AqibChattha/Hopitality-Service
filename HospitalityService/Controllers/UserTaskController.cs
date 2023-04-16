using HospitalityService.Data;
using HospitalityService.Models.Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HospitalityService.Controllers
{
    public class UserTaskController : Controller
    {
        private readonly HopitalityAppDbContext _dbContext;

        public UserTaskController(HopitalityAppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        // GET: UserTaskController
        public ActionResult Index()
        {
            if (GlobalVariables.IS_LOGGED_IN)
            {
                var userlist = _dbContext.UserTasks.Where(o => o.UserId.Equals(GlobalVariables.USER_ID)).ToList();
                return View(userlist);
            }
            return RedirectToAction("Login", "Account");
        }

        // GET: UserTaskController/Create
        public ActionResult Create()
        {
            if (GlobalVariables.IS_LOGGED_IN)
            {
                return View();
            }
            return RedirectToAction("Login", "Account");
        }

        // POST: UserTaskController/Create
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
                        UserTask task = new()
                        {
                            Id = Guid.NewGuid(),
                            Title = collection["Title"],
                            Description = collection["Description"],
                            Deadline = String.IsNullOrEmpty(collection["Deadline"]) ? DateTime.Now : Convert.ToDateTime(collection["Deadline"]),
                            UserId = GlobalVariables.USER_ID
                        };
                        _dbContext.UserTasks.Add(task);
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

        // GET: UserTaskController/Edit/5
        public ActionResult Edit(Guid id)
        {
            if (GlobalVariables.IS_LOGGED_IN)
            {
                var find = _dbContext.UserTasks.Where(o => o.Id.Equals(id)).FirstOrDefault();
                return View(find);
            }
            return RedirectToAction("Login", "Account");
        }

        // POST: UserTaskController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Guid id, IFormCollection collection)
        {
            if (GlobalVariables.IS_LOGGED_IN)
            {
                try
                {
                    var find = _dbContext.UserTasks.Where(o => o.Id.Equals(id)).FirstOrDefault();
                    if (find != null)
                    {
                        find.Title = collection["Title"].ToString();
                        find.Description = collection["Description"].ToString();
                        find.Deadline = Convert.ToDateTime(collection["Deadline"]);
                        find.UserId = GlobalVariables.USER_ID;
                        _dbContext.UserTasks.Update(find);
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

        // GET: UserTaskController/Delete/5
        public ActionResult Delete(Guid id)
        {
            if (GlobalVariables.IS_LOGGED_IN)
            {
                var find = _dbContext.UserTasks.Where(o => o.Id.Equals(id)).FirstOrDefault();
                return View(find);
            }
            return RedirectToAction("Login", "Account");
        }

        // POST: UserTaskController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(Guid id, IFormCollection collection)
        {
            if (GlobalVariables.IS_LOGGED_IN)
            {
                try
                {
                    var find = _dbContext.UserTasks.Where(o => o.Id.Equals(id)).FirstOrDefault();
                    if (find != null)
                    {
                        _dbContext.UserTasks.Remove(find);
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
