using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Caching;
using Tattoo.Domain;
using Tattoo.Domain.Models;

namespace TatooWeb.Utils
{
    public static class CacheUtils
    {
        public const string CompanyProfileCache = "ConpanyProfileCache";
        public const string DefaultArtistCache = "DefaultArtistCache";
        public const string SocialConnectionsCache = "SocialConnectionsCache";
        public static CompanyProfile GetCompanyProfileCache()
        {
            if (HttpContext.Current.Cache[CompanyProfileCache] == null) SetCompanyProfileCache();
            return (CompanyProfile)HttpContext.Current.Cache[CompanyProfileCache];
        }

        public static Artist GetDefaultArtistCache()
        {
            if (HttpContext.Current.Cache[DefaultArtistCache] == null) SetDefaultArtistCache();
            return (Artist)HttpContext.Current.Cache[DefaultArtistCache];
        }

        public static List<SocialConnection> GetSocialConnectionsCache()
        {
            if (HttpContext.Current.Cache[SocialConnectionsCache] == null) SetSocialConnecctionsCahce();
            return (List<SocialConnection>)HttpContext.Current.Cache[SocialConnectionsCache];
        }


        private static void SetCompanyProfileCache()
        {
            var context = new NewGenerationDataService();
            var profile = context.GetCompanyProfile();
            HttpContext.Current.Cache.Insert(CompanyProfileCache, profile, null, Cache.NoAbsoluteExpiration, new TimeSpan(24, 0, 0));
        }

        private static void SetDefaultArtistCache()
        {
            var context = new NewGenerationDataService();
            var artists = context.GetArtistByCriteria(new ArtistSearchCriteria { SearchName = string.Empty, IsDefault = true }).First();
            HttpContext.Current.Cache.Insert(DefaultArtistCache, artists, null, Cache.NoAbsoluteExpiration, new TimeSpan(1, 0, 0));
        }

        private static void SetSocialConnecctionsCahce()
        {
            var context = new NewGenerationDataService();
            var cons = context.GetSocialonnections();
            HttpContext.Current.Cache.Insert(SocialConnectionsCache, cons, null, Cache.NoAbsoluteExpiration, new TimeSpan(24, 0, 0));
        }
    }
}