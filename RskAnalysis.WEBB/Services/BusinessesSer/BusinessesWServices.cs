﻿using RskAnalysis.CORE.Models;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;

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
        public async Task<List<Businesses>> GetBusinessByIdWithSector(int id)
        {
            var response = await _httpClient.GetAsync($"https://localhost:7009/api/Businesses/BusinessesIDWithSector/{id}");
            response.EnsureSuccessStatusCode();
            
            var bus = await response.Content.ReadFromJsonAsync<List<Businesses>>();
            
            return bus;
        }
        
        public async Task<Businesses> AddBusiness(Businesses buss)
        {
            buss.Sector = null;
            
            var response = await _httpClient.PostAsJsonAsync($"https://localhost:7009/api/Businesses/AddBusinesses/{buss}", buss);
            response.EnsureSuccessStatusCode();
            
            var bus = await response.Content.ReadFromJsonAsync<Businesses>();
            
            return bus;
        }

        public async Task<Businesses> UpdateBusiness(Businesses business)
        {
            business.Sector = null;

            var response = await _httpClient.PutAsJsonAsync($"https://localhost:7009/api/Businesses/UpdateBusinesses/{business.BusinessId}", business);
            response.EnsureSuccessStatusCode();

            var bus = await response.Content.ReadAsStringAsync();
            var res = string.IsNullOrEmpty(bus) ? null : JsonObject.Parse(bus);

            return business;

        }

        public void DeleteBusiness(Businesses bussiness)
        {
            var request = new HttpRequestMessage(HttpMethod.Delete, $"https://localhost:7009/api/Businesses/DeleteBusiness/{bussiness}")
            {
                Content = new StringContent(JsonSerializer.Serialize(bussiness), Encoding.UTF8, "application/json")
            };


            var response = _httpClient.SendAsync(request).Result;
            response.EnsureSuccessStatusCode();


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
