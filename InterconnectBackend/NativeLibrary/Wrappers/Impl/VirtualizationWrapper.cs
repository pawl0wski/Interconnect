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
            _virtualizationFacade = InteropVirtualization.CreateVirtualizationFacade();
        }
        public void InitializeConnection(string? connectionUrl)
        {
            NativeExecutionInfo executionInfo = new();

            InteropVirtualization.Virtualization_InitializeConnection(out executionInfo, _virtualizationFacade, connectionUrl);

            ExecutionInfoAnalyzer.ThrowIfErrorOccurred(executionInfo);
        }
        public NativeConnectionInfo GetConnectionInfo()
        {
            NativeExecutionInfo executionInfo = new();
            NativeConnectionInfo info = new();

            InteropVirtualization.Virtualization_GetConnectionInfo(out executionInfo, _virtualizationFacade, out info);

            ExecutionInfoAnalyzer.ThrowIfErrorOccurred(executionInfo);
            return info;
        }

        public void CreateVirtualMachine(string xmlDefinition)
        {
            NativeExecutionInfo executionInfo = new();

            InteropVirtualization.Virtualization_CreateVirtualMachine(out executionInfo, _virtualizationFacade, xmlDefinition);

            ExecutionInfoAnalyzer.ThrowIfErrorOccurred(executionInfo);
        }

        public NativeVirtualMachineInfo GetVirtualMachineInfo(string name)
        {
            NativeExecutionInfo executionInfo = new();
            NativeVirtualMachineInfo info = new();

            InteropVirtualization.Virtualization_GetInfoAboutVirtualMachine(out executionInfo, _virtualizationFacade, name, out info);

            ExecutionInfoAnalyzer.ThrowIfErrorOccurred(executionInfo);

            return info;
        }

        public INativeList<NativeVirtualMachineInfo> GetListOfVirtualMachines()
        {
            NativeExecutionInfo executionInfo = new();
            IntPtr listOfVirtualMachines;
            int numberOfVirtualMachines;

            InteropVirtualization.Virtualization_GetListOfVirtualMachinesWithInfo(out executionInfo, _virtualizationFacade, out listOfVirtualMachines, out numberOfVirtualMachines);

            ExecutionInfoAnalyzer.ThrowIfErrorOccurred(executionInfo);

            return new NativeList<NativeVirtualMachineInfo>(listOfVirtualMachines, numberOfVirtualMachines);
        }

        public bool IsConnectionAlive()
        {
            NativeExecutionInfo executionInfo = new();
            bool isConnectionAlive;

            InteropVirtualization.Virtualization_IsConnectionAlive(out executionInfo, _virtualizationFacade, out isConnectionAlive);

            ExecutionInfoAnalyzer.ThrowIfErrorOccurred(executionInfo);

            return isConnectionAlive;
        }

        public IntPtr OpenVirtualMachineConsole(Guid uuid)
        {
            NativeExecutionInfo executionInfo = new();
            IntPtr stream;

            InteropVirtualization.Virtualization_OpenVirtualMachineConsole(out executionInfo, _virtualizationFacade, out stream, uuid.ToString());

            ExecutionInfoAnalyzer.ThrowIfErrorOccurred(executionInfo);

            return stream;
        }

        public void SendDataToStream(IntPtr stream, string data)
        {
            NativeExecutionInfo executionInfo = new();

            InteropVirtualization.Virtualization_SendDataToConsole(out executionInfo, _virtualizationFacade, stream, data);

            ExecutionInfoAnalyzer.ThrowIfErrorOccurred(executionInfo);
        }

        public NativeStreamData GetDataFromStream(IntPtr stream)
        {
            NativeExecutionInfo executionInfo = new();
            var streamData = new NativeStreamData();

            InteropVirtualization.Virtualization_ReceiveDataFromConsole(out executionInfo, _virtualizationFacade, stream, out streamData);

            ExecutionInfoAnalyzer.ThrowIfErrorOccurred(executionInfo);

            return streamData;
        }

        public void CloseStream(IntPtr stream)
        {
            NativeExecutionInfo executionInfo = new();

            InteropVirtualization.Virtualization_CloseStream(out executionInfo, _virtualizationFacade, stream);

            ExecutionInfoAnalyzer.ThrowIfErrorOccurred(executionInfo);
        }

        public IntPtr CreateVirtualNetwork(string networkDefinition)
        {
            NativeExecutionInfo executionInfo = new();
            IntPtr network;

            InteropVirtualization.Virtualization_CreateVirtualNetwork(out executionInfo, _virtualizationFacade, out network, networkDefinition);

            ExecutionInfoAnalyzer.ThrowIfErrorOccurred(executionInfo);

            return network;
        }

        public void AttachDeviceToVirtualMachine(Guid uuid, string deviceDefinition)
        {
            NativeExecutionInfo executionInfo = new();

            InteropVirtualization.Virtualization_AttachDeviceToVirtualMachine(out executionInfo, _virtualizationFacade, uuid.ToString(), deviceDefinition);

            ExecutionInfoAnalyzer.ThrowIfErrorOccurred(executionInfo);
        }

        public void DetachDeviceFromVirtualMachine(Guid uuid, string deviceDefinition)
        {
            NativeExecutionInfo executionInfo = new();

            InteropVirtualization.Virtualization_DetachDeviceFromVirtualMachine(out executionInfo, _virtualizationFacade, uuid.ToString(), deviceDefinition);

            ExecutionInfoAnalyzer.ThrowIfErrorOccurred(executionInfo);
        }

        public void UpdateVmDevice(Guid uuid, string deviceDefinition)
        {
            NativeExecutionInfo executionInfo = new();

            InteropVirtualization.Virtualization_UpdateVmDevice(out executionInfo, _virtualizationFacade, uuid.ToString(), deviceDefinition);

            ExecutionInfoAnalyzer.ThrowIfErrorOccurred(executionInfo);
        }

        public void DestroyNetwork(string name)
        {
            NativeExecutionInfo executionInfo = new();

            InteropVirtualization.Virtualization_DestroyNetwork(out executionInfo, _virtualizationFacade, name);

            ExecutionInfoAnalyzer.ThrowIfErrorOccurred(executionInfo);
        }
    }
}
