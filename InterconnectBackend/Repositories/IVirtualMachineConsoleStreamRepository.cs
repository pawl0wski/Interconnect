using Models;

namespace Repositories
{
    public interface IVirtualMachineConsoleStreamRepository
    {
        void Add(StreamInfo stream);
        void Remove(StreamInfo stream);
        StreamInfo? GetByUuid(Guid uuid);
        List<StreamInfo> GetAllStreams();
    }
}
