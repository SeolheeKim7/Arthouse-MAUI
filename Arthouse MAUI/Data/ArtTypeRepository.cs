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
    public class ArtTypeRepository : IArtTypeRepository
    {
        readonly HttpClient client = new HttpClient();

        public ArtTypeRepository()
        {
            client.BaseAddress = Jeeves.DBUri;
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<List<ArtType>> GetArtTypes()
        {
            var response = await client.GetAsync("api/artTypes");
            if (response.IsSuccessStatusCode)
            {
                List<ArtType> artTypes = await response.Content.ReadAsAsync<List<ArtType>>();
                return artTypes;
            }
            else
            {
                var ex = Jeeves.CreateApiException(response);
                throw ex;
            }

        }

        public async Task<ArtType> GetArtType(int ID)
        {
            var response = await client.GetAsync($"api/artTypes/{ID}");
            if (response.IsSuccessStatusCode)
            {
                ArtType artType = await response.Content.ReadAsAsync<ArtType>();
                return artType;
            }
            else
            {
                var ex = Jeeves.CreateApiException(response);
                throw ex;
            }
        }
        public async Task AddArtType(ArtType artTypeToAdd)
        {
            var response = await client.PostAsJsonAsync("api/artTypes", artTypeToAdd);
            if (!response.IsSuccessStatusCode)
            {
                var ex = Jeeves.CreateApiException(response);
                throw ex;
            }
        }

        public async Task UpdateArtType(ArtType artTypeToUpdate)
        {
            var response = await client.PutAsJsonAsync($"api/artTypes/{artTypeToUpdate.ID}", artTypeToUpdate);
            if (!response.IsSuccessStatusCode)
            {
                var ex = Jeeves.CreateApiException(response);
                throw ex;
            }
        }

        public async Task DeleteArtType(ArtType artTypeToDelete)
        {
            var response = await client.DeleteAsync($"api/artTypes/{artTypeToDelete.ID}");
            if (!response.IsSuccessStatusCode)
            {
                var ex = Jeeves.CreateApiException(response);
                throw ex;
            }

        }

    }
}
