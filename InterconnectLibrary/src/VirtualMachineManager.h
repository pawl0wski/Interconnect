#ifndef VIRTUALMACHINEMANAGER_H
#define VIRTUALMACHINEMANAGER_H
#include <optional>
#include <string>
#include <libvirt/libvirt.h>

#include "interfaces/ILibvirtWrapper.h"

class VirtualMachineManager {
    virConnectPtr connectPtr = nullptr;
    ILibvirtWrapper &libvirt;

public:
    explicit VirtualMachineManager(ILibvirtWrapper &libvirt)
        : libvirt(libvirt) {
    }

    void initializeConnection(const std::optional<std::string> &customConnectionPath = std::nullopt);

    void createVirtualMachine();

    void getInfoAboutVirtualMachine();
};

#endif //VIRTUALMACHINEMANAGER_H
