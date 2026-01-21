using System.ComponentModel.DataAnnotations.Schema;

namespace Models.Database
{
    /// <summary>
    /// Database model representing a virtual network.
    /// </summary>
    public class VirtualNetworkModel
    {
        /// <summary>
        /// Network identifier.
        /// </summary>
        public int Id { get; set; }
        
        /// <summary>
        /// Network bridge name.
        /// </summary>
        [Column(TypeName = "varchar(255)")]
        public required string BridgeName { get; set; }
        
        /// <summary>
        /// Unique network UUID identifier.
        /// </summary>
        public required Guid Uuid { get; set; }
        
        /// <summary>
        /// Network IP address.
        /// </summary>
        [Column(TypeName = "varchar(15)")]
        public string? IpAddress { get; set; }
        
        /// <summary>
        /// List of virtual network nodes.
        /// </summary>
        public List<VirtualNetworkNodeEntityModel> VirtualNetworkNodes { get; set; } = [];
        
        /// <summary>
        /// List of Internet entities connected to the network.
        /// </summary>
        public List<InternetEntityModel> Internets { get; set; } = [];
    }
}
