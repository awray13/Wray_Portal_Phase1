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
    public class ChartsController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();
        // GET: Charts
        public JsonResult GetCategoryChartData()
        {
            var colorList = new List<string>();
            colorList.Add("#886896");
            colorList.Add("#d2d6de");

            var catDataVM = new CatDataVM();
            var householdId = db.Users.Find(User.Identity.GetUserId()).HouseholdId;

            foreach (var category in db.Categories.Where(c => c.HouseholdId == householdId).ToList())
            {
                var catData = new CategoryDataVM
                {
                    Name = category.Name,
                    Target = category.TargetAmount
                };

                foreach (var item in db.Categories.Find(category.Id).CategoryItems.ToList())
                {
                    // This should be focused on the current month only!!!
                    // How do I work down the Transactions to only this month??
                    // Where(t = t.Created) is a good place to start
                    var transactions = db.Transactions.Where(t => t.CategoryItemId == item.Id).ToList();
                    if (transactions.Count > 0)
                    {
                        catData.Actual += transactions.Sum(t => t.Amount);
                    }
                }

                catDataVM.Data.Add(catData);
                catDataVM.Colors.AddRange(colorList);
            }

            catDataVM.Labels.AddRange(new List<string> { "Target", "Actual", "Name" });

            return Json(catDataVM);
        }
    }
}