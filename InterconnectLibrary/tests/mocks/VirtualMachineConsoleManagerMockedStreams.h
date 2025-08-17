#ifndef VIRTUALMACHINECONSOLEMANAGERMOCKEDSTREAMS_H
#define VIRTUALMACHINECONSOLEMANAGERMOCKEDSTREAMS_H
#include <gmock/gmock-function-mocker.h>

#include "virt/managers/VirtualMachineConsoleManager.h"

class VirtualMachineConsoleManagerMockedStreams final : public VirtualMachineConsoleManager
{
public:
    explicit VirtualMachineConsoleManagerMockedStreams(ILibvirtWrapper* libvirt)
        : VirtualMachineConsoleManager(libvirt)
    {
    }

    MOCK_METHOD(virStreamPtr, getStreamById, (int streamId), (const, override));
};

#endif //VIRTUALMACHINECONSOLEMANAGERMOCKEDSTREAMS_H
