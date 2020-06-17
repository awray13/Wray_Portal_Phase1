using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Wray_Portal_Phase1.Models;

namespace Wray_Portal_Phase1.Helper
{
    public static class TransactionHelper
    {
       
        public static List<Transaction> GetCategoryTransactions(int categoryId)
        {
             var db = new ApplicationDbContext();
             var transactions = new List<Transaction>();

            foreach(var item in db.Categories.Find(categoryId).CategoryItems.ToList())
            {
                transactions.AddRange(db.Transactions.Where(t => t.CategoryItemId == item.Id));
            }
                return transactions;
        }
    }
}