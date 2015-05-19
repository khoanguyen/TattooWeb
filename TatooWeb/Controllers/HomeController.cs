using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TatooWeb.Models;
using TatooWeb.Utils;
using Tattoo.Domain;
using Tattoo.Domain.Models;

namespace TatooWeb.Controllers
{
    public class HomeController : Controller
    {
        public ViewResult Index()
        {
            var profile = CacheUtils.GetCompanyProfileCache();
            return View(profile);
        }

        public ActionResult Main()
        {
            return View();
        }

        public JsonResult GetSlideImages()
        {
            var context = new NewGenerationDataService();
            var arts = context.GetOnScreenArtworks();
            var items = new List<SlideImage>();
            var urlBase = GetImagePath.GetDomain() + GetImagePath.GetArtImagePath();
            foreach (var art in arts)
            {
                items.Add(new SlideImage
                {
                    image = string.Format( "{0}/{1}", urlBase, art.ArtImage),
                    thumb = string.Format("{0}/thumb-{1}", urlBase, art.ArtImage),
                    title = art.ArtDesc,
                    url = "" 
                });
            }

            return Json(items, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetAllArtists()
        {
            var context = new NewGenerationDataService();
            var urlBase = GetImagePath.GetDomain() + GetImagePath.GetArtistImagePath();
            var artists = context.GetArtists().Select(a => new ArtistCarousel
            {
                Link = string.Format("/{0}/{1}", a.Id, a.NameKey),
                Name = a.FullName,
                ProfilePicture = string.Format("{0}/{1}", urlBase, a.ArtistImage)
            }).ToList();


            return Json(artists, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetSocialNetworks()
        {
            var urlBase = GetImagePath.GetDomain() + GetImagePath.GetPath();
            var networks = CacheUtils.GetSocialConnectionsCache()
                                     .Select(i => new{
                                                        mainImage = string.Format( "{0}/{1}", urlBase,i.MainImage),
                                                        hoverImage = string.Format( "{0}/{1}", urlBase,i.HoverImage),
                                                        networkName = i.NetworkName,
                                                        link = i.ProfileLink
                                                      }).ToList();
            return Json(networks, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetCompanyProfile()
        {
            var profile = CacheUtils.GetCompanyProfileCache();
            var urlBase = GetImagePath.GetDomain() + GetImagePath.GetPath();
            return Json(new
            {
                title = profile.Title,
                intro = profile.Intro,
                mainImage = string.Format("{0}/{1}", urlBase, profile.HomepageImage),
                phone = profile.ContactNumber,
                address = profile.CompanyAddress
            }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetArtistAndWorks(int? id)
        {
            var context = new NewGenerationDataService();
            var result = id.HasValue ? context.GetArtistAndWorks(id.Value) :
                         CacheUtils.GetDefaultArtistCache();
            var urlBase = GetImagePath.GetDomain() + GetImagePath.GetArtImagePath();
            var arts = result.ArtWorks.Select(i => new
            {
                thumb = string.Format("{0}/thumb-{1}", urlBase, i.ArtImage),
                image = string.Format( "{0}/{1}", urlBase,i.ArtImage),
                desc = i.ArtDesc
            }).ToList();
            return Json(new { 
                                id = result.Id,
                                artistName = result.FullName,
                                contactNumber = result.PhoneNo,
                                artistImage = string.Format( "{0}/{1}/{2}", GetImagePath.GetDomain(), 
                                                            GetImagePath.GetArtistImagePath(), result.ArtistImage),
                                works = arts

                            }, JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public void SendFeedback(Feedback feedback)
        {
            feedback.customerName = feedback.customerName.Trim();
            feedback.customerEmail = string.IsNullOrWhiteSpace(feedback.customerEmail) ? 
                                     feedback.customerEmail : feedback.customerEmail.Trim();
            feedback.customerPhone = string.IsNullOrWhiteSpace(feedback.customerPhone) ? 
                                     feedback.customerPhone : feedback.customerPhone.Trim();

            var context = new NewGenerationDataService();
            context.CreateCustomerFeedback(new CustomerFeedback
            {
                CustomerName = feedback.customerName,
                CustomerEmail = feedback.customerEmail,
                CustomerPhone = feedback.customerPhone,
                Message = feedback.message,
                IsRead = false,
                SentDate = DateTime.Now
            });

        }
    }
}