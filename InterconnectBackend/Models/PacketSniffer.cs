namespace Models
{
    public class PacketSniffer
    {
        public required string BridgeName { get; set; }
        public required IntPtr Handler { get; set; }
    }
}
