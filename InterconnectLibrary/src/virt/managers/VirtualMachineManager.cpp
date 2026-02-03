#include "VirtualMachineManager.h"

#include <iostream>

#include "../../exceptions/VirtualizationException.h"
#include "../../utils/StringUtils.h"

/**
 * @brief Creates a new virtual machine from an XML definition.
 * 
 * @param virtualMachineXml The XML definition of the virtual machine.
 * @throws VirtualizationException if the connection is not set or VM creation fails.
 */
void VirtualMachineManager::createVirtualMachine(const std::string& virtualMachineXml) const
{
    checkIfConnectionIsSet();

    const auto domain = libvirt->createVirtualMachineFromXml(conn, virtualMachineXml.c_str());
    if (domain == nullptr)
    {
        throw VirtualizationException(libvirt->getLastError());
    }
}

/**
 * @brief Retrieves detailed information about a specific virtual machine.
 * 
 * Gets information including memory usage, state, UUID, and name for the VM.
 * 
 * @param name The name of the virtual machine.
 * @return VirtualMachineInfo Structure containing VM information.
 * @throws VirtualizationException if VM is not found or info retrieval fails.
 */
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

/**
 * @brief Retrieves information about all virtual machines.
 * 
 * Gets a list of all active and inactive virtual machines with their details.
 * 
 * @return std::vector<VirtualMachineInfo> Vector of VM information structures.
 * @throws VirtualizationException if connection is not set or retrieval fails.
 */
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

/**
 * @brief Attaches a device to a virtual machine.
 * 
 * @param uuid The UUID of the virtual machine.
 * @param deviceDefinition XML definition of the device to attach.
 * @throws VirtualizationException if connection is not set or attachment fails.
 */
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

/**
 * @brief Detaches a device from a virtual machine.
 * 
 * @param uuid The UUID of the virtual machine.
 * @param deviceDefinition XML definition of the device to detach.
 * @throws VirtualizationException if connection is not set or detachment fails.
 */
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

/**
 * @brief Updates a device configuration on a virtual machine.
 * 
 * @param uuid The UUID of the virtual machine.
 * @param deviceDefinition Updated XML definition of the device.
 * @throws VirtualizationException if connection is not set or update fails.
 */
void VirtualMachineManager::updateVmDevice(const std::string& uuid, const std::string& deviceDefinition) const
{
    checkIfConnectionIsSet();

    const auto domainPtr = getVirtualMachineByUuid(uuid);

    if (libvirt->updateVmDevice(domainPtr, deviceDefinition) == -1)
    {
        throw VirtualizationException("Can't modify device");
    }
}

/**
 * @brief Looks up a virtual machine by name.
 * 
 * @param name The name of the virtual machine.
 * @return virDomainPtr Pointer to the virtual machine domain.
 * @throws VirtualizationException if the VM is not found.
 */
virDomainPtr VirtualMachineManager::getVirtualMachineByName(const std::string& name) const
{
    const auto domainPtr = libvirt->domainLookupByName(conn, name);
    if (domainPtr == nullptr)
    {
        throw VirtualizationException("Error while obtaining pointer to virtual machine");
    }
    return domainPtr;
}

/**
 * @brief Looks up a virtual machine by UUID.
 * 
 * @param uuid The UUID of the virtual machine.
 * @return virDomainPtr Pointer to the virtual machine domain.
 * @throws VirtualizationException if the VM is not found.
 */
virDomainPtr VirtualMachineManager::getVirtualMachineByUuid(const std::string& uuid) const
{
    const auto domainPtr = libvirt->domainLookupByUuid(conn, uuid);
    if (domainPtr == nullptr)
    {
        throw VirtualizationException("Error while obtaining pointer to virtual machine");
    }
    return domainPtr;
}
