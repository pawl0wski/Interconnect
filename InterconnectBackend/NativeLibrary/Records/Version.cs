using System.Runtime.InteropServices;

namespace Models
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public record Version
    {
        public uint Minor;
        public uint Major;
        public uint Patch;

        public override string ToString()
            => $"{Minor}.{Major}.{Patch}";
    }
}
