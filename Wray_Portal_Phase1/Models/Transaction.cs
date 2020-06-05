﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Wray_Portal_Phase1.Models
{
    public class Transaction
    {
        public int Id { get; set; }

        public int TransactionTypeId { get; set; }

        public int BankAccountId { get; set; }

        public int? CategoryItemId { get; set; }

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