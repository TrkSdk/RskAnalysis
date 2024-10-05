using RskAnalysis.CORE.Models;
using System.Text;
//using Newtonsoft.Json;
using System.Text.Json;

namespace RskAnalysis.WEBB.Services.RejectedContractsSer
{
    public class RejectedContractsWServices
    {
        private HttpClient _httpClient;

        public RejectedContractsWServices(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<RejectedContracts>> GetRejectedContractsAsync()
        {
            var response = await _httpClient.GetAsync("https://localhost:7009/api/RejectedContracts/RejectedContractsList");
            response.EnsureSuccessStatusCode();
            var rjctdcntrct = await response.Content.ReadFromJsonAsync<List<RejectedContracts>>();
            return rjctdcntrct;
        }

        public async Task<RejectedContracts> GetRejectedContractById(int id)
        {
            var response = await _httpClient.GetAsync($"https://localhost:7009/api/RejectedContracts/RejectedContracts/{id}");
            response.EnsureSuccessStatusCode();
            var rjctdcntrct = await response.Content.ReadFromJsonAsync<RejectedContracts>();
            return rjctdcntrct;
        }

        public async Task<RejectedContracts> AddRejectedContract(RejectedContracts rejectedcontract)
        {
            var response = await _httpClient.PostAsJsonAsync($"https://localhost:7009/api/RejectedContracts/AddRejectedContract/{rejectedcontract}", rejectedcontract);
            response.EnsureSuccessStatusCode();
            var rjctdcntrct = await response.Content.ReadFromJsonAsync<RejectedContracts>();
            return rjctdcntrct;
        }

        public async Task<RejectedContracts> UpdateRejectedContract(RejectedContracts rejectedcontract)
        {
            var response = await _httpClient.PutAsJsonAsync($"https://localhost:7009/api/Contracts/UpdateRejectedContract/{rejectedcontract}", rejectedcontract);
            response.EnsureSuccessStatusCode();
            var rjctdcntrct = await response.Content.ReadFromJsonAsync<RejectedContracts>();
            return rjctdcntrct;
        }


        public void DeleteRejectedContract(RejectedContracts rejectedcontract)
        {
            var request = new HttpRequestMessage(HttpMethod.Delete, $"https://localhost:7009/api/RejectedContracts/DeleteRejectedContract/{rejectedcontract}")
            {
                Content = new StringContent(JsonSerializer.Serialize(rejectedcontract), Encoding.UTF8, "application/json")
            };


            var response = _httpClient.SendAsync(request).Result;
            response.EnsureSuccessStatusCode();
        }
    }
}
