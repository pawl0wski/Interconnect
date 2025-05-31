using System.Runtime.InteropServices;

namespace NativeLibrary
{
    public class InterconnectLibrary
    {
        [DllImport(Constants.LIBRARY_NAME)]
        static extern IntPtr VirtualMachineManager_Create(IntPtr libvirtWrapperPtr);

        [DllImport(Constants.LIBRARY_NAME)]
        static extern void VirtualMachineManager_Destroy(IntPtr manager);

        [DllImport(Constants.LIBRARY_NAME)]
        static extern void VirtualMachineManager_InitializeConnection(IntPtr manager, string customConnectionUri);

        [DllImport(Constants.LIBRARY_NAME)]
        static extern IntPtr VirtualMachineManager_CreateVirtualMachine(IntPtr manager, string virtualMachineXml);

        [DllImport(Constants.LIBRARY_NAME)]
        static extern IntPtr VirtualMachineManager_GetInfoAboutVirtualMachine(IntPtr manager, string uuid);
    }
}
