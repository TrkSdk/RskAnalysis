using Elfie.Serialization;
using Humanizer;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.CodeAnalysis.Diagnostics;
using RskAnalysis.CORE.Models;
using RskAnalysis.WEBB.Controllers;
using RskAnalysis.WEBB.Services.ContractsSer;
using RskAnalysis.WEBB.Services.RejectedContractsSer;
using System;
using System.Diagnostics.Contracts;
using System.Net.Http;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace RskAnalysis.WEBB.Services.PartnerRiskSer
{
    public class PartnerRequestWServices
    {
        private HttpClient _httpClient;
        private RejectedContractsWServices _rejectedContractsWServices;
        private ContractsWServices _contractWServices;

        public PartnerRequestWServices(HttpClient httpClient, RejectedContractsWServices rejectedContractsWServices, ContractsWServices contractWServices)
        {
            _httpClient = httpClient;
            _rejectedContractsWServices = rejectedContractsWServices;
            _contractWServices = contractWServices;
        }

        public async Task<Contracts> TakePartnerRequest(Contracts contract)
        {

            contract.Partner = null;

            var response = await _httpClient.PostAsJsonAsync($"https://localhost:7009/api/PartnerRequest/TakePartnerRequest/{contract}", contract);
            response.EnsureSuccessStatusCode();
            var cntrct = await response.Content.ReadFromJsonAsync<Contracts>(); 
            return cntrct;
        }

        public async Task<Contracts> AddPartnerContract(Contracts contract)
        {
            contract.Partner = null;

            var response = await _httpClient.PostAsJsonAsync($"https://localhost:7009/api/PartnerRequest/AddPartnerContract/{contract}", contract);
            response.EnsureSuccessStatusCode();

            //var cntrct = await response.Content.ReadFromJsonAsync<Contracts>();

            //return cntrct;

            return contract;
        }

        public async Task<Contracts> AddRejectedPartnerContract(Contracts rejectedcontract)
        {
            rejectedcontract.Partner = null;

            var response = await _httpClient.PostAsJsonAsync($"https://localhost:7009/api/PartnerRequest/AddRejectedPartnerContract/{rejectedcontract}", rejectedcontract);
            response.EnsureSuccessStatusCode();
            var rjctdcntrct = await response.Content.ReadFromJsonAsync<Contracts>();
            return rjctdcntrct;
        }
    }
}
