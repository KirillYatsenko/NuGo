using System;
using System.IO;
using System.Threading.Tasks;

namespace EventWebScrapper.Services
{
    public class FileBrowserService : IFileBrowserService
    {
        public bool EnsureExists(string path)
        {
            if (string.IsNullOrWhiteSpace(path))
            {
                return false;
            }

            try
            {
                createDirectory(path);
            }
            catch (Exception    )
            {
                return false;
            }

            return true;
        }

        private bool createDirectory(string path)
        {
            if (Directory.Exists(path))
            {
                return true;
            }

            Directory.CreateDirectory(path);

            return true;
        }

    }
}