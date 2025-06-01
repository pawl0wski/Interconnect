#ifndef INTERCONNECTLIBRARYEXTERN_H
#define INTERCONNECTLIBRARYEXTERN_H
#include "VirtualMachineManager.h"

extern "C" {
VirtualMachineManager *VirtualMachineManager_Create() {
    return new VirtualMachineManager();
}

void VirtualMachineManager_Destroy(const VirtualMachineManager *manager) {
    delete manager;
}

void VirtualMachineManager_InitializeConnection(VirtualMachineManager *manager, const char *customConnectionUri) {
    manager->initializeConnection(
        customConnectionUri == nullptr
            ? std::nullopt
            : std::make_optional(std::string(customConnectionUri)));
}

char *VirtualMachineManager_CreateVirtualMachine(const char *virtualMachineXml);

char *VirtualMachineManager_GetInfoAboutVirtualMachine(const char *uuid);
}

#endif // INTERCONNECTLIBRARYEXTERN_H
