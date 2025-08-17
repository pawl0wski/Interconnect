#include "VirtualMachineManager.h"

#include <iostream>

#include "../../exceptions/VirtualMachineManagerException.h"
#include "../../utils/StringUtils.h"

void VirtualMachineManager::updateConnection(const virConnectPtr conn)
{
    this->conn = conn;
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
