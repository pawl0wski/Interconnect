namespace Models
{
    /// <summary>
    /// Information about a virtual machine.
    /// </summary>
    public class VirtualMachineInfo
    {
        /// <summary>
        /// Virtual machine UUID.
        /// </summary>
        public required string Uuid { get; set; }
        
        /// <summary>
        /// Virtual machine name.
        /// </summary>
        public required string Name { get; set; }
        
        /// <summary>
        /// Used memory in bytes.
        /// </summary>
        public required ulong UsedMemory { get; set; }
        
        /// <summary>
        /// Virtual machine state.
        /// </summary>
        public required byte State { get; set; }
    }
}
