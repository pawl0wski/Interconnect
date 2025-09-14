using Models.Database;
using Models.DTO;

namespace Mappers
{
    public static class VirtualSwitchEntityMapper
    {
        public static VirtualSwitchEntityDTO MapToDTO(VirtualSwitchEntityModel model)
        {
            return new VirtualSwitchEntityDTO
            {
                Id = model.Id,
                Name = model.Name,
                X = model.X,
                Y = model.Y
            };
        }
    }
}
