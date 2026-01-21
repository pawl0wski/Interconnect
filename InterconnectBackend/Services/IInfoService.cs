using Models;

namespace Services
{
    /// <summary>
    /// Service providing system information.
    /// </summary>
    public interface IInfoService
    {
        /// <summary>
        /// Retrieves operating system information.
        /// </summary>
        /// <returns>System information.</returns>
        public InformationModel GetSystemInfo();
    }
}
