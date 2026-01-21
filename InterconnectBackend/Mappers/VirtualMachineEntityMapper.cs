using Models.Database;
using Models.DTO;

namespace Mappers
{
    /// <summary>
    /// Mapper for converting virtual machine entity models.
    /// </summary>
    public static class VirtualMachineEntityMapper
    {
        /// <summary>
        /// Maps database model of virtual machine entity to DTO.
        /// </summary>
        /// <param name="model">Database model of virtual machine entity.</param>
        /// <returns>DTO of virtual machine entity.</returns>
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

        /// <summary>
        /// Maps DTO of virtual machine entity to database model.
        /// </summary>
        /// <param name="dto">DTO of virtual machine entity.</param>
        /// <returns>Database model of virtual machine entity.</returns>
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
