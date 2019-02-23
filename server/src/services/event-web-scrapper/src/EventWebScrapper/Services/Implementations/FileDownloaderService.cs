using System;
using System.Configuration;
using System.Net;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace EventWebScrapper.Services
{
    public class FileDownloaderService : IFileDownloaderService
    {
        private readonly string _imageDirectoryPath;
        private readonly IFileBrowserService _fileBrowserService;
        public FileDownloaderService(IConfiguration configuration, IFileBrowserService fileBrowserService)
        {
            _fileBrowserService = fileBrowserService;
            _imageDirectoryPath = configuration["ImageDirectory"];

            if (string.IsNullOrWhiteSpace(_imageDirectoryPath))
            {
                throw new ConfigurationErrorsException("ImageDirectory entry can not be found, please check appsettings");
            }
        }

        public async Task<string> DownloadFile(string fileUrl, string filePrefix)
        {
            if (string.IsNullOrWhiteSpace(fileUrl))
            {
                throw new ArgumentNullException($"{nameof(fileUrl)}");
            }

            if (string.IsNullOrWhiteSpace(filePrefix))
            {
                throw new ArgumentNullException($"{nameof(filePrefix)}");
            }

            var path = string.Empty;

            if (_fileBrowserService.EnsureExists(_imageDirectoryPath) == false)
            {
                return path;
            }

            var imagePath = await downloadFile(fileUrl, filePrefix);

            return imagePath;
        }

        private async Task<string> downloadFile(string fileUrl, string filePrefix)
        {
            var fileUri = new Uri(fileUrl);
            var filePath = $"{_imageDirectoryPath}/{filePrefix}_{DateTime.Now.Ticks}";

            using (var webClient = new WebClient())
            {
                await webClient.DownloadFileTaskAsync(fileUri, filePath);
            }

            return filePath;
        }

    }
}