using Models.DTO;

namespace Services
{
    public interface IVirtualNetworkService
    {
        Task<VirtualNetworkEntityConnectionDTO> ConnectTwoVirtualMachines(int sourceEntityId, int sourceSocketId, int destinationEntityId, int destinationSocketId);
        Task<List<VirtualNetworkEntityConnectionDTO>> GetVirtualNetworkConnections();
        Task<VirtualSwitchEntityDTO> CreateVirtualSwitch(string? name);
    }
}
