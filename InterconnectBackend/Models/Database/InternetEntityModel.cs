namespace Models.Database
{
    /// <summary>
    /// Database model representing an Internet entity connected to a virtual network.
    /// </summary>
    public class InternetEntityModel
    {
        /// <summary>
        /// Entity identifier.
        /// </summary>
        public int Id { get; set; }
        
        /// <summary>
        /// X coordinate of the entity position.
        /// </summary>
        public required int X { get; set; }
        
        /// <summary>
        /// Y coordinate of the entity position.
        /// </summary>
        public required int Y { get; set; }
        
        /// <summary>
        /// Virtual network to which the entity is connected.
        /// </summary>
        public required VirtualNetworkModel VirtualNetwork { get; set; }
    }
}
