namespace Models.DTO
{
    public class VirtualMachineEntityMacAddressDTO
    {
        public required int VirtualMachineEntityId { get; set; }
        public List<string> MacAddresses { get; set; } = [];
    }
}
