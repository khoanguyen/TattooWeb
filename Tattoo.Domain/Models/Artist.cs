//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Tattoo.Domain.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Artist
    {
        public Artist()
        {
            this.ArtWorks = new HashSet<ArtWork>();
        }
    
        public int Id { get; set; }
        public string FullName { get; set; }
        public string PhoneNo { get; set; }
        public string ArtistImage { get; set; }
        public string NameKey { get; set; }
        public string BriefIntro { get; set; }
        public Nullable<bool> DefaultArtist { get; set; }
    
        public virtual ICollection<ArtWork> ArtWorks { get; set; }
    }
}
