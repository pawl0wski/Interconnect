using Models.Enums;

namespace Models.Requests
{
    public class ConnectEntitiesRequest
    {
        public required int SourceEntityId { get; set; }
        public EntityType SourceEntityType { get; set; }
        public required int DestinationEntityId { get; set; }
        public EntityType DestinationEntityType { get; set; }
    }
}
