using Models;

namespace Services
{
    public interface IVirtualMachineConsoleService
    {
        public byte[] GetInitialConsoleData(Guid uuid);
        public StreamInfo OpenVirtualMachineConsole(Guid uuid);
        public void SendBytesToVirtualMachineConsoleByUuid(Guid uuid, string bytes);
        public StreamData GetBytesFromConsole(StreamInfo stream);
        public List<StreamInfo> GetStreams();
        public void CloseStream(StreamInfo stream);
    }
}
