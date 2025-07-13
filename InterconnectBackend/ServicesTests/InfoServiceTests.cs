using Services;
using Services.Impl;
using System.Runtime.InteropServices;

namespace ServicesTests
{
    public class InfoServiceTests
    {
        [Test]
        public void GetOperatingSystem_Invoke_ShouldReturnOperatingSystem()
        {
            var infoService = new InfoService();
            var info = infoService.GetSystemInfo();

            Assert.That(info.OsDescription, Is.EqualTo(RuntimeInformation.OSDescription));
            Assert.That(info.OsArch, Is.EqualTo(RuntimeInformation.OSArchitecture.ToString()));
        }
    }
}