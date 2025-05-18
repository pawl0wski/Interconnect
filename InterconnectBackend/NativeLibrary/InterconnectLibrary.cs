using System.Runtime.InteropServices;

namespace NativeLibrary
{
    public class InterconnectLibrary
    {
        [DllImport("libVirtualMachineManager.so")]
        static extern IntPtr VirtualMachineManager_Create(IntPtr libvirtWrapperPtr);

        [DllImport("libVirtualMachineManager.so")]
        static extern void VirtualMachineManager_Destroy(IntPtr manager);

        [DllImport("libVirtualMachineManager.so")]
        static extern void VirtualMachineManager_InitializeConnection(IntPtr manager, string customConnectionUri);

        [DllImport("libVirtualMachineManager.so")]
        static extern IntPtr VirtualMachineManager_CreateVirtualMachine(IntPtr manager, string virtualMachineXml);

        [DllImport("libVirtualMachineManager.so")]
        static extern IntPtr VirtualMachineManager_GetInfoAboutVirtualMachine(IntPtr manager, string uuid);


    }
}
