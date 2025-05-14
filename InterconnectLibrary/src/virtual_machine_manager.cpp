#include "virtual_machine_manager.h"

#include "exceptions/ConnectionToVMBackendFailed.h"

void VirtualMachineManager::initializeConnection(const std::optional<std::string> &customConnectionPath) {
    const auto connectionPath = customConnectionPath.has_value() ? customConnectionPath.value() : "qemu:///system";

    this->connectPtr = virConnectOpen(connectionPath.c_str());
    if (!this->connectPtr) {
        throw ConnectionToVMBackendFailed("An error occurred while connecting to " + connectionPath);
    }
}

void VirtualMachineManager::createVirtualMachine() {
}

void VirtualMachineManager::getInfoAboutVirtualMachine() {
}
