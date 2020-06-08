using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Wray_Portal_Phase1.Models;
using Wray_Portal_Phase1.ViewModels;

namespace Wray_Portal_Phase1.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();


        public ActionResult Index()
        {
            return View();
        }

        [Authorize]
        public ActionResult About()
        {
            var currentUserId = User.Identity.GetUserId();

            var currentUser = db.Users.Find(currentUserId);
            var userProfileVM = new UserProfileVM();
            userProfileVM.Id = currentUser.Id;
            userProfileVM.FirstName = currentUser.FirstName;
            userProfileVM.LastName = currentUser.LastName;
            userProfileVM.DisplayName = currentUser.DisplayName;
            userProfileVM.Email = currentUser.Email;

            return View(userProfileVM);
        }

        [Authorize]
        public ActionResult EditProfile()
        {
            // Store the Primary Key of the User in the currentUserId variable
            var currentUserId = User.Identity.GetUserId();

            // The currentUser variable represents the entire User record with something like 20 columns...I don't need all of these
            var currentUser = db.Users.Find(currentUserId);
            var userProfileVM = new UserProfileVM();
            userProfileVM.Id = currentUser.Id;
            userProfileVM.FirstName = currentUser.FirstName;
            userProfileVM.LastName = currentUser.LastName;
            userProfileVM.DisplayName = currentUser.DisplayName;
            userProfileVM.Email = currentUser.Email;


            return View(userProfileVM);
        }

        // POST: User Profile
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult EditProfile(UserProfileVM model)
        {
            // I want to get a tracked User record based on the incoming model.Id
            var currentUser = db.Users.Find(model.Id);
            currentUser.FirstName = model.FirstName;
            currentUser.LastName = model.LastName;
            currentUser.DisplayName = model.DisplayName;
            currentUser.Email = model.Email;
            currentUser.UserName = model.Email;

            db.SaveChanges();

            TempData["EditProfileMessage"] = $"Your data has been changed successfully";
            return RedirectToAction("About");
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}