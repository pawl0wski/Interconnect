using Models;
using NativeLibrary.Structs;

namespace Mappers
{
    public static class NativeVirtualMachineInfoMapper
    {
        public static VirtualMachineInfo MapToVirtualMachineInfo(NativeVirtualMachineInfo virtualMachineInfo)
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
