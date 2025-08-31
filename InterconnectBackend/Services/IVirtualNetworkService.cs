using Models;

namespace Services
{
    public interface IVirtualNetworkService
    {
        void CreateVirtualNetwork(VirtualNetworkCreateDefinition definition);
        Task ConnectTwoVirtualMachines(int sourceEntityId, int sourceSocketId, int destinationEntityId, int destinationSocketId);
        void AttachNetworkInterfaceToVirtualMachine(Guid uuid, VirtualNetworkInterfaceCreateDefinition interfaceDefinition);
    }
}
