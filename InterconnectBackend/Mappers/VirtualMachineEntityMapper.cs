using Models.Database;
using Models.DTO;
using System.Xml.Linq;

namespace Mappers
{
    public static class VirtualMachineEntityMapper
    {
        public static VirtualMachineEntityDTO MapToDTO(VirtualMachineEntityModel model)
        {
            var macAddress = model.DeviceDefinition is null ? null : XElement.Parse(model.DeviceDefinition)
            .Elements("mac")
            .Select(e => e.Attribute("address")?.Value)
            .FirstOrDefault();

            return new VirtualMachineEntityDTO
            {
                Id = model.Id,
                Name = model.Name,
                VmUuid = model.VmUuid,
                Type = model.Type,
                X = model.X,
                Y = model.Y,
                MacAddress = macAddress,
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
