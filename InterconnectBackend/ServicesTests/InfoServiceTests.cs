using Services;
using Services.Impl;
using System.Runtime.InteropServices;

namespace ServicesTests
{
    public class InfoServiceTests
    {
        private IInfoService _infoService;

        [SetUp]
        public void Setup()
        {
            _infoService = new InfoService();
        }

        [Test]
        public void GetOperatingSystem_Invoke_ShouldReturnOperatingSystem()
        {
            var info = _infoService.GetSystemInfo();

            Assert.That(info.OsDescription, Is.EqualTo(RuntimeInformation.OSDescription));
            Assert.That(info.OsArch, Is.EqualTo(RuntimeInformation.OSArchitecture.ToString()));
        }
    }
}