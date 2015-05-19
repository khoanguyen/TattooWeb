using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TatooWeb.Areas.ControlPanel.Models
{
    public class FeedbackBase
    {
        public int Id { get; set; }
        public string CustomerName { get; set; }
        public string CustomerEmail { get; set; }
        public string CustomerPhone { get; set; }
        public string Message { get; set; }
        public bool IsRead { get; set; }
    }
}