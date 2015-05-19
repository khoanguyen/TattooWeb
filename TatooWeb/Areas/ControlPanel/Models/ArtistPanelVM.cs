using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TatooWeb.Utils;
using Tattoo.Domain.Models;

namespace TatooWeb.Areas.ControlPanel.Models
{
    public class ArtistPanelVM
    {
        public PagingInfo PageInfo { get; set; }

        public List<ArtistBase> Artists { get; set; }

        public ArtistSearchCriteria SearchCriteria { get; set; }

        public ArtistPanelVM()
        {
            Artists = new List<ArtistBase>();
            SearchCriteria = new ArtistSearchCriteria
            {
                SearchName = string.Empty
            };
        }
    }
}