using RskAnalysis.CORE.Models;
using System.Text;
using System.Text.Json;

namespace RskAnalysis.WEBB.Services.SectorsSer
{
    public class SectorsWServices
    {
        private HttpClient _httpClient;

        public SectorsWServices(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<List<Sectors>> GetSectorsAsync()
        {
            var response = await _httpClient.GetAsync("https://localhost:7009/api/Sectors/SectorsList");
            response.EnsureSuccessStatusCode();
            var sec = await response.Content.ReadFromJsonAsync<List<Sectors>>();
            return sec;
        }

        public async Task<Sectors> GetSectorById(int id)
        {
            var response = await _httpClient.GetAsync($"https://localhost:7009/api/Sectors/Sectors/{id}");
            response.EnsureSuccessStatusCode();
            var sec = await response.Content.ReadFromJsonAsync<Sectors>();
            return sec;
        }

        public async Task<Sectors> AddSector(Sectors sect)
        {
            var response = await _httpClient.PostAsJsonAsync($"https://localhost:7009/api/Sectors/AddSector/{sect}", sect);
            response.EnsureSuccessStatusCode();
            var sec = await response.Content.ReadFromJsonAsync<Sectors>();
            return sec;
        }

        public async Task<Sectors> UpdateSector(Sectors sect)
        {
            var response = await _httpClient.PutAsJsonAsync($"https://localhost:7009/api/Sectors/UpdateSector/{sect}", sect);
            response.EnsureSuccessStatusCode();
            var sec = await response.Content.ReadFromJsonAsync<Sectors>();
            return sec;
        }

        public void DeleteSector(Sectors sect)
        {
            //var response = await _httpClient.DeleteAsync($"https://localhost:7009/api/Sectors/DeleteSectors/{sect}");
            //response.EnsureSuccessStatusCode();
            //var cty = await response.Content.ReadFromJsonAsync<Cities>();
            //return cty;



            var request = new HttpRequestMessage(HttpMethod.Delete, $"https://localhost:7009/api/Sectors/DeleteSectors/{sect}")
            {
                Content = new StringContent(JsonSerializer.Serialize(sect), Encoding.UTF8, "application/json")
            };


            var response = _httpClient.SendAsync(request).Result;
            response.EnsureSuccessStatusCode();
            //var cty = await response.Content.ReadFromJsonAsync<Cities>();
            //return cty;
        }
    }
}
