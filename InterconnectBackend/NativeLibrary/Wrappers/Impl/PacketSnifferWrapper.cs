using NativeLibrary.Interop;
using NativeLibrary.Structs;

namespace NativeLibrary.Wrappers.Impl
{
    public class PacketSnifferWrapper : IPacketSnifferWrapper
    {
        private readonly IntPtr _sniffer;

        public PacketSnifferWrapper()
        {
            _sniffer = InteropPacketSniffer.CreatePacketSniffer();
        }

        public int GetNumberOfPackets()
        {
            int numberOfPackets;
            NativeExecutionInfo executionInfo = new();

            InteropPacketSniffer.PacketSniffer_GetNumberOfPackets(out executionInfo, _sniffer, out numberOfPackets);

            ExecutionInfoAnalyzer.ThrowIfErrorOccurred(executionInfo);
            return numberOfPackets;
        }

        public NativePacket GetPacket()
        {
            NativePacket packet;
            NativeExecutionInfo executionInfo;

            InteropPacketSniffer.PacketSniffer_GetPacket(out executionInfo, _sniffer, out packet);

            ExecutionInfoAnalyzer.ThrowIfErrorOccurred(executionInfo);
            return packet;
        }

        public IntPtr OpenSnifferHandler(string bridgeName)
        {
            NativeExecutionInfo executionInfo;

            var handler = InteropPacketSniffer.PacketSniffer_OpenSnifferHandler(out executionInfo, _sniffer, bridgeName);

            ExecutionInfoAnalyzer.ThrowIfErrorOccurred(executionInfo);

            return handler;
        }

        public bool ListenForPacket(nint handler, string bridgeName)
        {
            NativeExecutionInfo executionInfo;

            var output = InteropPacketSniffer.PacketSniffer_ListenForPacket(out executionInfo, _sniffer, bridgeName, handler);

            ExecutionInfoAnalyzer.ThrowIfErrorOccurred(executionInfo);

            return output;
        }
    }
}
