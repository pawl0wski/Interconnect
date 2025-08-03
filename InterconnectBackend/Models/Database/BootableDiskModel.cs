using Models.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models.Database
{
    public class BootableDiskModel
    {
        public required int Id { get; set; }
        [Column(TypeName = "varchar(255)")]
        public required string Name { get; set; }
        [Column(TypeName = "varchar(32)")]
        public string? Version { get; set; }
        public OperatingSystemType OperatingSystemType { get; set; }
        [Column(TypeName = "varchar(255)")]
        public string? Path { get; set; }
    }
}
