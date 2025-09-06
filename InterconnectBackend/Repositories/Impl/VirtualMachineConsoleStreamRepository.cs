using Models;

namespace Repositories.Impl
{
    public class VirtualMachineConsoleStreamRepository : IVirtualMachineConsoleStreamRepository
    {
        private List<StreamInfo> streams = [];

        public void Add(StreamInfo stream)
        {
            streams.Add(stream);
        }

        public List<StreamInfo> GetAllStreams()
        {
            return streams;
        }

        public StreamInfo? GetByUuid(Guid uuid)
        {
            return streams.Where(s => s.Uuid == uuid).FirstOrDefault();
        }

        public void Remove(StreamInfo stream)
        {
            var streamToRemove = streams.Where(s => s.Uuid == stream.Uuid).Single();
            streams.Remove(streamToRemove);
        }
    }
}
