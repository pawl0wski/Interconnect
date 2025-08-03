using Models.Database;
using Models.DTO;

namespace Mappers
{
    public static class BootableDiskModelMapper
    {
        public static BootableDiskModelDTO MapToDTO(BootableDiskModel model)
        {
            return new BootableDiskModelDTO
            {
                Id = model.Id,
                Name = model.Name,
                Version = model.Version,
                OperatingSystemType = model.OperatingSystemType,
            };
        }
    }
}
