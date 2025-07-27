namespace Controllers.Requests
{
    public class CreateVirtualMachineEntityRequest
    {
        public required string Name { get; set; }
        public required int X { get; set; }
        public required int Y { get; set; }
    }
}
