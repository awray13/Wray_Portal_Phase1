using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Wray_Portal_Phase1.Models
{
    public class TransactionType
    {
        public int Id { get; set; }

        public string Type { get; set; }

        public string Description { get; set; }

        // Nav Props
        public virtual ICollection<Transaction> Transactions { get; set; }

        public TransactionType()
        {
            Transactions = new HashSet<Transaction>();
        }
    }
}