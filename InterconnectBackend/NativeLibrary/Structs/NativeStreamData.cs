using System.Runtime.InteropServices;

namespace NativeLibrary.Structs
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct NativeStreamData
    {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 255)]
        public byte[] Buffer;
        [MarshalAs(UnmanagedType.I1)]
        public bool IsStreamBroken;
    }
}
