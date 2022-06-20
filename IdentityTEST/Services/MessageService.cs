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
    public class MessageService : IMessageService
    {
        private readonly HttpClient _httpClient;

        public MessageService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task DeleteMessage(int id)
        {
            await _httpClient.DeleteAsync($"api/message/{id}");
        }

        public async Task<IEnumerable<Message>> GetAllMessages()
        {
            return await JsonSerializer.DeserializeAsync<IEnumerable<Message>>(
                await _httpClient.GetStreamAsync($"api/message"),
                new JsonSerializerOptions() { PropertyNameCaseInsensitive = true }
                );
        }

        public async Task<Message> GetMessageDetails(int id)
        {
            return await JsonSerializer.DeserializeAsync<Message>(
              await _httpClient.GetStreamAsync($"api/message/{id}"),
              new JsonSerializerOptions() { PropertyNameCaseInsensitive = true }
              );
        }

        public async Task SaveMessage(Message message)
        {
            var clientJson = new StringContent(JsonSerializer.Serialize(message),
              Encoding.UTF8, "application/json");

            if (message.id_mensaje == 0)
                await _httpClient.PostAsync("api/message", clientJson);
            else
                await _httpClient.PutAsync("api/message", clientJson);
        }
    }
}
