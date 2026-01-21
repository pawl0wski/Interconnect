using Models.Enums;

namespace Models.Database
{
    /// <summary>
    /// Database model representing a connection between entities in a virtual network.
    /// </summary>
    public class VirtualNetworkEntityConnectionModel
    {
        /// <summary>
        /// Connection identifier.
        /// </summary>
        public int Id { get; set; }
        
        /// <summary>
        /// Source entity identifier.
        /// </summary>
        public required int SourceEntityId { get; set; }
        
        /// <summary>
        /// Source entity type.
        /// </summary>
        public required EntityType SourceEntityType { get; set; }
        
        /// <summary>
        /// Destination entity identifier.
        /// </summary>
        public required int DestinationEntityId { get; set; }
        
        /// <summary>
        /// Destination entity type.
        /// </summary>
        public required EntityType DestinationEntityType { get; set; }
    }
}
