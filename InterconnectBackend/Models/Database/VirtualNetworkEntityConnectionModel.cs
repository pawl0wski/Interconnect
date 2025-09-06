namespace Models.Database
{
    public class VirtualNetworkEntityConnectionModel
    {
        public int Id { get; set; }
        public required string BridgeName { get; set; }
        public required Guid FirstEntityUuid { get; set; }
        public required Guid SecondEntityUuid { get; set; }
    }
}
