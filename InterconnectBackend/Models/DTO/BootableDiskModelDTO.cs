namespace Models.DTO
{
    public class BootableDiskModelDTO
    {
        public required int Id { get; set; }
        public required string Name { get; set; }
        public string? Version { get; set; }
    }
}
