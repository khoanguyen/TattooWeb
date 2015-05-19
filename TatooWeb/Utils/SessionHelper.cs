using System;
using System.Web;

namespace TatooWeb.Utils
{
    public class SessionHelper
    {
        public const string LoginSession = "Login_Session";
        public const string ArtistsSession = "Artists_Session";
        public const string ArtWorksSession = "ArtWorks_Session";
        public const string FeedbacksSession = "Feedbacks_Session";

        public static void ClearAllSession()
        {
            HttpContext.Current.Session.Clear();
        }

        public static void ClearSession(string session)
        {
            HttpContext.Current.Session.Remove(session);
        }

        public static void SetSession(string session, object value)
        {
            HttpContext.Current.Session[session] = value;
        }

        public static object GetSession(string session)
        {
            return HttpContext.Current.Session[session];
        }
    }
}