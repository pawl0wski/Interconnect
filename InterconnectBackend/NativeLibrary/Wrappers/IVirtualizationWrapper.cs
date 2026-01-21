using NativeLibrary.Structs;
using NativeLibrary.Utils;

namespace NativeLibrary.Wrappers
{
    /// <summary>
    /// Interface wrapper for native virtualization library.
    /// </summary>
    public interface IVirtualizationWrapper
    {
        /// <summary>
        /// Initializes connection with the hypervisor.
        /// </summary>
        /// <param name="connectionUrl">Connection URL or null for default.</param>
        public void InitializeConnection(string? connectionUrl);
        
        /// <summary>
        /// Gets connection information from the hypervisor.
        /// </summary>
        /// <returns>Connection information.</returns>
        public NativeConnectionInfo GetConnectionInfo();
        
        /// <summary>
        /// Creates a virtual machine based on XML definition.
        /// </summary>
        /// <param name="xmlDefinition">Machine definition in XML format.</param>
        public void CreateVirtualMachine(string xmlDefinition);
        
        /// <summary>
        /// Gets information about a virtual machine.
        /// </summary>
        /// <param name="name">Name of the virtual machine.</param>
        /// <returns>Virtual machine information.</returns>
        public NativeVirtualMachineInfo GetVirtualMachineInfo(string name);
        
        /// <summary>
        /// Gets a list of all virtual machines.
        /// </summary>
        /// <returns>List of virtual machines.</returns>
        public INativeList<NativeVirtualMachineInfo> GetListOfVirtualMachines();
        
        /// <summary>
        /// Checks if the connection with the hypervisor is active.
        /// </summary>
        /// <returns>True if connection is active.</returns>
        public bool IsConnectionAlive();
        
        /// <summary>
        /// Opens a console stream for a virtual machine.
        /// </summary>
        /// <param name="uuid">UUID of the virtual machine.</param>
        /// <returns>Pointer to the console stream.</returns>
        public IntPtr OpenVirtualMachineConsole(Guid uuid);
        
        /// <summary>
        /// Gets data from the console stream.
        /// </summary>
        /// <param name="stream">Pointer to the stream.</param>
        /// <returns>Data from the stream.</returns>
        public NativeStreamData GetDataFromStream(IntPtr stream);
        
        /// <summary>
        /// Sends data to the console stream.
        /// </summary>
        /// <param name="stream">Pointer to the stream.</param>
        /// <param name="data">Data to send.</param>
        public void SendDataToStream(IntPtr stream, string data);
        
        /// <summary>
        /// Closes the console stream.
        /// </summary>
        /// <param name="stream">Pointer to the stream.</param>
        public void CloseStream(IntPtr stream);
        
        /// <summary>
        /// Creates a virtual network based on definition.
        /// </summary>
        /// <param name="networkDefinition">Network definition in XML format.</param>
        /// <returns>Pointer to the created network.</returns>
        public IntPtr CreateVirtualNetwork(string networkDefinition);
        
        /// <summary>
        /// Attaches a device to a virtual machine.
        /// </summary>
        /// <param name="uuid">UUID of the virtual machine.</param>
        /// <param name="deviceDefinition">Device definition in XML format.</param>
        public void AttachDeviceToVirtualMachine(Guid uuid, string deviceDefinition);
        
        /// <summary>
        /// Detaches a device from a virtual machine.
        /// </summary>
        /// <param name="uuid">UUID of the virtual machine.</param>
        /// <param name="deviceDefinition">Device definition in XML format.</param>
        public void DetachDeviceFromVirtualMachine(Guid uuid, string deviceDefinition);
        
        /// <summary>
        /// Updates a device on a virtual machine.
        /// </summary>
        /// <param name="uuid">UUID of the virtual machine.</param>
        /// <param name="deviceDefinition">Device definition in XML format.</param>
        public void UpdateVmDevice(Guid uuid, string deviceDefinition);
        
        /// <summary>
        /// Destroys a virtual network.
        /// </summary>
        /// <param name="name">Name of the network to destroy.</param>
        public void DestroyNetwork(string name);
    }
}
