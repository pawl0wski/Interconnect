using System.ComponentModel.DataAnnotations.Schema;

namespace Models.Database
{
    public class VirtualSwitchEntityModel
    {
        public int Id { get; set; }
        [Column(TypeName = "varchar(255)")]
        public string? Name { get; set; }
        [Column(TypeName = "varchar(128)")]
        public required string BridgeName { get; set; }
        public required Guid Uuid { get; set; }
        public required bool Visible { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
    }
}
