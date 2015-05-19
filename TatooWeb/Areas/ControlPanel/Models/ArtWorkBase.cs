using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TatooWeb.Areas.ControlPanel.Models
{
    public class ArtWorkBase
    {
        public int Id { get; set; }
        public HttpPostedFileBase ArtImage { get; set; }
        public HttpPostedFileBase Thumbnail { get; set; }
        public string ArtImageUrl { get; set; }
        public string ThumbnailUrl { get; set; }
        public string ArtDesc { get; set; }
        public bool ShowOnIntroScreen { get; set; }

    }
}