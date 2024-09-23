using RskAnalysis.CORE.Models;

namespace RskAnalysis.WEB.Services.BusinessesSer
{
    public class BusinessesWServices
    {
        private readonly HttpClient _httpClient;

        public BusinessesWServices(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<Businesses>> GetBusinessesList()
        {
            var response = await _httpClient.GetAsync("https://localhost:7067/api/OrderControllers/GetOrderAsync");
            response.EnsureSuccessStatusCode();
            var list = await response.Content.ReadFromJsonAsync<List<Businesses>>();
            return list;
        }
    }
}
