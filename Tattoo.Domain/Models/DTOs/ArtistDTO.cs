using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tattoo.Domain.Models.DTOs
{    public partial class ArtistDTO
    {
		public ArtistDTO() 
        {
            SetNameKey(FullName);
        }
		
        public int Id { get; set; }
        public string FullName { get; set; }
        public string NameKey { get; private set; }
        public string ArtistImage { get; set; }
        public string PhoneNo { get; set; }
        public string BriefIntro { get; set; }
        public bool DefaultArtist { get; set; }
        public List<ArtWorkDTO> ArtWorks { get; set; }

        public void SetName(string fullName)
        {
            FullName = fullName;
            SetNameKey(FullName);
        }

        private void SetNameKey(string fullName)
        {
            if (string.IsNullOrEmpty(FullName))
                NameKey = FullName.ToLower().Replace(" ", "-");
        }
    }
}
