using RskAnalysis.CORE.Models;
using System.Text;
using System.Text.Json;

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

        public async Task<List<Partners>> GetPartnersWithBussinessAndCity()
        {
            var response = await _httpClient.GetAsync("https://localhost:7009/api/Partners/PartnersWithBussinessAndCity");
            response.EnsureSuccessStatusCode();
            var part = await response.Content.ReadFromJsonAsync<List<Partners>>();
            return part;
        }

        public async Task<Partners> GetPartnerById(int id)
        {
            var response = await _httpClient.GetAsync($"https://localhost:7009/api/Partners/PartnersId/{id}");
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


        public async Task<Partners> AddPartner(Partners part)
        {
            part.Business = null;
            part.City = null;
            var response = await _httpClient.PostAsJsonAsync($"https://localhost:7009/api/Partners/AddPartner/{part}", part);
            response.EnsureSuccessStatusCode();
            var par = await response.Content.ReadFromJsonAsync<Partners>();
            return par;
        }

        public async Task<Partners> UpdatePartner(Partners part)
        {
            var response = await _httpClient.PutAsJsonAsync($"https://localhost:7009/api/Partner/UpdatePartner/{part}", part);
            response.EnsureSuccessStatusCode();
            var par = await response.Content.ReadFromJsonAsync<Partners>();
            return par;
        }

        public void DeletePartner(Partners part)
        {
            //var response = await _httpClient.DeleteAsync($"https://localhost:7009/api/Business/DeleteCity/{buss}");
            //response.EnsureSuccessStatusCode();
            //var cty = await response.Content.ReadFromJsonAsync<Cities>();
            //return cty;



            var request = new HttpRequestMessage(HttpMethod.Delete, $"https://localhost:7009/api/Partner/DeletePartner/{part}")
            {
                Content = new StringContent(JsonSerializer.Serialize(part), Encoding.UTF8, "application/json")
            };


            var response = _httpClient.SendAsync(request).Result;
            response.EnsureSuccessStatusCode();
            //var cty = await response.Content.ReadFromJsonAsync<Cities>();
            //return cty;
        }

        
        
    }
}

