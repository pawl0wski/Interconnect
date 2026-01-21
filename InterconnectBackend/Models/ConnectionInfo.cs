namespace Models
{
    /// <summary>
    /// Information about the connection to the virtualization hypervisor.
    /// </summary>
    public class ConnectionInfo
    {
        /// <summary>
        /// Number of CPUs.
        /// </summary>
        public uint CpuCount { get; set; }
        
        /// <summary>
        /// CPU frequency in MHz.
        /// </summary>
        public uint CpuFreq { get; set; }
        
        /// <summary>
        /// Total system memory in bytes.
        /// </summary>
        public long TotalMemory { get; set; }
        
        /// <summary>
        /// Connection URL to the hypervisor.
        /// </summary>
        public required string ConnectionUrl { get; set; }
        
        /// <summary>
        /// Virtualization driver type.
        /// </summary>
        public required string DriverType { get; set; }
        
        /// <summary>
        /// Libvirt library version.
        /// </summary>
        public required string LibVersion { get; set; }
        
        /// <summary>
        /// Virtualization driver version.
        /// </summary>
        public required string DriverVersion { get; set; }
    }
}
