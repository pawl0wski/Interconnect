using NativeLibrary.Interop;
using NativeLibrary.Structs;
using NativeLibrary.Utils;
using NativeLibrary.Utils.Impl;

namespace NativeLibrary.Wrappers.Impl
{
    public class VirtualizationWrapper : IVirtualizationWrapper
    {
        private IntPtr _virtualizationFacade = IntPtr.Zero;

        public VirtualizationWrapper()
        {
            _virtualizationFacade = InteropVirtualization.Create();
        }
        public void InitializeConnection(string? connectionUrl)
        {
            NativeExecutionInfo executionInfo = new();

            InteropVirtualization.InitializeConnection(out executionInfo, _virtualizationFacade, connectionUrl);

            ExecutionInfoAnalyzer.ThrowIfErrorOccurred(executionInfo);
        }
        public NativeConnectionInfo GetConnectionInfo()
        {
            NativeExecutionInfo executionInfo = new();
            NativeConnectionInfo info = new();

            InteropVirtualization.GetConnectionInfo(out executionInfo, _virtualizationFacade, out info);

            ExecutionInfoAnalyzer.ThrowIfErrorOccurred(executionInfo);
            return info;
        }

        public void CreateVirtualMachine(string xmlDefinition)
        {
            NativeExecutionInfo executionInfo = new();

            InteropVirtualization.CreateVirtualMachine(out executionInfo, _virtualizationFacade, xmlDefinition);

            ExecutionInfoAnalyzer.ThrowIfErrorOccurred(executionInfo);
        }

        public NativeVirtualMachineInfo GetVirtualMachineInfo(string name)
        {
            NativeExecutionInfo executionInfo = new();
            NativeVirtualMachineInfo info = new();

            InteropVirtualization.GetInfoAboutVirtualMachine(out executionInfo, _virtualizationFacade, name, out info);

            ExecutionInfoAnalyzer.ThrowIfErrorOccurred(executionInfo);

            return info;
        }

        public INativeList<NativeVirtualMachineInfo> GetListOfVirtualMachines()
        {
            NativeExecutionInfo executionInfo = new();
            IntPtr listOfVirtualMachines;
            int numberOfVirtualMachines;

            InteropVirtualization.GetListOfVirtualMachinesWithInfo(out executionInfo, _virtualizationFacade, out listOfVirtualMachines, out numberOfVirtualMachines);

            ExecutionInfoAnalyzer.ThrowIfErrorOccurred(executionInfo);

            return new NativeList<NativeVirtualMachineInfo>(listOfVirtualMachines, numberOfVirtualMachines);
        }

        public bool IsConnectionAlive()
        {
            NativeExecutionInfo executionInfo = new();
            bool isConnectionAlive;

            InteropVirtualization.IsConnectionAlive(out executionInfo, _virtualizationFacade, out isConnectionAlive);

            ExecutionInfoAnalyzer.ThrowIfErrorOccurred(executionInfo);

            return isConnectionAlive;
        }

        public IntPtr OpenVirtualMachineConsole(Guid uuid)
        {
            NativeExecutionInfo executionInfo = new();
            IntPtr stream;

            InteropVirtualization.OpenVirtualMachineConsole(out executionInfo, _virtualizationFacade, out stream, uuid.ToString());

            ExecutionInfoAnalyzer.ThrowIfErrorOccurred(executionInfo);

            return stream;
        }

        public void SendDataToStream(IntPtr stream, string data)
        {
            NativeExecutionInfo executionInfo = new();

            InteropVirtualization.SendDataToConsole(out executionInfo, _virtualizationFacade, stream, data);

            ExecutionInfoAnalyzer.ThrowIfErrorOccurred(executionInfo);
        }

        public NativeStreamData GetDataFromStream(IntPtr stream)
        {
            NativeExecutionInfo executionInfo = new();
            var streamData = new NativeStreamData();

            InteropVirtualization.ReceiveDataFromConsole(out executionInfo, _virtualizationFacade, stream, out streamData);

            ExecutionInfoAnalyzer.ThrowIfErrorOccurred(executionInfo);

            return streamData;
        }

        public void CloseStream(IntPtr stream)
        {
            NativeExecutionInfo executionInfo = new();

            InteropVirtualization.CloseStream(out executionInfo, _virtualizationFacade, stream);

            ExecutionInfoAnalyzer.ThrowIfErrorOccurred(executionInfo);
        }
    }
}
