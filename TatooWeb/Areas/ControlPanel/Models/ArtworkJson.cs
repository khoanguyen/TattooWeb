using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TatooWeb.Areas.ControlPanel.Models
{
    public class ArtworkJson
    {
        public string name { get; set; }
        public string size { get; set; }
        public string url { get; set; }
        public string thumbnailUrl { get; set; }
        public string deleteUrl { get; set; }
        public string deleteType { get; set; }
        public string error { get; set; }
    }
}