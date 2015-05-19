using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tattoo.Domain.Models
{
    public class ArtWorkSearchCriteria
    {
        public int? ArtistId { get; set; }
        public bool? ShowOnIntroScreen { get; set; }
    }
}
