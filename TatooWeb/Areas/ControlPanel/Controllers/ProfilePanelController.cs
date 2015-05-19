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
    public class ProfilePanelController : PanelBaseController
    {
        // GET: ControlPanel/CompanyProfile
        public ActionResult Index()
        {
            var context = new NewGenerationDataService();
            var profile = context.GetCompanyProfile();
                   
            var viewmodel = new CompanyProfileBase
            {
                CompanyAddress = profile.CompanyAddress, 
                ContactNumber = profile.ContactNumber,
                HomepageImage = string.Format("{0}/{1}", GetImageLocation(), profile.HomepageImage),
                Intro = profile.Intro,
                Title = profile.Title                
            };

            return View(viewmodel);
        }

        public ViewResult SocialConnection()
        {
            var context = new NewGenerationDataService();
            var fullpath = string.Format("{0}/", GetImageLocation());
            var connections = context.GetSocialonnections().Select(c => new SocialConnectionBase
            {
                Id = c.Id,
                NetworkName = c.NetworkName,
                ProfileLink = c.ProfileLink,
                MainImage = fullpath + c.MainImage,
                HoverImage = fullpath + c.MainImage
            }).ToList();
            return View(connections);
        }

        public ViewResult AddSocialConnection()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddSocialConnection(SocialConnectionBase connection, HttpPostedFileBase mainImage, 
                                           HttpPostedFileBase hoverImage)
        {
            var context = new NewGenerationDataService();
            var imgUtil = new ImageUtils();
            try
            {
                if (mainImage != null && mainImage.ContentLength > 0)
                {
                    var fileName = string.Format("{0}{1}", DateTime.Now.ToString("ddMMyyyyHHmmss"),
                                                 mainImage.FileName.Substring(mainImage.FileName.LastIndexOf('.')));
                    imgUtil.SaveImage(mainImage, GetSaveLocation(), fileName);
                    connection.MainImage = fileName;

                    if (hoverImage != null && hoverImage.ContentLength > 0)
                    {
                        fileName = "hover-" + fileName;
                        imgUtil.SaveImage(hoverImage, GetSaveLocation(), fileName);
                        connection.HoverImage = fileName;
                    }
                    else
                    {
                        connection.HoverImage = connection.MainImage;
                    }
                }
                context.CreateSocialConnection(new SocialConnection
                {
                    NetworkName = connection.NetworkName,
                    ProfileLink = connection.ProfileLink,
                    MainImage = connection.MainImage,
                    HoverImage = connection.HoverImage
                });

                connection.MainImage = GetImageLocation() + connection.MainImage;
                connection.HoverImage = GetImageLocation() + connection.HoverImage;

                if (HttpContext.Cache[CacheUtils.SocialConnectionsCache] != null)
                {
                    HttpContext.Cache.Remove(CacheUtils.SocialConnectionsCache);
                }

                return RedirectToAction("SocialConnection");
            }
            catch
            {
                if (!string.IsNullOrWhiteSpace(connection.MainImage))
                {
                    imgUtil.DeleteImage(connection.MainImage, GetSaveLocation());
                    if(connection.MainImage != connection.HoverImage)
                        imgUtil.DeleteImage(connection.HoverImage, GetSaveLocation());
                }
                ViewBag.Error = "Can not create connection.";
                return View(connection);
            }
        }

        [HttpPost] 
        public JsonResult UpdateSocialConnection(SocialConnectionBase connection)
        {
            try
            {
                var context = new NewGenerationDataService();
                context.UpdateSocialConnection(new SocialConnection
                {
                    Id = connection.Id,
                    NetworkName = connection.NetworkName,
                    ProfileLink = connection.ProfileLink
                });
                
                return Json(connection);
            }
            catch (Exception ex)
            {
                throw new HttpException(400, "Can not update record. Error: " + ex.Message);
            }
        }

        [HttpPost]
        public JsonResult DeleteSocialConnection(int id)
        {
            try
            {
                var context = new NewGenerationDataService();
                var deleteds = context.DeleteSocialConnection(id).Split('|');
                var imgUtil = new ImageUtils();

                foreach (var str in deleteds)
                {
                    imgUtil.DeleteImage(str, GetSaveLocation());
                }

                if (HttpContext.Cache[CacheUtils.SocialConnectionsCache] != null)
                {
                    HttpContext.Cache.Remove(CacheUtils.SocialConnectionsCache);
                }

                return Json(new { });
            }
            catch (Exception ex)
            {
                throw new HttpException(400, "Can't delete record. Error: " + ex.Message);
            }
        }

        [HttpPost]
        public ActionResult Index(CompanyProfileBase profile, HttpPostedFileBase profileImage)
        {
            var imgUtil = new ImageUtils();
            var fileName = string.Empty;
            var context = new NewGenerationDataService();
            try
            {
                if (profileImage != null && profileImage.ContentLength > 0)
                {
                    fileName = string.Format("newgeneration_tattoo_{0}{1}",
                                                 DateTime.Now.ToString("ddMMyyyyHHmmss"),
                                                 profileImage.FileName.Substring(profileImage.FileName.LastIndexOf('.')));
                    imgUtil.SaveImage(profileImage, GetSaveLocation(), fileName);

                    var oldImage = context.GetCompanyProfile().HomepageImage;
                    context.UpdateProfileImage(fileName);
                    imgUtil.DeleteImage(oldImage, GetSaveLocation());
                    
                    context.UpdateProfileImage(fileName);
                }

                var updated = context.UpdateCompanyProfile(new CompanyProfile
                {
                    Title = profile.Title,
                    Intro = profile.Intro,
                    ContactNumber = profile.ContactNumber,
                    CompanyAddress = profile.CompanyAddress
                });

                if (HttpContext.Cache[CacheUtils.CompanyProfileCache] != null)
                {
                    HttpContext.Cache.Remove(CacheUtils.CompanyProfileCache);
                }

                return View("Index",new CompanyProfileBase
                    {
                        Title = updated.Title,
                        Intro = updated.Intro,
                        ContactNumber = updated.ContactNumber,
                        CompanyAddress = updated.CompanyAddress,
                        HomepageImage = string.Format("{0}/{1}", GetImageLocation(), updated.HomepageImage)
                    });
            }
            catch (Exception ex)
            {
                if (string.IsNullOrWhiteSpace(fileName)) imgUtil.DeleteImage(fileName, GetSaveLocation());
                ViewBag.Error = "Can't update profile. Error: " + ex.Message;
                return View("Index", profile);
            }            
        }

        private string GetSaveLocation()
        {
            return Server.MapPath(string.Format("~{0}", GetImagePath.GetPath()));
        }

        private string GetImageLocation()
        {
            return string.Format("{0}{1}", GetImagePath.GetDomain(), GetImagePath.GetPath());
        }
    }
}