using System.ComponentModel.DataAnnotations.Schema;

namespace Models.Database
{
    /// <summary>
    /// Database model representing a bootable disk.
    /// </summary>
    public class BootableDiskModel
    {
        /// <summary>
        /// Disk identifier.
        /// </summary>
        public required int Id { get; set; }
        
        /// <summary>
        /// Disk name.
        /// </summary>
        [Column(TypeName = "varchar(255)")]
        public required string Name { get; set; }
        
        /// <summary>
        /// Operating system version on the disk.
        /// </summary>
        [Column(TypeName = "varchar(32)")]
        public string? Version { get; set; }
        
        /// <summary>
        /// Path to the disk file.
        /// </summary>
        [Column(TypeName = "varchar(255)")]
        public string? Path { get; set; }
    }
}
