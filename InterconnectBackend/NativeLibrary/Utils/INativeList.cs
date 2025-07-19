namespace NativeLibrary.Utils
{
    public interface INativeList<T> : IReadOnlyList<T> where T : struct
    {
    }
}
