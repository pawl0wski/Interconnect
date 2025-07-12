using System.Runtime.InteropServices;

namespace NativeLibrary.Structs
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public record NativeVersion
    {
        public uint Minor;
        public uint Major;
        public uint Patch;

        public override string ToString()
            => $"{Minor}.{Major}.{Patch}";
    }
}
