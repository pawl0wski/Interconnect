#ifndef INTERCONNECTLIBRARYEXTERN_H
#define INTERCONNECTLIBRARYEXTERN_H

extern "C" {
typedef struct VirtualMachineManager VirtualMachineManager;

VirtualMachineManager *VirtualMachineManager_Create(void *libvirtWrapper);

void VirtualMachineManager_Destroy(VirtualMachineManager *manager);

void VirtualMachineManager_InitializeConnection(VirtualMachineManager *manager, const char *customConnectionUri);

char *VirtualMachineManager_CreateVirtualMachine(VirtualMachineManager *manager, const char *virtualMachineXml);

char *VirtualMachineManager_GetInfoAboutVirtualMachine(VirtualMachineManager *manager, const char *uuid);
}

#endif // INTERCONNECTLIBRARYEXTERN_H
