using Models.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models.Database
{
    /// <summary>
    /// Database model representing a virtual machine entity.
    /// </summary>
    public class VirtualMachineEntityModel
    {
        /// <summary>
        /// Entity identifier.
        /// </summary>
        public int Id { get; set; }
        
        /// <summary>
        /// Virtual machine UUID in the hypervisor.
        /// </summary>
        [Column(TypeName = "varchar(255)")]
        public Guid? VmUuid { get; set; }
        
        /// <summary>
        /// Virtual machine name.
        /// </summary>
        [Column(TypeName = "varchar(255)")]
        public required string Name { get; set; }
        
        /// <summary>
        /// Virtual machine entity type.
        /// </summary>
        public VirtualMachineEntityType Type { get; set; } = VirtualMachineEntityType.Host;
        
        /// <summary>
        /// X coordinate of the entity position.
        /// </summary>
        public required int X { get; set; }
        
        /// <summary>
        /// Y coordinate of the entity position.
        /// </summary>
        public required int Y { get; set; }
    }
}
