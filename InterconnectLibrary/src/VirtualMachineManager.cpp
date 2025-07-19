#include "VirtualMachineManager.h"

#include <iostream>
#include <stdexcept>
#include <bits/ostream.tcc>

#include "exceptions/VirtualMachineManagerException.h"
#include "utils/VersionUtils.h"
#include "utils/StringUtils.h"

void VirtualMachineManager::initializeConnection(const std::optional<std::string>& customConnectionUrl)
{
    const auto connectionUri = customConnectionUrl.has_value() ? customConnectionUrl.value() : "qemu:///system";

    conn = libvirt->connectOpen(connectionUri.c_str());
    if (conn == nullptr)
    {
        throw VirtualMachineManagerException("An error occurred while connecting to " + connectionUri);
    }
}

ConnectionInfo VirtualMachineManager::getConnectionInfo() const
{
    if (conn == nullptr)
    {
        throw VirtualMachineManagerException("No connected to any hypervisor");
    }

    virNodeInfo info;
    unsigned long libVersion;
    unsigned long driverVersion;

    const auto driverType = libvirt->getDriverType(conn);
    const auto connectionUrl = libvirt->getConnectUrl(conn);
    if (libvirt->getNodeInfo(conn, &info) != 0)
    {
        throw VirtualMachineManagerException("An error occurred while getting node information ");
    }
    if (libvirt->getLibVersion(conn, &libVersion) != 0)
    {
        throw VirtualMachineManagerException("Failed to get lib version");
    }
    if (libvirt->getDriverVersion(conn, &driverVersion) != 0)
    {
        throw VirtualMachineManagerException("Failed to get driver version");
    }

    auto connectionInfo = ConnectionInfo{
        .cpuCount = info.cpus,
        .cpuFreq = info.mhz,
        .totalMemory = info.memory,
        .libVersion = VersionUtils::getVersion(libVersion),
        .driverVersion = VersionUtils::getVersion(driverVersion)
    };
    StringUtils::copyStringToCharArray(connectionUrl, connectionInfo.connectionUrl,
                                       sizeof(connectionInfo.connectionUrl));
    StringUtils::copyStringToCharArray(driverType, connectionInfo.driverType,
                                       sizeof(connectionInfo.driverType));

    return connectionInfo;
}

void VirtualMachineManager::createVirtualMachine(const std::string& virtualMachineXml) const
{
    if (conn == nullptr)
    {
        throw VirtualMachineManagerException("No active connection to the VM backend.");
    }

    const auto domain = libvirt->createVirtualMachineFromXml(conn, virtualMachineXml.c_str());
    if (domain == nullptr)
    {
        throw VirtualMachineManagerException(libvirt->getLastError());
    }
}

VirtualMachineInfo VirtualMachineManager::getInfoAboutVirtualMachine(const std::string& name)
{
    std::string virtualMachineUuid;
    auto domainInfo = virDomainInfo{};

    if (conn == nullptr)
    {
        throw VirtualMachineManagerException("No active connection to the VM backend.");
    }

    const auto domainPtr = libvirt->domainLookupByName(conn, name);
    if (domainPtr == nullptr)
    {
        throw VirtualMachineManagerException("Error while obtaining pointer to virtual machine");
    }

    if (libvirt->domainGetInfo(domainPtr, domainInfo) != 0)
    {
        throw VirtualMachineManagerException("Virtual machine was found but error occurred while obtaining its info");
    }

    if (libvirt->getDomainUUID(domainPtr, virtualMachineUuid) != 0)
    {
        throw VirtualMachineManagerException("Virtual machine was found but error occurred while obtaining its uuid");
    }

    auto vmInfo = VirtualMachineInfo{
        .usedMemory = domainInfo.memory,
        .state = domainInfo.state,
    };
    StringUtils::copyStringToCharArray(virtualMachineUuid, vmInfo.uuid, sizeof(vmInfo.uuid));
    StringUtils::copyStringToCharArray(name, vmInfo.name, sizeof(vmInfo.name));
    return vmInfo;
}

std::vector<VirtualMachineInfo> VirtualMachineManager::getListOfVirtualMachinesWithInfo()
{
    virDomainPtr* domains = nullptr;
    int numDomains = 0;

    if (conn == nullptr)
    {
        throw VirtualMachineManagerException("No active connection to the VM backend.");
    }

    numDomains = libvirt->getListOfAllDomains(conn, &domains);
    if (numDomains == -1)
    {
        throw VirtualMachineManagerException("Error retrieving the list of virtual machines.");
    }

    std::vector<VirtualMachineInfo> virtualMachines;
    for (int i = 0; i < numDomains; i++)
    {
        auto virtualMachineName = libvirt->getDomainName(domains[i]);
        virtualMachines.push_back(getInfoAboutVirtualMachine(virtualMachineName));
        libvirt->freeDomain(domains[i]);
    }

    free(domains);
    return virtualMachines;
}
