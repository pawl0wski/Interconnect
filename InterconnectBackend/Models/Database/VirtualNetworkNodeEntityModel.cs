using System.ComponentModel.DataAnnotations.Schema;

namespace Models.Database
{
    /// <summary>
    /// Database model representing a virtual network node (switch/router).
    /// </summary>
    public class VirtualNetworkNodeEntityModel
    {
        /// <summary>
        /// Node identifier.
        /// </summary>
        public int Id { get; set; }
        
        /// <summary>
        /// Network node name.
        /// </summary>
        [Column(TypeName = "varchar(255)")]
        public string? Name { get; set; }
        
        /// <summary>
        /// Determines whether the node is visible on the board.
        /// </summary>
        [Column(TypeName = "varchar(128)")]
        public required bool Visible { get; set; }
        
        /// <summary>
        /// X coordinate of the node position.
        /// </summary>
        public int X { get; set; }
        
        /// <summary>
        /// Y coordinate of the node position.
        /// </summary>
        public int Y { get; set; }
        
        /// <summary>
        /// Virtual network to which the node belongs.
        /// </summary>
        public required VirtualNetworkModel VirtualNetwork { get; set; }
    }
}
