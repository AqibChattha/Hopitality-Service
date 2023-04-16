﻿using HospitalityService.Data;
using HospitalityService.Models.Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HospitalityService.Controllers
{
    public class UserController : Controller
    {
        private readonly HopitalityAppDbContext _dbContext;

        public UserController(HopitalityAppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        // GET: UserController
        public IActionResult Index()
        {
            if (GlobalVariables.IS_LOGGED_IN)
            {
                var userlist = _dbContext.Users.Where(o => !GlobalVariables.ADMIN.Equals(o.UserType)).ToList();
                return View(userlist);
            }
            return RedirectToAction("Login", "Account");
        }

        // GET: UserController/Create
        public IActionResult Create()
        {
            if (GlobalVariables.IS_LOGGED_IN)
            {
                return View();
            }
            return RedirectToAction("Login", "Account");
        }

        // POST: UserController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(IFormCollection collection)
        {
            if (GlobalVariables.IS_LOGGED_IN)
            {
                try
                {
                    var find = _dbContext.Users.Where(o => collection["Email"].Equals(o.Email)).FirstOrDefault();
                    if (find == null)
                    {
                        User user = new()
                        {
                            Id = Guid.NewGuid(),
                            Name = collection["Name"],
                            Email = collection["Email"],
                            Password = collection["Password"],
                            ImgURL = collection["ImgURL"],
                            UserType = collection["UserType"]
                        };
                        _dbContext.Users.Add(user);
                        _dbContext.SaveChanges();
                        return RedirectToAction(nameof(Index));
                    }
                    TempData["ErrorMessage"] = "User with given email already exists.";
                }
                catch
                {
                }
                return View();
            }
            return RedirectToAction("Login", "Account");
        }

        // GET: UserController/Edit/5
        public IActionResult Edit(Guid id)
        {
            if (GlobalVariables.IS_LOGGED_IN)
            {
                var find = _dbContext.Users.Where(o => o.Id.Equals(id)).FirstOrDefault();
                return View(find);
            }
            return RedirectToAction("Login", "Account");
        }

        // POST: UserController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Guid id, IFormCollection collection)
        {
            if (GlobalVariables.IS_LOGGED_IN)
            {
                try
                {
                    var find = _dbContext.Users.Where(o => o.Id.Equals(id)).FirstOrDefault();
                    if (find != null)
                    {
                        find.Name = collection["Name"].ToString();
                        find.Email = collection["Email"].ToString();
                        find.Password = collection["Password"].ToString();
                        find.UserType = collection["UserType"].ToString();
                        find.ImgURL = collection["ImgURL"].ToString();
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

        // GET: UserController/Delete/5
        public IActionResult Delete(Guid id)
        {
            if (GlobalVariables.IS_LOGGED_IN)
            {
                var find = _dbContext.Users.Where(o => o.Id.Equals(id)).FirstOrDefault();
                return View(find);
            }
            return RedirectToAction("Login", "Account");
        }

        // POST: UserController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(Guid id, IFormCollection collection)
        {
            if (GlobalVariables.IS_LOGGED_IN)
            {
                try
                {
                    var find = _dbContext.Users.Where(o => o.Id.Equals(id)).FirstOrDefault();
                    if (find != null)
                    {
                        _dbContext.Users.Remove(find);
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
