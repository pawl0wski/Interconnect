using System.Runtime.InteropServices;

namespace NativeLibrary.Structs
{
    [StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Ansi)]
    public struct NativeExecutionInfo
    {
        [MarshalAs(UnmanagedType.I1)]
        public bool ErrorOccurred;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
        public string Msg;
    }
}
