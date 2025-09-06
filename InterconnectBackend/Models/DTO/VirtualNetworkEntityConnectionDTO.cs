namespace Models.DTO
{
    public class VirtualNetworkEntityConnectionDTO
    {
        public required int Id { get; set; }
        public required Guid FirstEntityUuid { get; set; }
        public required Guid SecondEntityUuid { get; set; }
    }
}
