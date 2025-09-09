namespace Models.Database
{
    public class InternetEntityModel
    {
        public int Id { get; set; }
        public required string BridgeName { get; set; }
        public required Guid Uuid { get; set; }
        public required int X { get; set; }
        public required int Y { get; set; }
    }
}
