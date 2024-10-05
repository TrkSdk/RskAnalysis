using RskAnalysis.CORE.Models;
using System.Text;
//using Newtonsoft.Json;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace RskAnalysis.WEBB.Services.RejectedContractsSer
{
    public class RejectedContractsWServices
    {
        private HttpClient _httpClient;

        public RejectedContractsWServices(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<Contracts>> GetRejectedContractsAsync()
        {
            var response = await _httpClient.GetAsync("https://localhost:7009/api/RejectedContracts/RejectedContractsList");
            response.EnsureSuccessStatusCode();
            
            var rjctdcntrct = await response.Content.ReadFromJsonAsync<List<Contracts>>();
            return rjctdcntrct;
        }

        public async Task<Contracts> GetRejectedContractById(int id)
        {
            var response = await _httpClient.GetAsync($"https://localhost:7009/api/RejectedContracts/RejectedContractsID/{id}");
            response.EnsureSuccessStatusCode();
            var rjctdcntrct = await response.Content.ReadFromJsonAsync<Contracts>();
            return rjctdcntrct;
        }


        public async Task<List<Contracts>> GetRejectedContractByIdWithPartner(int id)
        {
            var response = await _httpClient.GetAsync($"https://localhost:7009/api/RejectedContracts/RejectedContractsIDWithPartner/{id}");
            response.EnsureSuccessStatusCode();

            var cntrct = await response.Content.ReadFromJsonAsync<List<Contracts>>();

            return cntrct;
        }


        public async Task<Contracts> AddRejectedContract(Contracts rejectedcontract)
        {
            rejectedcontract.Partner = null;
            
            var response = await _httpClient.PostAsJsonAsync($"https://localhost:7009/api/RejectedContracts/AddRejectedContracts/{rejectedcontract}", rejectedcontract);
            response.EnsureSuccessStatusCode();
            var rjctdcntrct = await response.Content.ReadFromJsonAsync<Contracts>();
            return rjctdcntrct;
        }

        public async Task<Contracts> UpdateRejectedContract(Contracts rejectedcontract)
        {
            var response = await _httpClient.PutAsJsonAsync($"https://localhost:7009/api/RejectedContracts/UpdateRejectedContract/{rejectedcontract.ContractId}", rejectedcontract);
            response.EnsureSuccessStatusCode();

            var rjctdcntrct = await response.Content.ReadAsStringAsync();
            var res = string.IsNullOrEmpty(rjctdcntrct) ? null : JsonObject.Parse(rjctdcntrct);

            return rejectedcontract;

        }


        public void DeleteRejectedContract(Contracts rejectedcontract)
        {
            var request = new HttpRequestMessage(HttpMethod.Delete, $"https://localhost:7009/api/RejectedContracts/DeleteRejectedContract/{rejectedcontract}")
            {
                Content = new StringContent(JsonSerializer.Serialize(rejectedcontract), Encoding.UTF8, "application/json")
            };


            var response = _httpClient.SendAsync(request).Result;
            response.EnsureSuccessStatusCode();
        }

        public async Task<List<Contracts>> GetRejectedContractWithPart()
        {
            var response = await _httpClient.GetAsync("https://localhost:7009/api/RejectedContracts/RejectedContractsWithPartner");
            response.EnsureSuccessStatusCode();

            var cntrct = await response.Content.ReadFromJsonAsync<List<Contracts>>();

            return cntrct;
        }

    }
}
