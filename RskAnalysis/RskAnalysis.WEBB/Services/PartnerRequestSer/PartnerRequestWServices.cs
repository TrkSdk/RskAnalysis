using RskAnalysis.CORE.Models;
using System.Net.Http;

namespace RskAnalysis.WEBB.Services.PartnerRiskSer
{
    public class PartnerRequestWServices
    {
        private readonly HttpClient _httpClient;

        public PartnerRequestWServices(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<PartnerRequest> TakePartnerRequest(PartnerRequest partnerRequest)
        {

            var response = await _httpClient.PostAsJsonAsync($"https://localhost:7009/api/PartnerRequest/TakePartnerRequest/{partnerRequest}", partnerRequest);
            response.EnsureSuccessStatusCode();
            var partReq = await response.Content.ReadFromJsonAsync<PartnerRequest>();
            return partReq;
        }
    }
}
