using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tattoo.Domain.Models.DTOs
{
    public partial class ArtWorkDTO
    {
        public int Id { get; set; }
        public string ArtImage { get; set; }
        public string ArtDesc { get; set; }
        public int ArtistId { get; set; }
        public bool ShowOnIntroScreen { get; set; }
    }
}
