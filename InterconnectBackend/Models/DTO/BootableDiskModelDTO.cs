using Models.Enums;

namespace Models.DTO
{
    public class BootableDiskModelDTO
    {
        public required int Id { get; set; }
        public required string Name { get; set; }
        public string? Version { get; set; }
        public OperatingSystemType OperatingSystemType { get; set; }
    }
}
