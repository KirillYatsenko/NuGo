using System;
using System.Threading.Tasks;
using EventWebScrapper.Services;
using UnidecodeSharpFork;

namespace EventWebScrapper.Scrappers
{
    public class EventImageScrapper : IEventImageScrapper
    {
        private readonly IFileDownloaderService _fileDownloaderService;
        public EventImageScrapper(IFileDownloaderService fileDownloaderService)
        {
            _fileDownloaderService = fileDownloaderService;
        }
        public async Task<string> ScrapImage(string imageUrl, string eventTitle)
        {
            if (string.IsNullOrWhiteSpace(imageUrl))
            {
                throw new ArgumentNullException(nameof(imageUrl));
            }

            if (string.IsNullOrWhiteSpace(eventTitle))
            {
                throw new ArgumentNullException(nameof(eventTitle));
            }

            var imagePrefixUnicode = eventTitle.Replace(" ", "_").Unidecode();

            var imagePath = await _fileDownloaderService.DownloadFile(imageUrl, imagePrefixUnicode);

            return imagePath;
        }
        
    }
}