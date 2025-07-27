using System.ComponentModel.DataAnnotations.Schema;

namespace Models.Database
{
    public class VirtualMachineEntityModel
    {
        public int Id { get; set; }
        [Column(TypeName = "varchar(255)")]
        public Guid? VmUuid { get; set; }
        [Column(TypeName = "varchar(255)")]
        public required string Name { get; set; }
        public required int X { get; set; }
        public required int Y { get; set; }
    }
}
