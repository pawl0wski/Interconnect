using Microsoft.Extensions.Hosting;

namespace Services
{
    internal interface IInitializationService : IHostedService
    {
        public void Initialize();
    }
}
