using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TatooWeb.Models
{
    public class Feedback
    {
        public string customerName { get; set; }
        public string customerEmail { get; set; }
        public string customerPhone { get; set; }
        public string message { get; set; }
    }
}