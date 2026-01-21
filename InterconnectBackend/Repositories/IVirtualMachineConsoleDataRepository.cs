namespace Repositories
{
    /// <summary>
    /// Repository managing virtual machine console data in memory.
    /// </summary>
    public interface IVirtualMachineConsoleDataRepository
    {
        /// <summary>
        /// Adds data to a virtual machine console.
        /// </summary>
        /// <param name="uuid">Virtual machine UUID.</param>
        /// <param name="data">Data to add.</param>
        public void AddDataToConsole(Guid uuid, byte[] data);
        
        /// <summary>
        /// Retrieves data from a virtual machine console.
        /// </summary>
        /// <param name="uuid">Virtual machine UUID.</param>
        /// <returns>List of bytes from console.</returns>
        public List<byte> GetData(Guid uuid);
    }
}
