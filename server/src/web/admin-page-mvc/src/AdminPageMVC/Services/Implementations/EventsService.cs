using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using AdminPageMVC.ViewModels;
using Microsoft.Extensions.Configuration;

namespace AdminPageMVC.Services
{
    public class EventsService : IEventsService
    {
        private readonly HttpClient _httpClient;
        private readonly string _scrapperApiUrl;

        public EventsService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;

            _scrapperApiUrl = configuration["ScrapperServiceIP"];

            if (string.IsNullOrWhiteSpace(_scrapperApiUrl))
            {
                // throw new ConfigurationErrorsException("ImageDirectory entry can not be found, please check appsettings");
            }
        }

        public async Task<IEnumerable<Event>> GetRelevantAsync()
        {
            var url = $"{_scrapperApiUrl}/events";

            var getResult = await _httpClient.GetAsync(url);

            if (getResult.IsSuccessStatusCode)
            {
                return await getResult.Content.ReadAsAsync<IEnumerable<Event>>();
            }

            throw new HttpRequestException("Failed to GET relevant events");
        }

        public async Task<IEnumerable<Event>> GetHistoryAsync()
        {
                
            var url = $"{_scrapperApiUrl}/events/history";

            var getResult = await _httpClient.GetAsync(url);

            if (getResult.IsSuccessStatusCode)
            {
                return await getResult.Content.ReadAsAsync<IEnumerable<Event>>();
            }

            throw new HttpRequestException("Failed to GET old events");
        }

        public async Task<Event> GetAsync(long id)
        {
            var url = $"{_scrapperApiUrl}/events/{id}";

            var getResult = await _httpClient.GetAsync(url);

            if (getResult.IsSuccessStatusCode)
            {
                return await getResult.Content.ReadAsAsync<Event>();
            }

            throw new HttpRequestException("Failed to GET event");
        }

    }
}