#ifndef VIRTUALMACHINEMANAGERMOCKGETINFOABOUTVIRTUALMACHINE_H
#define VIRTUALMACHINEMANAGERMOCKGETINFOABOUTVIRTUALMACHINE_H
#include <gmock/gmock-function-mocker.h>

#include "virt/managers/VirtualMachineManager.h"


class VirtualMachineManagerMockGetInfoAboutVirtualMachine final : public VirtualMachineManager
{
public:
    explicit VirtualMachineManagerMockGetInfoAboutVirtualMachine(ILibvirtWrapper* libvirt)
        : VirtualMachineManager(libvirt)
    {
    }

    MOCK_METHOD(VirtualMachineInfo, getInfoAboutVirtualMachine, (const std::string &uuid), (override));
};

#endif //VIRTUALMACHINEMANAGERMOCKGETINFOABOUTVIRTUALMACHINE_H
