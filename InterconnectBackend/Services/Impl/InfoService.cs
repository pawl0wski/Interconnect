using Models;
using System.Runtime.InteropServices;

namespace Services.Impl
{
    public class InfoService : IInfoService
    {
        InformationModel IInfoService.GetInformation()
        {
            return new(RuntimeInformation.OSDescription, RuntimeInformation.OSArchitecture.ToString());
        }
    }
}
