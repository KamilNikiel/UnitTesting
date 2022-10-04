using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using UnitTesting.Mocking;

namespace UnitTesting.UnitTests.Mocking
{
    [TestFixture]
    internal class InstallerHelperTests
    {
        private InstallerHelper _installerHelper;
        private Mock<IFileDownloader> _fileDownloadService;

        [SetUp]
        public void SetUp()
        {
            _fileDownloadService = new Mock<IFileDownloader>();
            _installerHelper = new InstallerHelper(_fileDownloadService.Object);
        }

        [Test]
        public void DownloadInstaller_DownloadCompletes_ReturnsTrue()
        {
            var result = _installerHelper.DownloadInstaller("CustomerName", "InstallerName");
            
            Assert.That(result, Is.True);
        }
        [Test]
        public void DownloadInstaller_DownloadFails_ReturnsFalse()
        {
            _fileDownloadService.Setup(fds => 
                fds.DownloadFile(It.IsAny<string>(), It.IsAny<string>()))
                .Throws<WebException>();
           
            var result = _installerHelper.DownloadInstaller("CustomerName", "InstallerName");

            Assert.That(result, Is.False);
        }
    }
}
