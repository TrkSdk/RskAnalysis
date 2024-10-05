using RskAnalysis.CORE.Models;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;


namespace RskAnalysis.WEBB.Services.ContractsSer
{
    public class ContractsWServices
    {
        private HttpClient _httpClient;

        public ContractsWServices(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<Contracts>> GetContractAsync()
        {
            var response = await _httpClient.GetAsync("https://localhost:7009/api/Contracts/ContractsList");
            response.EnsureSuccessStatusCode();
            
            var cntrct = await response.Content.ReadFromJsonAsync<List<Contracts>>();
            
            return cntrct;
        }

        public async Task<Contracts> GetContractById(int id)
        {
            var response = await _httpClient.GetAsync($"https://localhost:7009/api/Contracts/ContractsID/{id}");
            response.EnsureSuccessStatusCode();

            var cntrct = await response.Content.ReadFromJsonAsync<Contracts>();

            return cntrct;
        }


        public async Task<List<Contracts>> GetContractByIdWithPartner(int id)
        {
            var response = await _httpClient.GetAsync($"https://localhost:7009/api/Contracts/ContractsIDWithPartner/{id}");
            response.EnsureSuccessStatusCode();
            
            var cntrct = await response.Content.ReadFromJsonAsync<List<Contracts>>();
            
            return cntrct;
        }


        public async Task<Contracts> AddContract(Contracts contract)
        {
            contract.Partner = null;
            
            var response = await _httpClient.PostAsJsonAsync($"https://localhost:7009/api/Contracts/AddContracts/{contract}", contract);
            response.EnsureSuccessStatusCode();
            var cntrct = await response.Content.ReadFromJsonAsync<Contracts>();
            return cntrct;
        }

        public async Task<Contracts> UpdateContract(Contracts contract)
        {
            var response = await _httpClient.PutAsJsonAsync($"https://localhost:7009/api/Contracts/UpdateContracts/{contract.ContractId}", contract);
            response.EnsureSuccessStatusCode();

            var cntrct = await response.Content.ReadAsStringAsync();
            var res = string.IsNullOrEmpty(cntrct) ? null : JsonObject.Parse(cntrct);
            
            return contract;
        }


        public void DeleteContract(Contracts contract)
        {
            var request = new HttpRequestMessage(HttpMethod.Delete, $"https://localhost:7009/api/Contracts/DeleteContract/{contract}")
            {
                Content = new StringContent(JsonSerializer.Serialize(contract), Encoding.UTF8, "application/json")
            };


            var response = _httpClient.SendAsync(request).Result;
            response.EnsureSuccessStatusCode();
        }

        public async Task<List<Contracts>> GetContractWithPart()
        {
            var response = await _httpClient.GetAsync("https://localhost:7009/api/Contracts/ContractsWithPartner");
            response.EnsureSuccessStatusCode();
            
            var cntrct = await response.Content.ReadFromJsonAsync<List<Contracts>>();

            return cntrct;
        }

    }
}
