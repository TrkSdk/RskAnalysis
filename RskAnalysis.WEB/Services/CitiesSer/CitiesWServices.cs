using RskAnalysis.CORE.Models;

namespace RskAnalysis.WEB.Services.CitiesSer
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
    }
}
