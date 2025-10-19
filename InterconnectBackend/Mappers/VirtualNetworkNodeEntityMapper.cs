using Models.Database;
using Models.DTO;

namespace Mappers
{
    public static class VirtualNetworkNodeEntityMapper
    {
        public static VirtualNetworkNodeEntityDTO MapToDTO(VirtualNetworkNodeEntityModel model)
        {
            return new VirtualNetworkNodeEntityDTO
            {
                Id = model.Id,
                Name = model.Name,
                Uuid = model.VirtualNetwork.Uuid,
                X = model.X,
                Y = model.Y
            };
        }
    }
}
