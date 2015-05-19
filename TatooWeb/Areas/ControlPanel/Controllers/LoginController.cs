using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TatooWeb.Areas.ControlPanel.Models;
using TatooWeb.Utils;
using Tattoo.Domain;

namespace TatooWeb.Areas.ControlPanel.Controllers
{
    public class LoginController : Controller
    {
        // GET: ControlPanel/Login
        public ActionResult Index()
        {
            ViewBag.Message = string.Empty;
            return View();
        }

        [HttpPost]
        public ActionResult Index(User user)
        {
            var context = new NewGenerationDataService();
            var credential = context.GetCredential(user.UserName, user.Password);
            if (credential == null)
            {
                ViewBag.Message = "Login name or password is incorrect";
                return View(user);
            }
            user.FullName = credential.FullName;
            SessionHelper.SetSession(SessionHelper.LoginSession, user);
            return RedirectToAction("Index", "ArtistPanel");         
            
        }

        public ActionResult Logout()
        {
            SessionHelper.ClearAllSession();
            return RedirectToAction("Index");
        }
    }
}