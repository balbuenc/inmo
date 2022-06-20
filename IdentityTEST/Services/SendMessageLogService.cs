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
    public class SendMessageLogService : ISendMessageLogService
    {
        private readonly HttpClient _httpClient;

        public SendMessageLogService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task DeleteSendMessageLog(int id)
        {
            await _httpClient.DeleteAsync($"api/SendMessageLog/{id}");
        }

        public async Task<IEnumerable<SendMessageLog>> GetAllSendMessageLogs()
        {
            return await JsonSerializer.DeserializeAsync<IEnumerable<SendMessageLog>>(
                await _httpClient.GetStreamAsync($"api/SendMessageLog"),
                new JsonSerializerOptions() { PropertyNameCaseInsensitive = true }
                );
        }

        public async Task<SendMessageLog> GetSendMessageLogDetails(int id)
        {
            return await JsonSerializer.DeserializeAsync<SendMessageLog>(
              await _httpClient.GetStreamAsync($"api/SendMessageLog/{id}"),
              new JsonSerializerOptions() { PropertyNameCaseInsensitive = true }
              );
        }

        public async Task SaveSendMessageLog(SendMessageLog SendMessageLog)
        {
            var clientJson = new StringContent(JsonSerializer.Serialize(SendMessageLog),
              Encoding.UTF8, "application/json");

            if (SendMessageLog.id_envio_mensaje == 0)
                await _httpClient.PostAsync("api/SendMessageLog", clientJson);
            else
                await _httpClient.PutAsync("api/SendMessageLog", clientJson);
        }
    }
}
