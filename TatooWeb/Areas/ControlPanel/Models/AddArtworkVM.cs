using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TatooWeb.Areas.ControlPanel.Models
{
    public class AddArtworkVM
    {
        public int ArtistId { get; set; }
        public string ArtistName { get; set; }
        public List<HttpPostedFileBase> Arts { get; set; }

        public AddArtworkVM()
        {
            Arts = new List<HttpPostedFileBase>();
        }
    }
}