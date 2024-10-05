using RskAnalysis.CORE.Models;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace RskAnalysis.WEBB.Services.PartnersSer
{
    public class PartnersWServices
    {
        private readonly HttpClient _httpClient;

        public PartnersWServices(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<Partners>> GetPartnersAsync()
        {
            var response = await _httpClient.GetAsync("https://localhost:7009/api/Partners/PartnersList");
            response.EnsureSuccessStatusCode();
            
            var par = await response.Content.ReadFromJsonAsync<List<Partners>>();
            return par;
        }

        public async Task<Partners> GetPartnerById(int id)
        {
            var response = await _httpClient.GetAsync($"https://localhost:7009/api/Partners/PartnerID/{id}");
            response.EnsureSuccessStatusCode();
            
            var par = await response.Content.ReadFromJsonAsync<Partners>();
            
            return par;
        }
        
        public async Task<List<Partners>> GetPartnerByIdWithBussinessAndCity(int id)
        {
            var response = await _httpClient.GetAsync($"https://localhost:7009/api/Partners/PartnersIDWithBussinessAndCity/{id}");
            response.EnsureSuccessStatusCode();
            
            var par = await response.Content.ReadFromJsonAsync<List<Partners>>();

            return par;
        }


        public async Task<List<Partners>> GetPartnersWithBussinessAndCity()
        {
            var response = await _httpClient.GetAsync("https://localhost:7009/api/Partners/PartnersWithBussinessAndCity");
            response.EnsureSuccessStatusCode();
            var part = await response.Content.ReadFromJsonAsync<List<Partners>>();
            
            return part;
        }


        public async Task<Partners> AddPartner(Partners part)
        {
            part.Business = null;
            part.City = null;
            
            var response = await _httpClient.PostAsJsonAsync($"https://localhost:7009/api/Partners/AddPartner/{part}", part);
            response.EnsureSuccessStatusCode();
            
            var par = await response.Content.ReadFromJsonAsync<Partners>();
            
            return par;
        }

        public async Task<Partners> UpdatePartner(Partners partner)
        {
            var response = await _httpClient.PutAsJsonAsync($"https://localhost:7009/api/Partners/UpdatePartner/{partner.PartnerId}", partner);
            response.EnsureSuccessStatusCode();
            
            var par = await response.Content.ReadAsStringAsync();
            var res = string.IsNullOrEmpty(par) ? null : JsonObject.Parse(par);

            return partner;
        }

        public void DeletePartner(Partners part)
        {

            var request = new HttpRequestMessage(HttpMethod.Delete, $"https://localhost:7009/api/Partners/DeletePartner/{part}")
            {
                Content = new StringContent(JsonSerializer.Serialize(part), Encoding.UTF8, "application/json")
            };


            var response = _httpClient.SendAsync(request).Result;
            response.EnsureSuccessStatusCode();
        }
    }
}

