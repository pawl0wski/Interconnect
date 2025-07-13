namespace Models
{
    public class VirtualMachineCreateDefinition
    {
        public required string Name { get; set; }
        public required uint Memory { get; set; }
        public required uint VirtualCpus { get; set; }
        public required string BootableDiskPath { get; set; }
    }
}
