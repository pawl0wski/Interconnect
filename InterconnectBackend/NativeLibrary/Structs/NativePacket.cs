using System.Runtime.InteropServices;

namespace NativeLibrary.Structs
{
    public struct NativePacket
    {
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
        public string InterfaceName;
        public IntPtr Content;
        public int ContentLength;
    }
}
