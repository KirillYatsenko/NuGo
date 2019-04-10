using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using AdminPageMVC.ViewModels;
using Microsoft.Extensions.Configuration;

namespace AdminPageMVC.Services
{
    public class EventCategoryService : IEventsCategoryService
    {
        private readonly HttpClient _httpClient;
        private readonly string _scrapperApiUrl;
        public EventCategoryService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;

            _scrapperApiUrl = configuration["ScrapperServiceIP"];

            if (string.IsNullOrWhiteSpace(_scrapperApiUrl))
            {
                // throw new ConfigurationErrorsException("ImageDirectory entry can not be found, please check appsettings");
            }
        }
        public async Task<IEnumerable<EventCategory>> GetAsync()
        {
            var url = $"{_scrapperApiUrl}/categories";

            var getResult = await _httpClient.GetAsync(url);

            if (getResult.IsSuccessStatusCode)
            {
                return await getResult.Content.ReadAsAsync<IEnumerable<EventCategory>>();
            }

            throw new HttpRequestException("Failed to GET events");
        }
    }
}