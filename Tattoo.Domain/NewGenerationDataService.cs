using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tattoo.Domain.Models;
using Tattoo.Domain.Exceptions;

namespace Tattoo.Domain
{
    public class NewGenerationDataService : ITattooDataService
    {
        public IEnumerable<Artist> GetArtists()
        {
            using (var context = new TattooDBEntities())
            {
                var artists = context.Artists;
                return artists.ToList();
            }
        }

        public Artist GetArtist(int id)
        {
            using (var context = new TattooDBEntities())
            {
                var artist = context.Artists.SingleOrDefault(i => i.Id == id);
                if (artist == null) throw new ResourceNotFoundException();
                return artist;
            }
        }
        public Artist GetArtistAndWorks(int artistId)
        {
            using (var context = new TattooDBEntities())
            {
                var artist = context.Artists.Include("ArtWorks").SingleOrDefault(i => i.Id == artistId);
                if (artist == null) throw new ResourceNotFoundException("Can not found this artist");
                return artist;
            }
        }

        public IEnumerable<Artist> GetArtistByCriteria(ArtistSearchCriteria criteria)
        {
            using (var context = new TattooDBEntities())
            {
                var artists = context.Artists.Include("ArtWorks")
                                     .Where(i => i.FullName.Contains(criteria.SearchName) &&
                                            (criteria.IsDefault == null || i.DefaultArtist == criteria.IsDefault));
                return artists.ToList();
            }
        }

        public IEnumerable<ArtWork> GetArtworks(int artistId)
        {
            using (var context = new TattooDBEntities())
            {
                var artist = context.Artists.SingleOrDefault(i => i.Id == artistId);
                if (artist == null) throw new ResourceNotFoundException("Can not found this artist");
                return artist.ArtWorks.ToList();
            }
        }

        public ArtWork GetArtwork(int id)
        {
            using (var context = new TattooDBEntities())
            {
                var art = context.ArtWorks.SingleOrDefault(i => i.Id == id);
                if (art == null) throw new ResourceNotFoundException();
                return art;
            }
        }

        public IEnumerable<ArtWork> GetOnScreenArtworks()
        {
            using (var context = new TattooDBEntities())
            {
                var artWorks = context.ArtWorks.Where(i => i.ShowOnIntroScreen == true);
                return artWorks.ToList();
            }
        }

        public CompanyProfile GetCompanyProfile()
        {
            using (var context = new TattooDBEntities())
            {
                var profile = context.CompanyProfiles.FirstOrDefault();
                if (profile == null) throw new ResourceNotFoundException();
                return profile;
            }
        }

        public IEnumerable<SocialConnection> GetSocialonnections()
        {
            using (var context = new TattooDBEntities())
            {
                var connections = context.SocialConnections;
                return connections.ToList();
            }
        }

        public SocialConnection GetSocialConnection(int id)
        {
            using (var context = new TattooDBEntities())
            {
                var connection = context.SocialConnections.SingleOrDefault(i => i.Id == id);
                if (connection == null) throw new ResourceNotFoundException();
                return connection;
            }
        }

        public Artist UpdateArtist(Artist artist)
        {
            using (var context = new TattooDBEntities())
            {
                var data = context.Artists.SingleOrDefault(i => i.Id == artist.Id);

                if (data == null) throw new ResourceNotFoundException("The artist does not or no longer exist.");

                data.BriefIntro = artist.BriefIntro;
                data.DefaultArtist = artist.DefaultArtist;
                data.FullName = artist.FullName;
                data.NameKey = artist.FullName.ToLower().Replace(" ", "-");
                data.PhoneNo = artist.PhoneNo;
                data.ArtistImage = artist.ArtistImage;

                context.ChangeTracker.DetectChanges();
                context.SaveChanges();

                return data;
            }
        }

        public ArtWork UpdateArtwork(ArtWork artWork)
        {
            using (var context = new TattooDBEntities())
            {
                var art = context.ArtWorks.SingleOrDefault(i => i.Id == artWork.Id);

                if (art == null) throw new ResourceNotFoundException("The art work does not or no longer exist.");

                art.ArtDesc = artWork.ArtDesc;
                art.ShowOnIntroScreen = artWork.ShowOnIntroScreen;

                context.ChangeTracker.DetectChanges();
                context.SaveChanges();

                return art;
            }
        }

        public CompanyProfile UpdateCompanyProfile(CompanyProfile profile)
        {
            using (var context = new TattooDBEntities())
            {
                var companyProfile = context.CompanyProfiles.FirstOrDefault();

                if (companyProfile == null) throw new ResourceNotFoundException();

                companyProfile.CompanyAddress = profile.CompanyAddress;
                companyProfile.ContactNumber = profile.ContactNumber;
                companyProfile.Intro = profile.Intro;
                companyProfile.Title = profile.Title;

                context.ChangeTracker.DetectChanges();
                context.SaveChanges();

                return companyProfile;
            }
        }



        public void UpdateArtistImage(int id, string image)
        {
            using (var context = new TattooDBEntities())
            {
                var artist = context.Artists.SingleOrDefault(i => i.Id == id);

                if (artist == null) throw new ResourceNotFoundException();

                artist.ArtistImage = image;

                context.ChangeTracker.DetectChanges();
                context.SaveChanges();
            }
        }

        public void UpdateArtworkImage(int id, string image)
        {
            using (var context = new TattooDBEntities())
            {
                var art = context.ArtWorks.SingleOrDefault(i => i.Id == id);

                if (art == null) throw new ResourceNotFoundException();

                art.ArtImage = image;

                context.ChangeTracker.DetectChanges();
                context.SaveChanges();
            }
        }

        public void UpdateProfileImage(string image)
        {
            using (var context = new TattooDBEntities())
            {
                var profile = context.CompanyProfiles.FirstOrDefault();

                if (profile == null) throw new ResourceNotFoundException();

                profile.HomepageImage = image;

                context.ChangeTracker.DetectChanges();
                context.SaveChanges();
            }
        }


        public SocialConnection UpdateSocialConnection(SocialConnection connection)
        {
            using (var context = new TattooDBEntities())
            {
                var con = context.SocialConnections.SingleOrDefault(i => i.Id == connection.Id);

                if (con == null) throw new ResourceNotFoundException();

                con.NetworkName = connection.NetworkName;
                con.ProfileLink = connection.ProfileLink;
                con.MainImage = string.IsNullOrWhiteSpace(connection.MainImage) ? con.MainImage : connection.MainImage;
                con.HoverImage = string.IsNullOrWhiteSpace(connection.HoverImage) ? con.HoverImage : connection.HoverImage;

                context.ChangeTracker.DetectChanges();
                context.SaveChanges();

                return con;
            }
        }

        public Artist CreateArtist(Artist artist)
        {
            using (var context = new TattooDBEntities())
            {
                artist.NameKey = artist.FullName.ToLower().Replace(" ", "-");

                context.Artists.Add(artist);
                context.SaveChanges();

                return artist;
            }
        }

        public ArtWork CreateArtwork(ArtWork artWork, int artistId)
        {
            using (var context = new TattooDBEntities())
            {
                var artist = context.Artists.SingleOrDefault(i => i.Id == artistId);

                if (artist == null) throw new ResourceNotFoundException("The artist does not or no longer exist");

                artWork.ArtistId = artistId;

                context.ArtWorks.Add(artWork);
                context.SaveChanges();

                return artWork;
            }
        }

        public IEnumerable<ArtWork> CreateArtworks(IEnumerable<ArtWork> artworks, int artistId)
        {
            using (var context = new TattooDBEntities())
            {
                var artist = context.Artists.SingleOrDefault(i => i.Id == artistId);

                if (artist == null) throw new ResourceNotFoundException("The artist does not or no longer exist");

                foreach (var art in artworks)
                {
                    art.ArtistId = artistId;
                }

                context.ArtWorks.AddRange(artworks);
                context.SaveChanges();

                return artworks;
            }
        }

        public SocialConnection CreateSocialConnection(SocialConnection connection)
        {
            using (var context = new TattooDBEntities())
            {
                context.SocialConnections.Add(connection);
                context.SaveChanges();

                return connection;
            }
        }

        public string DeleteArtist(int id)
        {
            using (var context = new TattooDBEntities())
            {
                var artist = context.Artists.SingleOrDefault(i => i.Id == id);

                if (artist == null) throw new ResourceNotFoundException();

                if (artist.ArtWorks.Count > 0) throw new CanNotDeleteException("This artist is linked with one or some art works.");

                context.Artists.Remove(artist);
                context.SaveChanges();
                return artist.ArtistImage;
            }
        }

        public string DeleteArtwork(int id)
        {
            using (var context = new TattooDBEntities())
            {
                var art = context.ArtWorks.SingleOrDefault(i => i.Id == id);

                if (art == null) throw new ResourceNotFoundException();

                context.ArtWorks.Remove(art);
                context.SaveChanges();
                return art.ArtImage;
            }
        }

        public IEnumerable<string> DeleteArtworks(IEnumerable<int> ids)
        {
            using (var context = new TattooDBEntities())
            {
                var removedArts = new List<string>();
                foreach (var id in ids)
                {
                    var art = context.ArtWorks.SingleOrDefault(i => i.Id == id);
                    if (art != null) 
                    { 
                        removedArts.Add(art.ArtImage);
                        context.ArtWorks.Remove(art);
                    }
                }

                context.SaveChanges();
                return removedArts;
            }
        }

        public string DeleteSocialConnection(int id)
        {
            using (var context = new TattooDBEntities())
            {
                var connection = context.SocialConnections.SingleOrDefault(i => i.Id == id);

                if (connection == null) throw new ResourceNotFoundException();

                context.SocialConnections.Remove(connection);
                context.SaveChanges();
                return string.Format("{0}|{1}", connection.MainImage, connection.HoverImage);
            }
        }


        public IEnumerable<CustomerFeedback> GetCustomerFeedbacks(FeedbackSearchCriteria criteria)
        {
            using (var context = new TattooDBEntities())
            {
                var feedbacks = context.CustomerFeedbacks.Where(i => i.SentDate >= criteria.SentDateFrom 
                                                                && i.SentDate <= criteria.SentDateTo);
                return feedbacks.ToList();
            }
        }

        public CustomerFeedback GetCustomerFeedback(int id)
        {
            using (var context = new TattooDBEntities())
            {
                var feedback = context.CustomerFeedbacks.SingleOrDefault(i => i.Id == id);

                if (feedback == null) throw new ResourceNotFoundException("The resource does not or no longer exist.");

                return feedback;
            }
        }

        public CustomerFeedback UpdateCustomerFeedback(int id, bool isRead)
        {
            using (var context = new TattooDBEntities())
            {
                var feedback = context.CustomerFeedbacks.SingleOrDefault(i => i.Id == id);

                if (feedback == null) throw new ResourceNotFoundException("The resource does not or no longer exist.");

                feedback.IsRead = true;
                context.ChangeTracker.DetectChanges();
                context.SaveChanges();

                return feedback;
            }
        }

        public CustomerFeedback CreateCustomerFeedback(CustomerFeedback feedback)
        {
            using (var context = new TattooDBEntities())
            {
                context.CustomerFeedbacks.Add(feedback);
                context.SaveChanges();
                return feedback;
            }
        }

        public void DeleteCustomerFeedback(int id)
        {
            using (var context = new TattooDBEntities())
            {
                var feedback = context.CustomerFeedbacks.SingleOrDefault(i => i.Id == id);

                if (feedback != null)
                {
                    context.CustomerFeedbacks.Remove(feedback);
                    context.SaveChanges();
                }
            }
        }



        public Credential GetCredential(string name, string password)
        {
            using (var context = new TattooDBEntities())
            {
                var credential = context.Credentials.SingleOrDefault(i => i.UserName == name && i.Password == password);
                return credential;
            }
        }

        public Credential UpdateCredentialPassword(string name, string newPassword)
        {
            throw new NotImplementedException();
        }

        public Credential CreateCredential(Credential credential)
        {
            throw new NotImplementedException();
        }

        public void DeleteCredential(string name)
        {
            throw new NotImplementedException();
        }
    }
}
