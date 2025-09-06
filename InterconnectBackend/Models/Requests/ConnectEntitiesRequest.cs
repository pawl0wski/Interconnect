using Models.Enums;

namespace Models.Requests
{
    public class ConnectEntitiesRequest
    {
        public required BaseEntity SourceEntity { get; set; }
        public EntityType SourceEntityType { get; set; }
        public int SourceSocketId { get; set; }
        public required BaseEntity DestinationEntity { get; set; }
        public EntityType DestinationEntityType { get; set; }
        public int DestinationSocketId { get; set; }
    }
}
