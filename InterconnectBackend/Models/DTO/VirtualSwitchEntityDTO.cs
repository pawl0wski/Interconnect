namespace Models.DTO
{
    public class VirtualSwitchEntityDTO
    {
        public int Id { get; set; }
        public required string BridgeName { get; set; }
        public Guid Uuid { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
    }
}
