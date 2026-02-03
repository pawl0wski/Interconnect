using Mappers;
using Models;
using NativeLibrary.Wrappers;
using Repositories;

namespace Services.Impl
{
    /// <summary>
    /// Service managing virtual machine console connections and data.
    /// </summary>
    public class VirtualMachineConsoleService : IVirtualMachineConsoleService
    {
        private readonly IVirtualizationWrapper _virtualizationWrapper;
        private readonly IVirtualMachineConsoleDataRepository _consoleDataRespository;
        private readonly IVirtualMachineConsoleStreamRepository _consoleStreamRespository;

        /// <summary>
        /// Initializes a new instance of the VirtualMachineConsoleService.
        /// </summary>
        /// <param name="virtualizationWrapper">Virtualization wrapper for console operations.</param>
        /// <param name="consoleDataRepository">Repository for console data.</param>
        /// <param name="consoleStreamRepository">Repository for console streams.</param>
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

        /// <summary>
        /// Retrieves initial console data for a virtual machine.
        /// </summary>
        /// <param name="uuid">Virtual machine UUID.</param>
        /// <returns>Array of console data bytes.</returns>
        public byte[] GetInitialConsoleData(Guid uuid)
        {
            return [.. _consoleDataRespository.GetData(uuid)];
        }

        /// <summary>
        /// Gets bytes from the console stream.
        /// </summary>
        /// <param name="stream">Stream information.</param>
        /// <returns>Data retrieved from the console stream.</returns>
        public StreamData GetBytesFromConsole(StreamInfo stream)
        {
            var data = _virtualizationWrapper.GetDataFromStream(stream.Stream);
            _consoleDataRespository.AddDataToConsole(stream.Uuid, data.Buffer);
            return NativeStreamDataMapper.MapToStreamData(data);
        }

        /// <summary>
        /// Opens a console connection for a virtual machine.
        /// </summary>
        /// <param name="uuid">Virtual machine UUID.</param>
        /// <returns>Stream information for the opened console.</returns>
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

        /// <summary>
        /// Sends bytes to a virtual machine console.
        /// </summary>
        /// <param name="uuid">Virtual machine UUID.</param>
        /// <param name="data">Data to send to the console.</param>
        public void SendBytesToVirtualMachineConsoleByUuid(Guid uuid, string data)
        {
            var stream = _consoleStreamRespository.GetByUuid(uuid);

            if (stream is null)
            {
                stream = OpenVirtualMachineConsole(uuid);
            }

            _virtualizationWrapper.SendDataToStream(stream.Stream, data);
        }

        /// <summary>
        /// Retrieves all active console streams.
        /// </summary>
        /// <returns>List of active console streams.</returns>
        public List<StreamInfo> GetStreams()
        {
            return _consoleStreamRespository.GetAllStreams();
        }

        /// <summary>
        /// Closes a console stream connection.
        /// </summary>
        /// <param name="stream">Stream information to close.</param>
        public void CloseStream(StreamInfo stream)
        {
            _consoleStreamRespository.Remove(stream);
            _virtualizationWrapper.CloseStream(stream.Stream);
        }
    }
}
