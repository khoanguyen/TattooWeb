using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TatooWeb.Areas.ControlPanel.Models;
using TatooWeb.Utils;
using Tattoo.Domain;
using Tattoo.Domain.Models;

namespace TatooWeb.Areas.ControlPanel.Controllers
{
    public class FeedbackPanelController : PanelBaseController
    {
        // GET: ControlPanel/FeedbackPanel
        public ActionResult Index(int page = 1)
        {
            return View(GetFeedbacks(null,page));
        }

        [HttpPost]
        public ActionResult Index(FeedbackSearchCriteria criteria, string dateFrom, string dateTo)
        {
            var fromValue = ConvertDateString(dateFrom, "ddMMyyyy", '-');
            var toValue = ConvertDateString(dateTo, "ddMMyyyy", '-');
            criteria.SentDateFrom = fromValue.HasValue ? fromValue.Value : criteria.SentDateFrom;
            criteria.SentDateTo = toValue.HasValue ? toValue.Value : criteria.SentDateTo;
            var vm = GetFeedbacks(criteria, 1);
            return View(vm);
        }

        public ActionResult Feedback(int id)
        {
            var context = new NewGenerationDataService();
            var feedback = context.UpdateCustomerFeedback(id, true);
            return Json(feedback, JsonRequestBehavior.AllowGet);
        }

        public ActionResult DeleteFeedback(int id)
        {
            var context = new NewGenerationDataService();
            context.DeleteCustomerFeedback(id);
            return RedirectToAction("Index");
        }

        private DateTime? ConvertDateString(string value, string format, char splitter)
        {
            if (string.IsNullOrWhiteSpace(value)) return null;
            var d = value.Split(splitter);
            DateTime date;
            switch (format)
            {
                case "ddMMyyyy":
                    date = new DateTime(int.Parse(d[2]), int.Parse(d[1]), int.Parse(d[0]));
                    break;
                case "MMddyyyy":
                    date = new DateTime(int.Parse(d[2]), int.Parse(d[0]), int.Parse(d[1]));
                    break;
                default:
                    return null;
            }
            return date;
        }

        private FeedbackVM GetFeedbacks(FeedbackSearchCriteria criteria,int page)
        {
            if (criteria != null)
            {
                SessionHelper.SetSession(SessionHelper.FeedbacksSession, criteria);
            }
            else
            {
                if (HttpContext.Session[SessionHelper.FeedbacksSession] == null)
                {
                    criteria = new FeedbackSearchCriteria
                    {
                        Email = string.Empty,
                        Name = string.Empty
                    };
                    SessionHelper.SetSession(SessionHelper.FeedbacksSession, criteria);
                }
                else
                {
                    criteria = (FeedbackSearchCriteria)SessionHelper.GetSession(SessionHelper.FeedbacksSession);
                }                    
            }

            var context = new NewGenerationDataService();
            var viewmodel = new FeedbackVM();
            var feedbacks = context.GetCustomerFeedbacks(criteria);
            viewmodel.SearchCriteria = criteria;
            viewmodel.PageInfo = PagingUtils.Paging(feedbacks.Count(), 20, page);
            viewmodel.Feedbacks = feedbacks.ToList()
                                           .GetRange(viewmodel.PageInfo.StartRecord - 1, viewmodel.PageInfo.EndRecord + 1 - viewmodel.PageInfo.StartRecord)
                                           .Select(i => new FeedbackBase
                                            {
                                                CustomerEmail = i.CustomerEmail,
                                                CustomerName = i.CustomerName,
                                                CustomerPhone = i.CustomerPhone,
                                                Id = i.Id,
                                                IsRead = i.IsRead,
                                                Message = i.Message
                                            }).ToList();

            return viewmodel;
        }
    }
}