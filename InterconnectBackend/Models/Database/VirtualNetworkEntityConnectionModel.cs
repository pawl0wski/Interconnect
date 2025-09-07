using Models.Enums;

namespace Models.Database
{
    public class VirtualNetworkEntityConnectionModel
    {
        public int Id { get; set; }
        public required int SourceEntityId { get; set; }
        public required EntityType SourceEntityType { get; set; }
        public required int DestinationEntityId { get; set; }
        public required EntityType DestinationEntityType { get; set; }
    }
}
