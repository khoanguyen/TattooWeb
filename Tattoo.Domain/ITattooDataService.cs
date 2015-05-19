using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tattoo.Domain.Models;

namespace Tattoo.Domain
{
    public interface ITattooDataService
    {
        IEnumerable<Artist> GetArtists();

        Artist GetArtist(int id);
        Artist GetArtistAndWorks(int id);
        IEnumerable<Artist> GetArtistByCriteria(ArtistSearchCriteria criteria);
        IEnumerable<ArtWork> GetArtworks(int artistId);
        IEnumerable<ArtWork> GetOnScreenArtworks();
        ArtWork GetArtwork(int id);
        CompanyProfile GetCompanyProfile();
        IEnumerable<SocialConnection> GetSocialonnections();
        SocialConnection GetSocialConnection(int id);
        IEnumerable<CustomerFeedback> GetCustomerFeedbacks(FeedbackSearchCriteria criteria);
        CustomerFeedback GetCustomerFeedback(int id);
        Credential GetCredential(string name, string password);


        Artist UpdateArtist(Artist artist);
        void UpdateArtistImage(int id, string image);
        ArtWork UpdateArtwork(ArtWork artWork);
        void UpdateArtworkImage(int id, string image);
        CompanyProfile UpdateCompanyProfile(CompanyProfile profile);
        void UpdateProfileImage(string image);
        SocialConnection UpdateSocialConnection(SocialConnection connection);
        CustomerFeedback UpdateCustomerFeedback(int id, bool isRead);
        Credential UpdateCredentialPassword(string name, string newPassword);

        Artist CreateArtist(Artist artist);
        ArtWork CreateArtwork(ArtWork artWork, int artistId);
        IEnumerable<ArtWork> CreateArtworks(IEnumerable<ArtWork> artworks, int artistId);
        SocialConnection CreateSocialConnection(SocialConnection connection);
        CustomerFeedback CreateCustomerFeedback(CustomerFeedback feedback);
        Credential CreateCredential(Credential credential);

        string DeleteArtist(int id);
        string DeleteArtwork(int id);
        IEnumerable<string> DeleteArtworks(IEnumerable<int> ids);
        string DeleteSocialConnection(int id);
        void DeleteCustomerFeedback(int id);
        void DeleteCredential(string name);
    }
}
