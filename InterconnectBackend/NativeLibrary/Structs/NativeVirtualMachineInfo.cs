using System.Runtime.InteropServices;

namespace NativeLibrary.Structs
{
    [StructLayout(LayoutKind.Explicit, CharSet = CharSet.Ansi, Size = 400)]
    public struct NativeVirtualMachineInfo
    {
        [FieldOffset(0)]
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
        public string Uuid;

        [FieldOffset(128)]
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 255)]
        public string Name;

        [FieldOffset(384)]
        public ulong UsedMemory;

        [FieldOffset(392)]
        public byte State;
    }
}
