using CoreERP.Model;
using CoreERP.UI.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace CoreERP.UI.Services
{
    public class FractionService :IFractionService
    {
        private readonly HttpClient _httpClient;

        public FractionService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task DeleteFraction(int id)
        {
            await _httpClient.DeleteAsync($"api/fraction/{id}");
        }

        public async Task<IEnumerable<Fraction>> GetAllFractions()
        {
            return await JsonSerializer.DeserializeAsync<IEnumerable<Fraction>>(
                await _httpClient.GetStreamAsync($"api/fraction"),
                new JsonSerializerOptions() { PropertyNameCaseInsensitive = true }
                );
        }

        public async Task<Fraction> GetFractionDetails(int id)
        {
            return await JsonSerializer.DeserializeAsync<Fraction>(
              await _httpClient.GetStreamAsync($"api/fraction/{id}"),
              new JsonSerializerOptions() { PropertyNameCaseInsensitive = true }
              );
        }

        public async Task SaveFraction(Fraction fraction)
        {
            var clientJson = new StringContent(JsonSerializer.Serialize(fraction),
              Encoding.UTF8, "application/json");

            if (fraction.id == 0)
                await _httpClient.PostAsync("api/fraction", clientJson);
            else
                await _httpClient.PutAsync("api/fraction", clientJson);
        }
    }
}
