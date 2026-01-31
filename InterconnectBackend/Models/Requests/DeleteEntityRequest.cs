using Models.Enums;

namespace Models.Requests
{
    public class DeleteEntityRequest
    {
        public required int Id { get; set; }
        public required EntityType Type { get; set; }
    }
}
