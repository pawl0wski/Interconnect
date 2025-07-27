namespace Models.DTO
{
    public class VirtualMachineEntityDTO
    {
        public required int Id { get; set; }
        public Guid? VmUuid { get; set; }
        public required string Name { get; set; }
        public required int X { get; set; }
        public required int Y { get; set; }
    }
}
