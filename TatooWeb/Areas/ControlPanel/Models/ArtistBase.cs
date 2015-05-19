using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TatooWeb.Utils;

namespace TatooWeb.Areas.ControlPanel.Models
{
    public class ArtistBase
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Avatar { get; set; }
        public string BriefIntro { get; set; }
        public string PhoneNo { get; set; }
        public bool DefaultArtist { get; set; }

        public ArtistBase()
        {
            DefaultArtist = false;
        }
    }
}