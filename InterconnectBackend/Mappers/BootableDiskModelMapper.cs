using Models.Database;
using Models.DTO;

namespace Mappers
{
    /// <summary>
    /// Mapper for converting bootable disk models.
    /// </summary>
    public static class BootableDiskModelMapper
    {
        /// <summary>
        /// Maps database model to DTO.
        /// </summary>
        /// <param name="model">Database model of bootable disk.</param>
        /// <returns>DTO of bootable disk.</returns>
        public static BootableDiskModelDTO MapToDTO(BootableDiskModel model)
        {
            return new BootableDiskModelDTO
            {
                Id = model.Id,
                Name = model.Name,
                Version = model.Version,
            };
        }
    }
}
