namespace Models.Database
{
    public class VirtualNetworkModel
    {
        public int Id { get; set; }
        public required string BridgeName { get; set; }
        public required Guid Uuid { get; set; }
        public List<VirtualSwitchEntityModel> VirtualSwitches { get; set; } = [];
        public List<InternetEntityModel> Internets { get; set; } = [];
    }
}
