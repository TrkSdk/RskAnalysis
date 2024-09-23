using RskAnalysis.CORE.Models;
using System.Text;
using System.Text.Json;

namespace RskAnalysis.WEBB.Services.BusinessesSer
{
    public class BusinessesWServices
    {
        private readonly HttpClient _httpClient;

        public BusinessesWServices(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<Businesses>> GetBusinessAsync()
        {
            var response = await _httpClient.GetAsync("https://localhost:7009/api/Businesses/BusinessesList");
            response.EnsureSuccessStatusCode();
            var bus = await response.Content.ReadFromJsonAsync<List<Businesses>>();
            return bus;
        }

        public async Task<Businesses> GetBusinessById(int id)
        {
            var response = await _httpClient.GetAsync($"https://localhost:7009/api/Businesses/BusinessesID/{id}");
            response.EnsureSuccessStatusCode();
            var bus = await response.Content.ReadFromJsonAsync<Businesses>();
            return bus;
        }
        public async Task<Businesses> GetBusinessByIdWithSector(int id)
        {
            var response = await _httpClient.GetAsync($"https://localhost:7009/api/Businesses/BusinessesIDWithSector/{id}");
            response.EnsureSuccessStatusCode();
            var bus = await response.Content.ReadFromJsonAsync<Businesses>();
            return bus;
        }
        public async Task<Businesses> AddBusiness(Businesses buss)
        {
            var response = await _httpClient.PostAsJsonAsync($"https://localhost:7009/api/Businesses/AddBusinesses/{buss}", buss);
            response.EnsureSuccessStatusCode();
            var bus = await response.Content.ReadFromJsonAsync<Businesses>();
            return bus;
        }

        public async Task<Businesses> UpdateBusiness(Businesses buss)
        {
            var response = await _httpClient.PutAsJsonAsync($"https://localhost:7009/api/Business/UpdateBusinesses/{buss}", buss);
            response.EnsureSuccessStatusCode();
            var bus = await response.Content.ReadFromJsonAsync<Businesses>();
            return bus;
        }

        public void DeleteBusiness(Businesses buss)
        {
            //var response = await _httpClient.DeleteAsync($"https://localhost:7009/api/Business/DeleteCity/{buss}");
            //response.EnsureSuccessStatusCode();
            //var cty = await response.Content.ReadFromJsonAsync<Cities>();
            //return cty;



            var request = new HttpRequestMessage(HttpMethod.Delete, $"https://localhost:7009/api/Business/DeleteBusinesses/{buss}")
            {
                Content = new StringContent(JsonSerializer.Serialize(buss), Encoding.UTF8, "application/json")
            };


            var response = _httpClient.SendAsync(request).Result;
            response.EnsureSuccessStatusCode();
            //var cty = await response.Content.ReadFromJsonAsync<Cities>();
            //return cty;
        }

        public async Task<List<Businesses>> GetBusinessWithSect()
        {
            var response = await _httpClient.GetAsync("https://localhost:7009/api/Businesses/BusinessesWithSector");
            response.EnsureSuccessStatusCode();
            var bus = await response.Content.ReadFromJsonAsync<List<Businesses>>();
            return bus;
        }
    }
}
