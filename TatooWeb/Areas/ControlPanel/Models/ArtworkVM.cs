using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TatooWeb.Utils;

namespace TatooWeb.Areas.ControlPanel.Models
{
    public class ArtworkVM
    {
        public int ArtistId { get; set; }
        public string ArtistName { get; set; }
        public List<ArtWorkBase> Artworks { get; set; }
        public PagingInfo PageInfo { get; set; }
        public ArtworkVM()
        {
            Artworks = new List<ArtWorkBase>();
        }

    }
}