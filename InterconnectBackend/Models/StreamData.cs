namespace Models
{
    public class StreamData
    {
        public required byte[] Data { get; set; }
        public bool IsStreamBroken { get; set; }
    }
}
