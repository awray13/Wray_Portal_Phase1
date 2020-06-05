using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Wray_Portal_Phase1.Models;

namespace Wray_Portal_Phase1.Helper
{
    public class NotificationHelper

    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public void ManageNotifications(BankAccount account, Transaction transaction)
        {
            
            ManageNotification(account, transaction);

            

        }

        private void ManageNotification(BankAccount account, Transaction transaction)
        {
            var overdraft = account.CurrentBalance < 0;
            var warning = account.CurrentBalance < account.LowBalanceLevel;
            var variableText = account.CurrentBalance < 0 ? "an overdraft" : "a low balance level breach";

            if (overdraft || warning)
            {
                var message = $"Your recent transaction in the amount of {transaction.Amount} for {transaction.Memo} has resulted in {variableText} leaving your {account.Name} account with a {account.LowBalanceLevel}";
                GenerateNotification(message, account.HouseholdId);
            }
            
        }

        private void GenerateNotification(string msg, int householdId)
        {
            var newNotification = new Notification
            {
                HouseholdId = householdId,
                Created = DateTime.Now,
                Subject = "Heads up something interesting happened in your Bank Account",
                Body = msg,
                IsRead = false
            };

            db.Notifications.Add(newNotification);
            db.SaveChanges();

        }
        
    }
}