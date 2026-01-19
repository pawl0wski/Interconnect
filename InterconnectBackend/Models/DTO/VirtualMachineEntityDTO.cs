using Models.Enums;

namespace Models.DTO
{
    public class VirtualMachineEntityDTO : BaseEntity
    {
        public Guid? VmUuid { get; set; }
        public required string Name { get; set; }
        public VirtualMachineEntityType Type { get; set; } = VirtualMachineEntityType.Host;
        public VirtualMachineState State { get; set; } = VirtualMachineState.Unknown;
    }
}
