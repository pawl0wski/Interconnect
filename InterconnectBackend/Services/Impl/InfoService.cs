using Models;
using System.Runtime.InteropServices;

namespace Services.Impl
{
    public class InfoService : IInfoService
    {
        public InformationModel GetSystemInfo()
        {
            return new InformationModel
            {
                OsDescription = RuntimeInformation.OSDescription,
                OsArch = RuntimeInformation.OSArchitecture.ToString(),
            };
        }
    }
}
