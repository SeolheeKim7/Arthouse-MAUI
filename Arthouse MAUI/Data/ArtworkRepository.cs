using Arthouse_MAUI.Models;
using Arthouse_MAUI.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Arthouse_MAUI.Data
{
    public class ArtworkRepository : IArtworkRepository
    {
        readonly HttpClient client = new HttpClient();

        public ArtworkRepository()
        {
            client.BaseAddress = Jeeves.DBUri;
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }
        public async Task<List<Artwork>> GetArtworks()
        {
            HttpResponseMessage response = await client.GetAsync("api/artworks");
            if (response.IsSuccessStatusCode)
            {
                List<Artwork> artworks = await response.Content.ReadAsAsync<List<Artwork>>();
                return artworks;
            }
            else
            {
                var ex = Jeeves.CreateApiException(response);
                throw ex;
            }
        }

        public async Task<List<Artwork>> GetArtworksByArtType(int ArtTypeID)
        {
            var response = await client.GetAsync($"api/artworks/byARtType/{ArtTypeID}");
            if (response.IsSuccessStatusCode)
            {
                List<Artwork> artworks = await response.Content.ReadAsAsync<List<Artwork>>();
                return artworks;
            }
            else
            {
                var ex = Jeeves.CreateApiException(response);
                throw ex;
            }
        }

        public async Task<Artwork> GetArtwork(int ID)
        {
            var response = await client.GetAsync($"api/artworks/{ID}");
            if (response.IsSuccessStatusCode)
            {
                Artwork Artwork = await response.Content.ReadAsAsync<Artwork>();
                return Artwork;
            }
            else
            {
                var ex = Jeeves.CreateApiException(response);
                throw ex;
            }
        }

        public async Task AddArtwork(Artwork artworkToAdd)
        {
            artworkToAdd.ArtType = null;
            var response = await client.PostAsJsonAsync("api/artworks", artworkToAdd);
            if (!response.IsSuccessStatusCode)
            {
                var ex = Jeeves.CreateApiException(response);
                throw ex;
            }
        }

        public async Task UpdateArtwork(Artwork artworkToUpdate)
        {
            artworkToUpdate.ArtType = null;
            var response = await client.PutAsJsonAsync($"api/artworks/{artworkToUpdate.ID}", artworkToUpdate);
            if (!response.IsSuccessStatusCode)
            {
                var ex = Jeeves.CreateApiException(response);
                throw ex;
            }
        }

        public async Task DeleteArtwork(Artwork artworkToDelete)
        {
            var response = await client.DeleteAsync($"api/artworks/{artworkToDelete.ID}");
            if (!response.IsSuccessStatusCode)
            {
                var ex = Jeeves.CreateApiException(response);
                throw ex;
            }
        }
    }
}
