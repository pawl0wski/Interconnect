#include "VirtualMachineManager.h"

#include "exceptions/ConnectionToVMBackendFailed.h"
#include "exceptions/CreateVirtualMachineFailed.h"
#include "exceptions/NoActiveVMBackendConnection.h"

void VirtualMachineManager::initializeConnection(const std::optional<std::string> &customConnectionUri) {
    const auto connectionUri = customConnectionUri.has_value() ? customConnectionUri.value() : "qemu:///system";

    this->connectPtr = libvirt.connectOpen(connectionUri.c_str());
    if (!this->connectPtr) {
        throw ConnectionToVMBackendFailed("An error occurred while connecting to " + connectionUri);
    }
}

VirtualMachineInfo VirtualMachineManager::createVirtualMachine(const std::string &virtualMachineXml) {
    if (this->connectPtr == nullptr) {
        throw NoActiveVMBackendConnection("No active connection to the VM backend.");
    }

    const auto domain = libvirt.createVirtualMachineFromXml(this->connectPtr, virtualMachineXml.c_str());
    if (domain == nullptr) {
        throw CreateVirtualMachineFailed("Error while creating Virtual Machine");
    }

    char uuid[VIR_UUID_STRING_BUFLEN];
    libvirt.getUuidFromDomain(domain, uuid);

    return getInfoAboutVirtualMachine(uuid);
}

VirtualMachineInfo VirtualMachineManager::getInfoAboutVirtualMachine(const std::string &uuid) {
    return VirtualMachineInfo(uuid);
}
