namespace Models
{
    public class VirtualMachineInfo
    {
        public required string Uuid { get; set; }
        public required string Name { get; set; }
        public required ulong UsedMemory { get; set; }
        public required byte State { get; set; }
    }
}
