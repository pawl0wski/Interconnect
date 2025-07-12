using System.Runtime.InteropServices;

namespace Library.Models
{
    [StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Ansi)]
    public struct ExecutionInfo
    {
        [MarshalAs(UnmanagedType.I1)]
        public bool ErrorOccurred;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
        public string Msg;
    }
}
