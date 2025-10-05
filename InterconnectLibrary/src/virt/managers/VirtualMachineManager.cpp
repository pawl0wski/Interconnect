#include "VirtualMachineManager.h"

#include <iostream>

#include "../../exceptions/VirtualizationException.h"
#include "../../utils/StringUtils.h"

void VirtualMachineManager::createVirtualMachine(const std::string& virtualMachineXml) const
{
    checkIfConnectionIsSet();

    const auto domain = libvirt->createVirtualMachineFromXml(conn, virtualMachineXml.c_str());
    if (domain == nullptr)
    {
        throw VirtualizationException(libvirt->getLastError());
    }
}

VirtualMachineInfo VirtualMachineManager::getInfoAboutVirtualMachine(const std::string& name)
{
    std::string virtualMachineUuid;
    auto domainInfo = virDomainInfo{};

    checkIfConnectionIsSet();

    const auto domainPtr = getVirtualMachineByName(name);

    if (libvirt->domainGetInfo(domainPtr, domainInfo) != 0)
    {
        throw VirtualizationException("Virtual machine was found but error occurred while obtaining its info");
    }

    if (libvirt->getDomainUUID(domainPtr, virtualMachineUuid) != 0)
    {
        throw VirtualizationException("Virtual machine was found but error occurred while obtaining its uuid");
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

    checkIfConnectionIsSet();

    numDomains = libvirt->getListOfAllDomains(conn, &domains);
    if (numDomains == -1)
    {
        throw VirtualizationException("Error retrieving the list of virtual machines.");
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

void VirtualMachineManager::attachDeviceToVirtualMachine(const std::string& uuid,
                                                         const std::string& deviceDefinition) const
{
    checkIfConnectionIsSet();

    const auto domainPtr = getVirtualMachineByUuid(uuid);

    if (libvirt->attachDeviceToVm(domainPtr, deviceDefinition) == -1)
    {
        throw VirtualizationException("Can't attach device");
    }
}

void VirtualMachineManager::detachDeviceFromVirtualMachine(const std::string& uuid,
                                                           const std::string& deviceDefinition) const
{
    checkIfConnectionIsSet();

    const auto domainPtr = getVirtualMachineByUuid(uuid);

    if (libvirt->detachDeviceFromVm(domainPtr, deviceDefinition) == -1)
    {
        throw VirtualizationException("Can't detach device");
    }
}

void VirtualMachineManager::updateVmDevice(const std::string& uuid, const std::string& deviceDefinition) const
{
    checkIfConnectionIsSet();

    const auto domainPtr = getVirtualMachineByUuid(uuid);

    if (libvirt->updateVmDevice(domainPtr, deviceDefinition) == -1)
    {
        throw VirtualizationException("Can't modify device");
    }
}

virDomainPtr VirtualMachineManager::getVirtualMachineByName(const std::string& name) const
{
    const auto domainPtr = libvirt->domainLookupByName(conn, name);
    if (domainPtr == nullptr)
    {
        throw VirtualizationException("Error while obtaining pointer to virtual machine");
    }
    return domainPtr;
}

virDomainPtr VirtualMachineManager::getVirtualMachineByUuid(const std::string& uuid) const
{
    const auto domainPtr = libvirt->domainLookupByUuid(conn, uuid);
    if (domainPtr == nullptr)
    {
        throw VirtualizationException("Error while obtaining pointer to virtual machine");
    }
    return domainPtr;
}
