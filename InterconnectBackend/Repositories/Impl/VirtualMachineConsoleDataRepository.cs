using Microsoft.Extensions.Options;
using Models.Config;

namespace Repositories.Impl
{
    public class VirtualMachineConsoleDataRepository : IVirtualMachineConsoleDataRepository
    {
        private readonly InterconnectConfig _config;
        private Dictionary<Guid, List<byte>> _memoryConsoleData = [];

        public VirtualMachineConsoleDataRepository(IOptions<InterconnectConfig> config)
        {
            _config = config.Value;
        }

        public void AddDataToConsole(Guid uuid, byte[] data)
        {
            if (!_memoryConsoleData.ContainsKey(uuid))
            {
                _memoryConsoleData[uuid] = [];
            }

            _memoryConsoleData[uuid].AddRange(data);

            if (_memoryConsoleData[uuid].Count > _config.MaxConsoleDataHistory)
            {
                int toRemove = _memoryConsoleData[uuid].Count - _config.MaxConsoleDataHistory;
                _memoryConsoleData[uuid].RemoveRange(0, toRemove);
            }
        }

        public List<byte> GetData(Guid uuid)
        {
            if (!_memoryConsoleData.ContainsKey(uuid))
            {
                return [];
            }

            return _memoryConsoleData[uuid];
        }
    }
}
