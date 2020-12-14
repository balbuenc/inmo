using CoreERP.Model;
using CoreERP.UI.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace CoreERP.UI.Services
{
    public class ClientTypeService : IClientTypeService
    {
        private readonly HttpClient _httpClient;
        public ClientTypeService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public Task DeleteClientType(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<ClientType>> GetAllClientTypes()
        {
            return await JsonSerializer.DeserializeAsync<IEnumerable<ClientType>>(
                await _httpClient.GetStreamAsync($"api/clientType"),
                new JsonSerializerOptions() { PropertyNameCaseInsensitive = true }
                );
        }

        public Task<ClientType> GetClientTypeDetails(int id)
        {
            throw new NotImplementedException();
        }

        public Task SaveClientType(ClientType ClientType)
        {
            throw new NotImplementedException();
        }
    }
}
