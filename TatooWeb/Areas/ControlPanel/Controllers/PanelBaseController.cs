using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TatooWeb.Utils;

namespace TatooWeb.Areas.ControlPanel.Controllers
{
    public class PanelBaseController : Controller
    {
        // GET: ControlPanel/PanelBase
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (filterContext.ActionDescriptor.ControllerDescriptor.ControllerName != "Login")
            {
                if (SessionHelper.GetSession(SessionHelper.LoginSession) == null)
                {
                    filterContext.Result = RedirectToAction("Index", "Login");
                }
            }
        }
    }
}