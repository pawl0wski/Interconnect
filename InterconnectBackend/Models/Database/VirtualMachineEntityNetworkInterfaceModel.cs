namespace Models.Database
{
    public class VirtualMachineEntityNetworkInterfaceModel
    {
        public int Id { get; set; }
        public required string Definition { get; set; }
        public int VirtualMachineEntityId { get; set; }
        public VirtualMachineEntityModel? VirtualMachineEntity { get; set; }
        public int VirtualNetworkEntityConnectionId { get; set; }
        public VirtualNetworkEntityConnectionModel? VirtualNetworkEntityConnection { get; set; }
    }
}
