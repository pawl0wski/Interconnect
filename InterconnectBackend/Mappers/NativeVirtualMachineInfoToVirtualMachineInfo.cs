using Models;
using NativeLibrary.Structs;

namespace Mappers
{
    public static class NativeVirtualMachineInfoToVirtualMachineInfo
    {
        public static VirtualMachineInfo Map(NativeVirtualMachineInfo virtualMachineInfo)
        {
            return new VirtualMachineInfo
            {
                Uuid = virtualMachineInfo.Uuid,
                Name = virtualMachineInfo.Name,
                UsedMemory = virtualMachineInfo.UsedMemory,
                State = virtualMachineInfo.State,
            };
        }
    }
}
