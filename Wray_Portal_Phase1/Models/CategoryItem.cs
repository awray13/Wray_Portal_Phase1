using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Linq;
using System.Web;

namespace Wray_Portal_Phase1.Models
{
    public class CategoryItem
    {
        public int Id { get; set; }

        public int CategoryId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }


        // Nav Props
        public virtual Category Category { get; set; }
    }
}