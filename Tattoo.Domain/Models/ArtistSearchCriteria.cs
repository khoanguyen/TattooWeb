using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tattoo.Domain.Models
{
    public class ArtistSearchCriteria
    {
        public string SearchName { get; set; }
        public bool? IsDefault { get; set; }
    }
}
