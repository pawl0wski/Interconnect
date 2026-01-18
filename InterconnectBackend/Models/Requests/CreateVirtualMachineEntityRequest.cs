using Models.Enums;

namespace Models.Requests
{
    public class CreateVirtualMachineEntityRequest
    {
        public required string Name { get; set; }
        public required uint Memory { get; set; }
        public required uint VirtualCPUs { get; set; }
        public required int BootableDiskId { get; set; }
        public required VirtualMachineEntityType Type { get; set; }
    }
}
