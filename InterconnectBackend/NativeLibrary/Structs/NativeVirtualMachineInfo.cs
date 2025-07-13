using System.Runtime.InteropServices;

namespace NativeLibrary.Structs
{
    [StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Ansi)]
    public record NativeVirtualMachineInfo
    {
        [MarshalAs(UnmanagedType.LPStr)]
        public string Uuid;
    }
}
