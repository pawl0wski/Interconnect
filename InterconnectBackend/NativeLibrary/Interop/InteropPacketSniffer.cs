using NativeLibrary.Structs;
using System.Runtime.InteropServices;

namespace NativeLibrary.Interop
{
    internal static class InteropPacketSniffer
    {
        [DllImport(Constants.LIBRARY_NAME)]
        public static extern IntPtr CreatePacketSniffer();
        [DllImport(Constants.LIBRARY_NAME)]
        public static extern void DestroyPacketSniffer(IntPtr sniffer);
        [DllImport(Constants.LIBRARY_NAME)]
        public static extern IntPtr PacketSniffer_OpenSnifferHandler(out NativeExecutionInfo executionInfo, IntPtr sniffer, string bridgeName);
        [DllImport(Constants.LIBRARY_NAME)]
        public static extern bool PacketSniffer_ListenForPacket(out NativeExecutionInfo executionInfo, IntPtr sniffer, string bridgeName, IntPtr handler);
        [DllImport(Constants.LIBRARY_NAME)]
        public static extern void PacketSniffer_GetNumberOfPackets(out NativeExecutionInfo executionInfo, IntPtr sniffer, out int numberOfPackets);
        [DllImport(Constants.LIBRARY_NAME)]
        public static extern void PacketSniffer_GetPacket(out NativeExecutionInfo executionInfo, IntPtr sniffer, out NativePacket packet);
    }
}
