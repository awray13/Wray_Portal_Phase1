using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Wray_Portal_Phase1.ViewModels
{
    public class CategoryDataVM
    {
        public string Name { get; set; }
        public decimal Target { get; set; }
        public decimal Actual { get; set; }

    }

    public class CatDataVM
    {
        public List<CategoryDataVM> Data = new List<CategoryDataVM>();
        public List<string> Labels = new List<string>();
        public List<string> Colors = new List<string>();


    }
}