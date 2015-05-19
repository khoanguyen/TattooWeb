using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TatooWeb.Areas.ControlPanel.Models;
using TatooWeb.Utils;
using Tattoo.Domain;
using Tattoo.Domain.Exceptions;
using Tattoo.Domain.Models;


namespace TatooWeb.Areas.ControlPanel.Controllers
{
    public class ArtistPanelController : PanelBaseController
    {
        // GET: ControlPanel/ArtistPanel
        public ActionResult Index(int page = 1)
        {
            return View(GetArtists(null, page));
        }       

        public ActionResult Add()
        {
            return View();
        }

        public ActionResult Edit(int id)
        {
            var context = new NewGenerationDataService();
            var artist = context.GetArtistAndWorks(id);
            var path = String.Format("{0}{1}/", GetImagePath.GetDomain(), GetImagePath.GetArtImagePath());
            var model = new ArtistBase
            {
                Id = artist.Id,
                FullName = artist.FullName,
                PhoneNo = artist.PhoneNo,
                DefaultArtist = artist.DefaultArtist.Value,
                BriefIntro = artist.BriefIntro,
                Avatar = string.Format("{0}{1}/{2}", GetImagePath.GetDomain(), GetImagePath.GetArtistImagePath(), artist.ArtistImage)
            };
           
            return View(model);
        }

        public ActionResult Artworks(int id, int page = 1)
        {
            var vm = GetArtworks(id, page);
            return View(vm);
        }
        [HttpPost]
        public PartialViewResult ArtworksAjax(int id, int page = 1)
        {
            var vm = GetArtworks(id, page);
            return PartialView("ArtworkPartial", vm);      
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Index(string searchName)
        {
            var vm = GetArtists(new ArtistSearchCriteria { SearchName = searchName }, 1);
            return View(vm);
        }

        public ViewResult AddArtworks(int id)
        {
            var context = new NewGenerationDataService();
            try
            {
                var artist = context.GetArtist(id);
                var vm = new AddArtworkVM
                {
                    ArtistId = id,
                    ArtistName = artist.FullName
                };
                return View(vm);
            }
            catch
            {
                ViewBag.Error = "This artist does not or no longer exist.";
                return View();
            }            
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Add(ArtistBase artistBase, HttpPostedFileBase avatar)
        {
            if (string.IsNullOrWhiteSpace(artistBase.FullName))
            {
                ViewBag.Error = "Name can not be left blank.";
                return View(artistBase);
            }

            var artist = new Artist
            {
                FullName = artistBase.FullName,
                DefaultArtist = artistBase.DefaultArtist,
                PhoneNo = artistBase.PhoneNo,
                BriefIntro = artistBase.BriefIntro
            };
            var imgUtil = new ImageUtils();

            try
            {                
                if (avatar != null && avatar.ContentLength > 0)
                {
                    imgUtil = new ImageUtils();
                    var fileName = artist.FullName.ToLower().Replace(' ','-') + DateTime.Now.ToString("ddMMyyyyHHmmss");
                    fileName = fileName.Length > 80 ? fileName.Substring(fileName.Length - 80) : fileName;
                    fileName += avatar.FileName.Substring(avatar.FileName.LastIndexOf('.'));
                    imgUtil.SaveImage(avatar, GetMapPath(GetImagePath.GetArtistImagePath()), fileName);
                    artist.ArtistImage = fileName;
                }
                else
                {
                    artist.ArtistImage = "default_avatar.jpg";
                }
            
                var context = new NewGenerationDataService();
                var result = context.CreateArtist(artist);
                artistBase.Id = result.Id;
                artistBase.Avatar = string.Format("{0}{1}/{2}", GetImagePath.GetDomain(), 
                                                    GetImagePath.GetArtistImagePath(), artist.ArtistImage);
                return View("ArtistReview", artistBase);
            }
            catch(Exception ex)
            {
                if (artist.ArtistImage != "default_avatar.jpg") 
                    imgUtil.DeleteImage(artist.ArtistImage, GetMapPath(GetImagePath.GetArtistImagePath()));

                ViewBag.Error = "Unknown error. Can not add artist. Error: " + ex.Message;
                return View(artistBase);
            }            
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Edit(ArtistBase artistBase, HttpPostedFileBase avatar)
        {
            if (string.IsNullOrWhiteSpace(artistBase.FullName))
            {
                ViewBag.Error = "Name can not be left blank.";
                return View(artistBase);
            }
                
            try
            {
                var context = new NewGenerationDataService();
                var artist = context.GetArtist(artistBase.Id);
                if (avatar != null && avatar.ContentLength > 0)
                {
                    UpdateArtistAvatar(artist, avatar);

                }
               
                artist.Id = artistBase.Id;
                artist.FullName = artistBase.FullName;
                artist.NameKey = artistBase.FullName;
                artist.PhoneNo = artistBase.PhoneNo;
                artist.BriefIntro = artistBase.BriefIntro;
                artist.DefaultArtist = artistBase.DefaultArtist;

                context.UpdateArtist(artist);

                artistBase.Avatar = string.Format("{0}{1}/{2}", GetImagePath.GetDomain(), GetImagePath.GetArtistImagePath(), artist.ArtistImage);
                return View("ArtistReview", artistBase);
            }
            catch (ResourceNotFoundException)
            {
                ViewBag.Error = "This artist does not or no longer exist.";
                return View(artistBase);
            }
            catch(Exception ex)
            {
                ViewBag.Error = "Unknown error. Can not update artist. " + ex.Message;
                return View(artistBase);
            }
        }

        [HttpPost]
        public JsonResult Artist(int id)
        {
            try
            {
                var context = new NewGenerationDataService();
                var deletedImg = context.DeleteArtist(id);
                if (deletedImg != "default_avatar.jpg")
                {
                    var imgUtil = new ImageUtils();
                    imgUtil.DeleteImage(deletedImg, GetMapPath(GetImagePath.GetArtistImagePath()));
                }
                    
                
                return Json(new { data = "deleted successfully" });
            }
            catch (ResourceNotFoundException)
            {
                throw new HttpException(404 , "This artist does not or no longer exist.");
            }
            catch (CanNotDeleteException)
            {
                throw new HttpException(403 , "The artist is linked with one or more artworks. Delete these artworks before delete the artist.");
            }
            catch
            {
                throw new HttpException(400, "Unknown error. Can not delete artist.");
            }
        }

        [HttpPost]
        public ActionResult AddArtworks(int id, string name, IEnumerable<HttpPostedFileBase> arts)
        {
            var context = new NewGenerationDataService();
            var imgUtil = new ImageUtils();
            var artworks = new List<ArtWork>();
            var path = GetMapPath(GetImagePath.GetArtImagePath());
            var currentFile = "";
            var result = new List<ArtworkJson>();
            var imgPath = GetImagePath.GetDomain() + GetImagePath.GetArtImagePath();
           
            foreach (var art in arts)
            {
                try
                {
                    if (art != null && art.ContentLength > 0)
                    {
                        var fileName = string.Format("art-{0}-{1}", id, GenerateGuidString());
                        fileName += art.FileName.Substring(art.FileName.LastIndexOf('.'));
                        currentFile = fileName;
                        imgUtil.SaveImageWithThumbnail(art, path, fileName, 135, 216);
                       
                        artworks.Add(new ArtWork
                        {
                            ArtImage = fileName,
                            ShowOnIntroScreen = false
                        });

                        result.Add(new ArtworkJson
                        {
                            name = fileName,
                            size = art.ContentLength.ToString(),
                            url = string.Format("{0}/{1}", imgPath, fileName),
                            thumbnailUrl = string.Format("{0}/thumb-{1}", imgPath, fileName),
                        });                      
                    }
                }
                catch (Exception ex)
                {
                    imgUtil.DeleteImage(currentFile, path);
                    imgUtil.DeleteImage("thumb-"+currentFile, path);
                    result.Add(new ArtworkJson
                    {
                        name = art.FileName,
                        size = art.ContentLength.ToString(),
                        error = ex.Message
                    });
                    continue;
                }
            }

            try
            {
                context.CreateArtworks(artworks, id);
                ClearDefaultArtistCache(context, id);
            }
            catch (Exception ex)
            {
                foreach (var r in result)
                {
                    r.error = ex.Message;
                    imgUtil.DeleteImage(r.name, path);
                    imgUtil.DeleteImage("thumb-"+r.name, path);
                }
            }
            return Json(new { files = result });
        }

        [HttpPost]
        public JsonResult UpdateArtwork(ArtWorkBase art)
        {
            var context = new NewGenerationDataService();
            try
            {
                var updated = context.UpdateArtwork(new ArtWork
                {
                    Id = art.Id,
                    ArtDesc = art.ArtDesc,
                    ShowOnIntroScreen = art.ShowOnIntroScreen
                });
                art.ArtImageUrl = string.Format("{0}{1}/{2}",GetImagePath.GetDomain(), GetImagePath.GetArtImagePath(),updated.ArtImage);
                art.ThumbnailUrl = string.Format("{0}{1}/thumb-{2}", GetImagePath.GetDomain(), GetImagePath.GetArtImagePath(), updated.ArtImage);
                art.ArtDesc = updated.ArtDesc;
                art.ShowOnIntroScreen = updated.ShowOnIntroScreen.Value;
                ClearDefaultArtistCache(context, updated.ArtistId);
                return Json(art);
            }
            catch(Exception ex)
            {
                throw new HttpException(400, "Can't update. Error: " + ex.Message);
            }
        }

        [HttpPost]
        public JsonResult Artwork(HttpPostedFileBase art, HttpPostedFileBase thumbnail, int artId)
        {
            var context = new NewGenerationDataService();
            var imgUtil = new ImageUtils();
            var path = GetMapPath(GetImagePath.GetArtImagePath());
            try
            {
                var artwork = context.GetArtwork(artId);

                if (art != null && art.ContentLength > 0)
                {
                    var fileName = string.Format("art-{0}-{1}", artwork.ArtistId, DateTime.Now.ToString("ddMMyyyyHHmmss"));
                    fileName += art.FileName.Substring(art.FileName.LastIndexOf('.'));
                    if (thumbnail != null && thumbnail.ContentLength > 0)
                    {
                        imgUtil.SaveImage(art, path, fileName);
                        imgUtil.ResizeAndCropImage(thumbnail, path, "thumb-" + fileName, 135, 216);
                    }
                    else
                    {
                        imgUtil.SaveImageWithThumbnail(art, path, fileName, 135, 216);
                    }
                    context.UpdateArtworkImage(artId, fileName);
                    
                    imgUtil.DeleteImage(artwork.ArtImage, path);
                    imgUtil.DeleteImage("thumb-" + artwork.ArtImage, path);

                    artwork.ArtImage = fileName; 
                }
                
                var fullPath = string.Format("{0}{1}", GetImagePath.GetDomain(), GetImagePath.GetArtImagePath());

                return Json(new { 
                                    dataImage = string.Format("{0}/{1}", fullPath, artwork.ArtImage),
                                    dataThumb = string.Format("{0}/thumb-{1}", fullPath, artwork.ArtImage)
                                });
            }
            catch (Exception ex)
            {
                throw new HttpException(400, "Can not update the art. Error: " + ex.Message);
            }
        }

        [HttpPost]
        public JsonResult DeleteArtwork(int artId)
        {
            var context = new NewGenerationDataService();
            try
            {
                var art = context.GetArtwork(artId);
                var imgUtil = new ImageUtils();
                imgUtil.DeleteImage(art.ArtImage, GetMapPath(GetImagePath.GetArtImagePath()));
                imgUtil.DeleteImage("thumb-" + art.ArtImage, GetMapPath(GetImagePath.GetArtImagePath()));
                context.DeleteArtwork(artId);
                ClearDefaultArtistCache(context, art.ArtistId);
                return Json(new { data = "deleted successfully" });
            }
            catch (ResourceNotFoundException)
            {
                throw new HttpException(404, "");
            }
            catch (Exception ex)
            {
                throw new HttpException(400, "Can not delete the art. Error: " + ex.Message);
            }
        }

        [HttpPost]
        public JsonResult DeleteAllArtworks(int artistId)
        {
            var context = new NewGenerationDataService();
            try
            {
                var arts = context.GetArtworks(artistId).Select(i => i.Id).ToList();
                var removedArts = context.DeleteArtworks(arts);
                var imgUtil = new ImageUtils();
                var path = GetMapPath(GetImagePath.GetArtImagePath());
                foreach (var art in removedArts)
                {
                    imgUtil.DeleteImage(art, path);
                    imgUtil.DeleteImage("thumb-" + art, path);
                }

                ClearDefaultArtistCache(context, artistId);
                return Json(arts.Count);
            }
            catch
            {
                throw new HttpException(400, "");
            }
        }

        private void ClearDefaultArtistCache(NewGenerationDataService context, int artistId)
        {
            var artist = context.GetArtist(artistId);
            if(artist.DefaultArtist.HasValue && artist.DefaultArtist.Value)
                System.Web.HttpContext.Current.Cache.Remove(CacheUtils.DefaultArtistCache);
        }

        private ArtistPanelVM GetArtists(ArtistSearchCriteria criteria, int page)
        {
            if (criteria != null)
            {
                SessionHelper.SetSession(SessionHelper.ArtistsSession, criteria);
            }
            else
            {
                if(SessionHelper.GetSession(SessionHelper.ArtistsSession) == null) 
                {
                    criteria = new ArtistSearchCriteria { SearchName = string.Empty };
                    SessionHelper.SetSession(SessionHelper.ArtistsSession, criteria);
                }
                else
                {
                    criteria = (ArtistSearchCriteria)SessionHelper.GetSession(SessionHelper.ArtistsSession);
                }                                    
            }

            var context = new NewGenerationDataService();
            var artists = context.GetArtistByCriteria(criteria);
            var viewmodel = new ArtistPanelVM();
            var urlBase = string.Format("{0}{1}", GetImagePath.GetDomain(), GetImagePath.GetArtistImagePath());

            viewmodel.PageInfo = PagingUtils.Paging(artists.Count(), 10, page);
            viewmodel.Artists = artists.ToList().GetRange(viewmodel.PageInfo.StartRecord - 1, viewmodel.PageInfo.EndRecord + 1 - viewmodel.PageInfo.StartRecord)
                                        .Select(a => new ArtistBase
                                        {
                                            Id = a.Id,
                                            FullName = a.FullName,
                                            PhoneNo = a.PhoneNo,
                                            DefaultArtist = a.DefaultArtist.Value,
                                            Avatar = String.Format("{0}/{1}", urlBase, a.ArtistImage)
                                        }).ToList();

            return viewmodel;
        }

        private ArtworkVM GetArtworks(int artistId, int page = 1)
        {
            var context = new NewGenerationDataService();
            var artist = context.GetArtistAndWorks(artistId);
            var urlBase = string.Format("{0}{1}", GetImagePath.GetDomain(), GetImagePath.GetArtImagePath());
            var vm = new ArtworkVM
                        {
                            ArtistId = artistId,
                            ArtistName = artist.FullName
                        };

            vm.PageInfo = PagingUtils.Paging(artist.ArtWorks.Count, 10, page);
            vm.Artworks = artist.ArtWorks.ToList()
                                .GetRange(vm.PageInfo.StartRecord - 1,
                                          vm.PageInfo.EndRecord + 1 - vm.PageInfo.StartRecord)
                                .Select(a => new ArtWorkBase
                                {
                                    Id = a.Id,
                                    ArtDesc = a.ArtDesc,
                                    ArtImageUrl = string.Format("{0}/{1}", urlBase, a.ArtImage),
                                    ThumbnailUrl = string.Format("{0}/thumb-{1}", urlBase, a.ArtImage),
                                    ShowOnIntroScreen = a.ShowOnIntroScreen.HasValue && a.ShowOnIntroScreen.Value
                                }).ToList();

            return vm;
        }
        private void UpdateArtistAvatar(Artist artist, HttpPostedFileBase avatar)
        {
            var imgUtil = new ImageUtils();
            string fileName = "";
            try
            {
                fileName = artist.FullName.ToLower().Replace(' ', '-') + DateTime.Now.ToString("ddMMyyyyHHmmss");
                fileName = fileName.Length > 80 ? fileName.Substring(fileName.Length - 80) : fileName;
                fileName += avatar.FileName.Substring(avatar.FileName.LastIndexOf('.'));
                imgUtil.SaveImage(avatar, GetMapPath(GetImagePath.GetArtistImagePath()), fileName);

                if (artist.ArtistImage != "default_avatar.jpg")
                    imgUtil.DeleteImage(artist.ArtistImage, GetMapPath(GetImagePath.GetArtistImagePath()));

                artist.ArtistImage = fileName;
            }
            catch (Exception ex)
            {
                imgUtil.DeleteImage(fileName, GetMapPath(GetImagePath.GetArtistImagePath()));
                throw ex;
            }
        }

        private string GetMapPath(string path)
        {
            return Server.MapPath(string.Format("~{0}", path));
        }

        private string GenerateGuidString()
        {
            var guid = Guid.NewGuid();
            var guidStr = guid.ToString();
            guidStr = guidStr.Replace("+","");
            guidStr = guidStr.Replace("=", "");
            return guidStr;
        }
    }
}