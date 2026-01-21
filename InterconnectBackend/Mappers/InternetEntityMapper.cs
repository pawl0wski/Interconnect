using Models.Database;
using Models.DTO;

namespace Mappers
{
    /// <summary>
    /// Mapper for converting Internet entity models.
    /// </summary>
    public static class InternetEntityMapper
    {
        /// <summary>
        /// Maps database model of Internet entity to DTO.
        /// </summary>
        /// <param name="model">Database model of Internet entity.</param>
        /// <returns>DTO of Internet entity.</returns>
        public static InternetEntityModelDTO MapToDTO(InternetEntityModel model)
        {
            return new InternetEntityModelDTO
            {
                Id = model.Id,
                X = model.X,
                Y = model.Y,
                IpAddress = model.VirtualNetwork.IpAddress!
            };
        }

    }
}
