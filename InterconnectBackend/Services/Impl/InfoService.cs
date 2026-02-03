using Models;
using System.Runtime.InteropServices;

namespace Services.Impl
{
    /// <summary>
    /// Service providing system information.
    /// </summary>
    public class InfoService : IInfoService
    {
        /// <summary>
        /// Retrieves operating system information.
        /// </summary>
        /// <returns>System information.</returns>
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
