using Models.Database;
using Models.DTO;

namespace Mappers
{
    public static class VirtualNetworkEntityConnectionMapper
    {
        public static VirtualNetworkConnectionDTO MapToDTO(VirtualNetworkEntityConnectionModel model)
        {
            return new VirtualNetworkConnectionDTO
            {
                Id = model.Id,
                SourceEntityId = model.SourceEntityId,
                SourceEntityType = model.SourceEntityType,
                DestinationEntityId = model.DestinationEntityId,
                DestinationEntityType = model.DestinationEntityType,
            };
        }
    }
}
