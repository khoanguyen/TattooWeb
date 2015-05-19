using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TatooWeb.Areas.ControlPanel.Models
{
    public class CompanyProfileBase
    {
        public string Title { get; set; }
        public string Intro { get; set; }
        public string HomepageImage { get; set; }
        public string CompanyAddress { get; set; }
        public string ContactNumber { get; set; }
    }
}
