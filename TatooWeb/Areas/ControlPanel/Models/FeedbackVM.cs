using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TatooWeb.Utils;
using Tattoo.Domain.Models;

namespace TatooWeb.Areas.ControlPanel.Models
{
    public class FeedbackVM
    {
        public List<FeedbackBase> Feedbacks { get; set; }
        public PagingInfo PageInfo { get; set; }

        public FeedbackSearchCriteria SearchCriteria { get; set; }

        public FeedbackVM()
        {
            Feedbacks = new List<FeedbackBase>();
        }
    }
}