namespace Models.Database
{
    public class InternetEntityModel
    {
        public int Id { get; set; }
        public required int X { get; set; }
        public required int Y { get; set; }
        public required VirtualNetworkModel VirtualNetwork { get; set; }
    }
}
