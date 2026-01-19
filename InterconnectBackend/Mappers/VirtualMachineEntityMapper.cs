using Models.Database;
using Models.DTO;

namespace Mappers
{
    public static class VirtualMachineEntityMapper
    {
        public static VirtualMachineEntityDTO MapToDTO(VirtualMachineEntityModel model)
        {
            return new VirtualMachineEntityDTO
            {
                Id = model.Id,
                Name = model.Name,
                VmUuid = model.VmUuid,
                Type = model.Type,
                X = model.X,
                Y = model.Y,
            };
        }

        public static VirtualMachineEntityModel MapFromDTO(VirtualMachineEntityDTO dto)
        {
            return new VirtualMachineEntityModel
            {
                Id = dto.Id,
                Name = dto.Name,
                VmUuid = dto.VmUuid,
                Type = dto.Type,
                X = dto.X,
                Y = dto.Y,
            };
        }
    }
}
