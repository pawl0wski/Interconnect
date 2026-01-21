using Models;

namespace Services
{
    /// <summary>
    /// Service managing virtual machine console.
    /// </summary>
    public interface IVirtualMachineConsoleService
    {
        /// <summary>
        /// Retrieves initial console data from a virtual machine.
        /// </summary>
        /// <param name="uuid">Virtual machine UUID.</param>
        /// <returns>Initial console data.</returns>
        public byte[] GetInitialConsoleData(Guid uuid);
        
        /// <summary>
        /// Opens a virtual machine console stream.
        /// </summary>
        /// <param name="uuid">Virtual machine UUID.</param>
        /// <returns>Opened stream information.</returns>
        public StreamInfo OpenVirtualMachineConsole(Guid uuid);
        
        /// <summary>
        /// Sends bytes to a virtual machine console.
        /// </summary>
        /// <param name="uuid">Virtual machine UUID.</param>
        /// <param name="bytes">Bytes to send in text format.</param>
        public void SendBytesToVirtualMachineConsoleByUuid(Guid uuid, string bytes);
        
        /// <summary>
        /// Retrieves data from a console stream.
        /// </summary>
        /// <param name="stream">Stream information.</param>
        /// <returns>Stream data.</returns>
        public StreamData GetBytesFromConsole(StreamInfo stream);
        
        /// <summary>
        /// Retrieves a list of all opened streams.
        /// </summary>
        /// <returns>List of opened streams.</returns>
        public List<StreamInfo> GetStreams();
        
        /// <summary>
        /// Closes a console stream.
        /// </summary>
        /// <param name="stream">Stream information to close.</param>
        public void CloseStream(StreamInfo stream);
    }
}
