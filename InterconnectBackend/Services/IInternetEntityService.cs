using Models.DTO;

namespace Services
{
    public interface IInternetEntityService
    {
        Task<InternetEntityModelDTO> CreateInternet();
        Task<List<InternetEntityModelDTO>> GetInternetEntities();
        Task<InternetEntityModelDTO> UpdateInternetEntityPosition(int entityId, int x, int y);
    }
}
