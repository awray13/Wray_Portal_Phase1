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
            var dataVM = new CatDataVM();

            var householdId = db.Users.Find(User.Identity.GetUserId()).HouseholdId;
            foreach (var category in db.Categories.Where(c => c.HouseholdId == householdId).ToList())
            {
                // Now that I have a Category I need all the category items
                var catItems = db.Categories.Find(category.Id).CategoryItems.ToList();
                decimal actual = 0;
                foreach (var item in catItems)
                {
                    var transactions = db.Transactions.Where(t => t.CategoryItemId == item.Id).ToList();
                    if (transactions != null)
                    {
                        actual += transactions.Sum(t => t.Amount);
                    }
                }

                var catData = new MorrisCatBarData
                {
                    Category = category.Name,
                    Target = category.TargetAmount,
                    Actual = actual
                };

                dataVM.Data.Add(catData);
                dataVM.Labels.Add(category.Name);
            }
            return Json(dataVM);
        }
    }
}