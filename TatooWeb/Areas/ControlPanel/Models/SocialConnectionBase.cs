using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TatooWeb.Areas.ControlPanel.Models
{
    public class SocialConnectionBase
    {
        public int Id { get; set; }
        public string NetworkName { get; set; }
        public string ProfileLink { get; set; }
        public string MainImage { get; set; }
        public string HoverImage { get; set; }
    }
}
