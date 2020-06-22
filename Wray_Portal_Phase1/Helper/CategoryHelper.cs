using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Wray_Portal_Phase1.Models;

namespace Wray_Portal_Phase1.Helper
{
    public class CategoryHelper
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public ICollection<Category> ListHouseCategories(string userId)
        {
            ApplicationUser user = db.Users.Find(userId);
            var houseId = db.Users.Find(userId).HouseholdId;
            var houseCategories = db.Categories.Where(c => c.HouseholdId == houseId).ToList();
            return (houseCategories);
            

        }


    }
}