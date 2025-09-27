using System.ComponentModel.DataAnnotations.Schema;

namespace Models.Database
{
    public class VirtualNetworkModel
    {
        public int Id { get; set; }
        [Column(TypeName = "varchar(255)")]
        public required string BridgeName { get; set; }
        public required Guid Uuid { get; set; }
        [Column(TypeName = "varchar(15)")]
        public string? IpAddress { get; set; }
        public List<VirtualSwitchEntityModel> VirtualSwitches { get; set; } = [];
        public List<InternetEntityModel> Internets { get; set; } = [];
    }
}
