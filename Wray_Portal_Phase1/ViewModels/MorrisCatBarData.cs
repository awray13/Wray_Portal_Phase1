using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Wray_Portal_Phase1.ViewModels
{
    public class MorrisCatBarData
    {
        public string Category { get; set; }
        public decimal Target { get; set; }
        public decimal Actual { get; set; }

    }

    public class CatDataVM
    {
        public List<MorrisCatBarData> Data { get; set; }
        public List<string> Labels { get; set; }

        public CatDataVM()
        {
            Data = new List<MorrisCatBarData>();
            Labels = new List<string>();
        }
    }
}