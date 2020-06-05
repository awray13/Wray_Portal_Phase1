using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Web;

namespace Wray_Portal_Phase1.Models
{
    public class Category
    {
        public int Id { get; set; }

        public int HouseholdId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public decimal TargetAmount { get; set; }



        // Navigation Props

        public virtual Household Household { get; set; }

        public virtual ICollection<CategoryItem> CategoryItems { get; set; }


        public Category()
        {
            CategoryItems = new HashSet<CategoryItem>();
        }
    }
}