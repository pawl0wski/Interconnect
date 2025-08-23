using Models;
using NativeLibrary.Structs;

namespace Mappers
{
    public static class NativeStreamDataMapper
    {
        public static StreamData MapToStreamData(NativeStreamData streamData)
        {
            return new StreamData
            {
                Data = streamData.Buffer,
                IsStreamBroken = streamData.IsStreamBroken,
            };
        }
    }
}
