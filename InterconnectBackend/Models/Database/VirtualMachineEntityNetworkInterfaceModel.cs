namespace Models.Database
{
    /// <summary>
    /// Database model representing a virtual machine network interface.
    /// </summary>
    public class VirtualMachineEntityNetworkInterfaceModel
    {
        /// <summary>
        /// Interface identifier.
        /// </summary>
        public int Id { get; set; }
        
        /// <summary>
        /// Network interface definition in XML format.
        /// </summary>
        public required string Definition { get; set; }
        
        /// <summary>
        /// Virtual machine entity identifier.
        /// </summary>
        public int VirtualMachineEntityId { get; set; }
        
        /// <summary>
        /// Virtual machine entity.
        /// </summary>
        public VirtualMachineEntityModel? VirtualMachineEntity { get; set; }
        
        /// <summary>
        /// Network connection identifier.
        /// </summary>
        public int VirtualNetworkEntityConnectionId { get; set; }
        
        /// <summary>
        /// Network connection.
        /// </summary>
        public VirtualNetworkEntityConnectionModel? VirtualNetworkEntityConnection { get; set; }
    }
}
