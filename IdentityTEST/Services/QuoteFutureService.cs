﻿using CoreERP.Model;
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
    public class QuoteFutureService : IQuoteFutureService
    {
        private readonly HttpClient _httpClient;

        public QuoteFutureService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task DeleteQuote(int id)
        {
            await _httpClient.DeleteAsync($"api/quoteFuture/{id}");
        }

        public async Task<IEnumerable<Quote>> GetAllQuotes()
        {
            return await JsonSerializer.DeserializeAsync<IEnumerable<Quote>>(
                await _httpClient.GetStreamAsync($"api/quoteFuture"),
                new JsonSerializerOptions() { PropertyNameCaseInsensitive = true }
                );
        }

        public async Task<Quote> GetQuoteDetails(int id)
        {
            return await JsonSerializer.DeserializeAsync<Quote>(
              await _httpClient.GetStreamAsync($"api/quoteFuture/{id}"),
              new JsonSerializerOptions() { PropertyNameCaseInsensitive = true }
              );
        }

        public async Task SaveQuote(Quote quote)
        {
            var clientJson = new StringContent(JsonSerializer.Serialize(quote),
              Encoding.UTF8, "application/json");

            if (quote.id_cliente == 0)
                await _httpClient.PostAsync("api/quoteFuture", clientJson);
            else
                await _httpClient.PutAsync("api/quoteFuture", clientJson);
        }
    }
}
