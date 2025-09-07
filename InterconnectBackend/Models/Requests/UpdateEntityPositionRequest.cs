namespace Models.Requests
{
    public class UpdateEntityPositionRequest
    {
        public required int Id { get; set; }
        public required int X { get; set; }
        public required int Y { get; set; }
    }
}
