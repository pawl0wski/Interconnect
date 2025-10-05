using NativeLibrary.Structs;

namespace NativeLibrary.Wrappers
{
    public interface IPacketSnifferWrapper
    {
        public IntPtr OpenSnifferHandler(string bridgeName);
        public bool ListenForPacket(IntPtr handler, string bridgeName);
        public NativePacket GetPacket();
        public int GetNumberOfPackets();
    }
}
