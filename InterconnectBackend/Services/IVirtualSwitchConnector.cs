using Models.DTO;

namespace Services
{
    public interface IVirtualSwitchConnector
    {
        public Task<VirtualNetworkConnectionDTO> ConnectTwoVirtualSwitches(int sourceVirtualSwitch, int destinationVirtualSwitch);
        public Task DisconnectTwoVirtualSwitches(int connectionId, int firstVirtualSwitchId, int secondVirtualSwitch);
        public Task<VirtualNetworkConnectionDTO> ConnectVirtualSwitchToInternet(int virtualSwitchId, int internetId);
        public Task DisconnectVirtualSwitchFromInternet(int connectionId, int virtualSwitchId);
    }
}
