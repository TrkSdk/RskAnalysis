using RskAnalysis.CORE.Models;
using System.Text;
//using Newtonsoft.Json;
using System.Text.Json;

namespace RskAnalysis.WEBB.Services.CitiesSer
{
    public class CitiesWServices
    {
        private readonly HttpClient _httpClient;

        public CitiesWServices(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<List<Cities>> GetCitiesAsync()
        {
            var response = await _httpClient.GetAsync("https://localhost:7009/api/Cities/CitiesList");
            response.EnsureSuccessStatusCode();
            var cty = await response.Content.ReadFromJsonAsync<List<Cities>>();
            return cty;
        }

        public async Task<Cities> GetCityById(int id)
        {
            var response = await _httpClient.GetAsync($"https://localhost:7009/api/Cities/Cities/{id}");
            response.EnsureSuccessStatusCode();
            
            var cty = await response.Content.ReadFromJsonAsync<Cities>();
            
            return cty;
        }

        public async Task<Cities> AddCity(Cities city)
        {
            var response = await _httpClient.PostAsJsonAsync($"https://localhost:7009/api/Cities/AddCity/{city}", city);
            response.EnsureSuccessStatusCode();
            var cty = await response.Content.ReadFromJsonAsync<Cities>();
            return cty;
        }

        public async Task<Cities> UpdateCity(Cities city)
        {
            var response = await _httpClient.PutAsJsonAsync($"https://localhost:7009/api/Cities/UpdateCity/{city}", city);
            response.EnsureSuccessStatusCode();
            var cty = await response.Content.ReadFromJsonAsync<Cities>();
            return cty;
        }

        public void DeleteCity(Cities city)
        {
            var request = new HttpRequestMessage(HttpMethod.Delete, $"https://localhost:7009/api/Cities/DeleteCity/{city}")
            {
                Content = new StringContent(JsonSerializer.Serialize(city), Encoding.UTF8, "application/json")
            };


            var response = _httpClient.SendAsync(request).Result;
            response.EnsureSuccessStatusCode();
        }
    }
}
