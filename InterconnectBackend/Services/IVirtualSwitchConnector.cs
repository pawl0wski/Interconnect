using Models.DTO;

namespace Services
{
    public interface IVirtualSwitchConnector
    {
        public Task<VirtualNetworkConnectionDTO> ConnectTwoVirtualSwitches(int sourceVirtualSwitch, int destinationVirtualSwitch);
        public Task DisconnectTwoVirtualSwitches(int connectionId, int firstVirtualSwitchId, int secondVirtualSwitch);
    }
}
