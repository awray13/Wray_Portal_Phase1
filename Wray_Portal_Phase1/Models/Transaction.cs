using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Wray_Portal_Phase1.Models
{
    public class Transaction
    {
        public int Id { get; set; }

        [Display(Name = "Transaction Type")]
        public int TransactionTypeId { get; set; }

        [Display(Name = "Bank Account")]
        public int BankAccountId { get; set; }

        [Display(Name = "Category Item")]
        public int? CategoryItemId { get; set; }

        [Display(Name = "Owner")]
        public string OwnerId { get; set; }

        public decimal Amount { get; set; }

        public string Memo { get; set; }

        public DateTime Created { get; set; }




        // Nav Props
        public virtual BankAccount BankAccount { get; set; }
        public virtual TransactionType TransactionType { get; set; }
        public virtual CategoryItem CategoryItem { get; set; }
        public virtual ApplicationUser Owner { get; set; }
 

    }
}