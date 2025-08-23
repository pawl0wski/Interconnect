namespace Models.Requests
{
    public class SendDataToConsoleRequest
    {
        public required string Uuid { get; set; }
        public required string Data { get; set; }
    }
}
