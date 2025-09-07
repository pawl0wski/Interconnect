using Models.Enums;

namespace Models.DTO
{
    public class VirtualNetworkConnectionDTO
    {
        public required int Id { get; set; }
        public required int SourceEntityId { get; set; }
        public required EntityType SourceEntityType { get; set; }
        public required int DestinationEntityId { get; set; }
        public required EntityType DestinationEntityType { get; set; }
    }
}
