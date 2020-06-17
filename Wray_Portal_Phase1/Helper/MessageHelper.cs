//using Microsoft.AspNet.Identity;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Web;
//using Wray_Portal_Phase1.Models;

//namespace Wray_Portal_Phase1.Helper
//{
//    public class MessageHelper
//    {
        
//        public static List<Notification> GetUnreadNotifications()
//        {
//            var userId = HttpContext.Current.User.Identity.GetUserId();
//            var houseId = 
//            //db.Users.Find(User.Identity.GetUserId()).HouseholdId
//            if (houseId == null)
//            {
//                return new List<Notification>();
//            }
//            var db = new ApplicationDbContext();
//            return db.Notifications.Where(t => t.HouseholdId == houseId && !t.IsRead).ToList();
//        }
//    }
//}