using Services.Impl;

namespace ServicesTests
{
    public class Tests
    {

        [Test]
        public void GetVersion_Invoke_ShouldReturnCorrectVersion()
        {
            var versionService = new VersionService();

            Assert.That(versionService.GetVersion(), Is.EqualTo("1.0"));
        }
    }
}