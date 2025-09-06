using Models.Database;
using Models.DTO;

namespace Mappers
{
    public static class VirtualNetworkEntityConnectionMapper
    {
        public static VirtualNetworkEntityConnectionDTO MapToDTO(VirtualNetworkEntityConnectionModel model)
        {
            return new VirtualNetworkEntityConnectionDTO
            {
                Id = model.Id,
                FirstEntityUuid = model.FirstEntityUuid,
                SecondEntityUuid = model.SecondEntityUuid,
            };
        }
    }
}
