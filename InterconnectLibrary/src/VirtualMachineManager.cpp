#include "VirtualMachineManager.h"

#include <stdexcept>

#include "exceptions/VirtualMachineManagerException.h"
#include "utils/VersionUtils.h"
#include "utils/StringUtils.h"

void VirtualMachineManager::initializeConnection(const std::optional<std::string> &customConnectionUrl) {
    const auto connectionUri = customConnectionUrl.has_value() ? customConnectionUrl.value() : "qemu:///system";

    conn = libvirt->connectOpen(connectionUri.c_str());
    if (conn == nullptr) {
        throw VirtualMachineManagerException("An error occurred while connecting to " + connectionUri);
    }
}

ConnectionInfo VirtualMachineManager::getConnectionInfo() const {
    if (conn == nullptr)
    {
        throw VirtualMachineManagerException("No connected to any hypervisor");
    }

    virNodeInfo info;
    unsigned long libVersion;
    unsigned long driverVersion;

    const auto driverType = libvirt->getDriverType(conn);
    const auto connectionUrl = libvirt->getConnectUrl(conn);
    if (libvirt->getNodeInfo(conn, &info) != 0) {
        throw VirtualMachineManagerException("An error occurred while getting node information ");
    }
    if (libvirt->getLibVersion(conn, &libVersion) != 0) {
        throw VirtualMachineManagerException("Failed to get lib version");
    }
    if (libvirt->getDriverVersion(conn, &driverVersion) != 0) {
        throw VirtualMachineManagerException("Failed to get driver version");
    }

    return ConnectionInfo{
        info.cpus,
        info.mhz,
        info.memory,
        StringUtils::toConstCharPointer(connectionUrl),
        StringUtils::toConstCharPointer(driverType),
        VersionUtils::getVersion(libVersion),
        VersionUtils::getVersion(driverVersion),
    };
}

VirtualMachineInfo VirtualMachineManager::createVirtualMachine(const std::string &virtualMachineXml) {
    if (conn == nullptr) {
        throw VirtualMachineManagerException("No active connection to the VM backend.");
    }

    const auto domain = libvirt->createVirtualMachineFromXml(conn, virtualMachineXml.c_str());
    if (domain == nullptr) {
        throw VirtualMachineManagerException("Error while creating Virtual Machine");
    }

    char uuid[VIR_UUID_STRING_BUFLEN];
    libvirt->getUuidFromDomain(domain, uuid);

    return getInfoAboutVirtualMachine(uuid);
}

VirtualMachineInfo VirtualMachineManager::getInfoAboutVirtualMachine(const std::string &uuid) {
    return VirtualMachineInfo(uuid);
}
