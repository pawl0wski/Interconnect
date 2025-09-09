using Models.Database;
using Models.DTO;

namespace Mappers
{
    public static class InternetEntityMapper
    {
        public static InternetEntityModelDTO MapToDTO(InternetEntityModel model)
        {
            return new InternetEntityModelDTO
            {
                Id = model.Id,
                X = model.X,
                Y = model.Y
            };
        }

    }
}
