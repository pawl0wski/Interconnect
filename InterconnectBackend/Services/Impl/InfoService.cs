using Models;
using System.Runtime.InteropServices;

namespace Services.Impl
{
    public class InfoService : IInfoService
    {
        InformationModel IInfoService.GetSystemInfo()
        {
            return new InformationModel
            {
                OsDescription = RuntimeInformation.OSDescription,
                OsArch = RuntimeInformation.OSArchitecture.ToString(),
            };
        }
    }
}
