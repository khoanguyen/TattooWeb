using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace TatooWeb.Utils
{
    public class GetImagePath
    {
        public static string GetDomain(){
            try
            {
                return HttpContext.Current.Request.Url.Scheme + Uri.SchemeDelimiter + HttpContext.Current.Request.Url.Host +
                    (HttpContext.Current.Request.Url.IsDefaultPort ? "" : ":" + HttpContext.Current.Request.Url.Port.ToString());
            }
            catch
            {
                return "~";
            }
        }
        public static string GetArtistImagePath()
        {
            return ConfigurationManager.AppSettings["DataArtistImageLocation"] == null ?
                    "/Content/images/site_images" :
                    ConfigurationManager.AppSettings["DataArtistImageLocation"];
        }

        public static string GetArtImagePath()
        {
            return ConfigurationManager.AppSettings["DataArtImageLocation"] == null ?
                    "/Content/images/site_images" :
                    ConfigurationManager.AppSettings["DataArtImageLocation"];
        }

        public static string GetPath()
        {
            return ConfigurationManager.AppSettings["DataImageLocation"] == null ?
                    "/Content/images/site_images" :
                    ConfigurationManager.AppSettings["DataImageLocation"];
        }
    }
}