using Mappers;
using Models;
using NativeLibrary.Wrappers;
using Repositories;

namespace Services.Impl
{
    public class VirtualMachineConsoleService : IVirtualMachineConsoleService
    {
        private readonly IVirtualizationWrapper _virtualizationWrapper;
        private readonly IVirtualMachineConsoleDataRepository _consoleDataRespository;
        private readonly IVirtualMachineConsoleStreamRepository _consoleStreamRespository;

        public VirtualMachineConsoleService(
            IVirtualizationWrapper virtualizationWrapper,
            IVirtualMachineConsoleDataRepository consoleDataRepository,
            IVirtualMachineConsoleStreamRepository consoleStreamRepository
            )
        {
            _virtualizationWrapper = virtualizationWrapper;
            _consoleDataRespository = consoleDataRepository;
            _consoleStreamRespository = consoleStreamRepository;
        }

        public byte[] GetInitialConsoleData(Guid uuid)
        {
            return [.. _consoleDataRespository.GetData(uuid)];
        }

        public StreamData GetBytesFromConsole(StreamInfo stream)
        {
            var data = _virtualizationWrapper.GetDataFromStream(stream.Stream);
            _consoleDataRespository.AddDataToConsole(stream.Uuid, data.Buffer);
            return NativeStreamDataMapper.MapToStreamData(data);
        }

        public StreamInfo OpenVirtualMachineConsole(Guid uuid)
        {
            var streamPtr = _virtualizationWrapper.OpenVirtualMachineConsole(uuid);
            var stream = new StreamInfo
            {
                Uuid = uuid,
                Stream = streamPtr
            };

            _consoleStreamRespository.Add(stream);

            return stream;
        }

        public void SendBytesToVirtualMachineConsoleByUuid(Guid uuid, string data)
        {
            var stream = _consoleStreamRespository.GetByUuid(uuid);
            _virtualizationWrapper.SendDataToStream(stream.Stream, data);
        }

        public List<StreamInfo> GetStreams()
        {
            return _consoleStreamRespository.GetAllStreams();
        }

        public void CloseStream(StreamInfo stream)
        {
            _consoleStreamRespository.Remove(stream);
            _virtualizationWrapper.CloseStream(stream.Stream);
        }
    }
}
