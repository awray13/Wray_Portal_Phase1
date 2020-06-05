using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Wray_Portal_Phase1.Models
{
    public class BankAccount
    {
        public int Id { get; set; }

        public int HouseholdId { get; set; }

        public int BankAccountTypeId { get; set; }

        public string OwnerId { get; set; }

        public DateTime Created { get; set; }

        public string Name { get; set; }

        public decimal StartingBalance { get; set; }

        public decimal CurrentBalance { get; set; }

        public decimal LowBalanceLevel { get; set; }

        // Nav props

        public virtual Household Household { get; set; }
        public virtual BankAccountType BankAccountType { get; set; }
        

        public virtual ICollection<Transaction> Transactions { get; set; }

        public BankAccount()
        {
            Transactions = new HashSet<Transaction>();
        }
    }
}