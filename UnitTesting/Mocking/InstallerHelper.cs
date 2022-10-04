using System;
using System.Net;

namespace UnitTesting.Mocking
{
    public class InstallerHelper
    {
        private readonly IFileDownloader _fileDownloader;
        private string _setupDestinationFile;

        public InstallerHelper(IFileDownloader fileDownloadService)
        {
            _fileDownloader = fileDownloadService;
        }
        public bool DownloadInstaller(string customerName, string installerName)
        {
            try
            {
                _fileDownloader.DownloadFile(
                    $"http://example.com/{customerName}/{installerName}",
                    _setupDestinationFile);
                return true;
            }
            catch (WebException)
            {
                return false;
            }
        }

    }
}