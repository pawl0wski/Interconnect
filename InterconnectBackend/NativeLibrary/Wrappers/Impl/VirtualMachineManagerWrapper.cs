using NativeLibrary.Interop;
using NativeLibrary.Structs;
using NativeLibrary.Utils;

namespace NativeLibrary.Wrappers.Impl
{
    public class VirtualMachineManagerWrapper : IVirtualMachineManagerWrapper
    {
        private IntPtr _manager = IntPtr.Zero;

        public VirtualMachineManagerWrapper()
        {
            _manager = InteropVirtualMachineManager.VirtualMachineManager_Create();
        }
        public void InitializeConnection(string? connectionUrl)
        {
            NativeExecutionInfo executionInfo = new();

            InteropVirtualMachineManager.VirtualMachineManager_InitializeConnection(ref executionInfo, _manager, connectionUrl);

            ExecutionInfoAnalyzer.ThrowIfErrorOccurred(executionInfo);
        }
        public NativeConnectionInfo GetConnectionInfo()
        {
            NativeExecutionInfo executionInfo = new();
            NativeConnectionInfo info = new();

            InteropVirtualMachineManager.VirtualMachineManager_GetConnectionInfo(ref executionInfo, _manager, ref info);

            ExecutionInfoAnalyzer.ThrowIfErrorOccurred(executionInfo);
            return info;
        }

        public void CreateVirtualMachine(string xmlDefinition)
        {
            NativeExecutionInfo executionInfo = new();

            InteropVirtualMachineManager.VirtualMachineManager_CreateVirtualMachine(ref executionInfo, _manager, xmlDefinition);

            ExecutionInfoAnalyzer.ThrowIfErrorOccurred(executionInfo);
        }

        public NativeVirtualMachineInfo GetVirtualMachineInfo(string name)
        {
            NativeExecutionInfo executionInfo = new();
            NativeVirtualMachineInfo info = new();

            InteropVirtualMachineManager.VirtualMachineManager_GetInfoAboutVirtualMachine(ref executionInfo, _manager, name, ref info);

            ExecutionInfoAnalyzer.ThrowIfErrorOccurred(executionInfo);

            return info;
        }

        public INativeList<NativeVirtualMachineInfo> GetListOfVirtualMachines()
        {
            NativeExecutionInfo executionInfo = new();
            IntPtr listOfVirtualMachines;
            int numberOfVirtualMachines;

            InteropVirtualMachineManager.VirtualMachineManager_GetListOfVirtualMachinesWithInfo(in executionInfo, _manager, out listOfVirtualMachines, out numberOfVirtualMachines);

            ExecutionInfoAnalyzer.ThrowIfErrorOccurred(executionInfo);

            return new NativeList<NativeVirtualMachineInfo>(listOfVirtualMachines, numberOfVirtualMachines);
        }
    }
}
