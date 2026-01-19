namespace Models.DTO
{
    public class VirtualMachineEntitiesWithMacAddressesDTO
    {
        public required List<VirtualMachineEntityDTO> VirtualMachineEntities { get; set; }
        public required List<VirtualMachineEntityMacAddressDTO> MacAddressEntities { get; set; }
    }
}
