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
    public class RuleService :IRuleService
    {
        private readonly HttpClient _httpClient;

        public RuleService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task DeleteRule(int id)
        {
            await _httpClient.DeleteAsync($"api/rule/{id}");
        }

        public async Task<IEnumerable<Rule>> GetAllRules()
        {
            return await JsonSerializer.DeserializeAsync<IEnumerable<Rule>>(
                await _httpClient.GetStreamAsync($"api/rule"),
                new JsonSerializerOptions() { PropertyNameCaseInsensitive = true }
                );
        }

        public async Task<Rule> GetRuleDetails(int id)
        {
            return await JsonSerializer.DeserializeAsync<Rule>(
              await _httpClient.GetStreamAsync($"api/rule/{id}"),
              new JsonSerializerOptions() { PropertyNameCaseInsensitive = true }
              );
        }

        public async Task SaveRule(Rule rule)
        {
            var clientJson = new StringContent(JsonSerializer.Serialize(rule),
              Encoding.UTF8, "application/json");

            if (rule.id_regla == 0)
                await _httpClient.PostAsync("api/rule", clientJson);
            else
                await _httpClient.PutAsync("api/rule", clientJson);
        }
    }
}
