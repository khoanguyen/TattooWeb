using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tattoo.Domain.Models
{
    public class FeedbackSearchCriteria
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public DateTime SentDateFrom { get; set; }
        public DateTime SentDateTo { get; set; }

        public FeedbackSearchCriteria()
        {
            SentDateFrom = DateTime.Now.AddDays(-15);
            SentDateTo = DateTime.Now;
        }
    }
}
