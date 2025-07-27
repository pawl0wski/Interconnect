using Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Models.Config;

namespace TestUtils
{
    public static class TestMocks
    {
        public static IOptions<InterconnectConfig> GetMockConfig()
        {
            return Options.Create(new InterconnectConfig
            {
                DatabaseConnectionUrl = "",
                HypervisorUrl = "test:///testing",
                VmPrefix = "interconnect"
            });
        }

        public static InterconnectDbContext CreateInMemoryDbContext()
        {
            var options = new DbContextOptionsBuilder<InterconnectDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            return new InterconnectDbContext(options);
        }
    }
}
