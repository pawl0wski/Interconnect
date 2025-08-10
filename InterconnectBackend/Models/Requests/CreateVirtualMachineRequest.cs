namespace Controllers.Requests
{
    public class CreateVirtualMachineRequest
    {
        public required string Name { get; set; }
        public required uint Memory { get; set; }
        public required uint VirtualCPUs { get; set; }
        public required int BootableDiskId { get; set; }
    }
}
