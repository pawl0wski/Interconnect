#include "VirtualizationFacade.h"

#include <algorithm>
#include <cstring>
#include <iostream>
#include <optional>

#include "../utils/StringUtils.h"

struct StreamInfo;

/**
 * @brief Constructs a VirtualizationFacade with a custom libvirt wrapper.
 * 
 * Initializes the facade with the specified libvirt wrapper implementation
 * and creates manager instances for virtual machines, consoles, connections, and networks.
 * 
 * @param libvirt Custom ILibvirtWrapper implementation to use.
 */
VirtualizationFacade::VirtualizationFacade(ILibvirtWrapper* libvirt)
{
    vmManager = new VirtualMachineManager(libvirt);
    vmConsoleManager = new VirtualMachineConsoleManager(libvirt);
    connManager = new ConnectionManager(libvirt);
    networkManager = new VirtualNetworkManager(libvirt);
}

/**
 * @brief Constructs a VirtualizationFacade with default libvirt wrapper.
 * 
 * Initializes the facade with the default LibvirtWrapper implementation
 * and creates manager instances for all virtualization components.
 */
VirtualizationFacade::VirtualizationFacade()
{
    static auto libvirt = LibvirtWrapper();
    vmManager = new VirtualMachineManager(&libvirt);
    vmConsoleManager = new VirtualMachineConsoleManager(&libvirt);
    connManager = new ConnectionManager(&libvirt);
    networkManager = new VirtualNetworkManager(&libvirt);
}

/**
 * @brief Initializes a connection to a hypervisor.
 * 
 * Establishes a connection to the specified hypervisor and updates all manager
 * instances with the new connection.
 * 
 * @param customConnectionUrl Optional custom connection URI. If nullptr, uses default "qemu:///system".
 * @throws VirtualizationException if connection fails.
 */
void VirtualizationFacade::initializeConnection(const char* customConnectionUrl) const
{
    std::optional<std::string> connectionUrl = std::nullopt;

    if (customConnectionUrl != nullptr)
    {
        connectionUrl = std::make_optional(std::string(customConnectionUrl));
    }

    connManager->initializeConnection(connectionUrl);

    const auto conn = connManager->getConnection();
    vmManager->updateConnection(conn);
    vmConsoleManager->updateConnection(conn);
    networkManager->updateConnection(conn);
}

/**
 * @brief Retrieves information about the current hypervisor connection.
 * 
 * @param infoPtr Pointer to ConnectionInfo structure to be populated with connection details.
 * @throws VirtualizationException if no active connection exists.
 */
void VirtualizationFacade::getConnectionInfo(ConnectionInfo* infoPtr) const
{
    *infoPtr = connManager->getConnectionInfo();
}

/**
 * @brief Creates a new virtual machine from an XML definition.
 * 
 * @param virtualMachineXml XML definition of the virtual machine to create.
 * @throws VirtualizationException if VM creation fails.
 */
void VirtualizationFacade::createVirtualMachine(const std::string& virtualMachineXml) const
{
    vmManager->createVirtualMachine(virtualMachineXml);
}

/**
 * @brief Retrieves information about a specific virtual machine.
 * 
 * @param virtualMachineInfo Pointer to VirtualMachineInfo structure to be populated.
 * @param name The name of the virtual machine.
 * @throws VirtualizationException if VM is not found or info retrieval fails.
 */
void VirtualizationFacade::getInfoAboutVirtualMachine(VirtualMachineInfo* virtualMachineInfo,
                                                      const std::string& name) const
{
    *virtualMachineInfo = vmManager->getInfoAboutVirtualMachine(name);
}

/**
 * @brief Retrieves information about all virtual machines.
 * 
 * @param arrayOfVirtualMachines Pointer to array of VirtualMachineInfo structures to be populated.
 * @param numberOfVirtualMachines Pointer to store the count of virtual machines.
 * @throws VirtualizationException if retrieval fails.
 */
void VirtualizationFacade::getListOfVirtualMachinesWithInfo(VirtualMachineInfo** arrayOfVirtualMachines,
                                                            int* numberOfVirtualMachines) const
{
    static std::vector<VirtualMachineInfo> vectorOfVirtualMachines;
    vectorOfVirtualMachines = vmManager->
        getListOfVirtualMachinesWithInfo();
    *arrayOfVirtualMachines = vectorOfVirtualMachines.data();
    *numberOfVirtualMachines = static_cast<int>(vectorOfVirtualMachines.size());
}

/**
 * @brief Checks if the hypervisor connection is still active.
 * 
 * @param isAlive Pointer to boolean to be set with connection status.
 * @return bool True if connection is alive, false otherwise.
 */
void VirtualizationFacade::isConnectionAlive(bool* isAlive) const
{
    *isAlive = connManager->isConnectionAlive();
}

/**
 * @brief Opens a console connection to a virtual machine.
 * 
 * @param vmUuid The UUID of the virtual machine.
 * @return virStreamPtr Stream pointer for console communication.
 * @throws VirtualizationException if console opening fails.
 */
virStreamPtr VirtualizationFacade::openVirtualMachineConsole(const std::string& vmUuid) const
{
    return vmConsoleManager->openVirtualMachineConsole(vmUuid);
}

/**
 * @brief Receives data from a virtual machine console.
 * 
 * @param stream The console stream.
 * @param streamData Pointer to StreamData structure to receive the data.
 */
void VirtualizationFacade::receiveDataFromConsole(virStreamPtr stream, StreamData* streamData) const
{
    vmConsoleManager->getDataFromStream(stream, streamData);
}

/**
 * @brief Sends data to a virtual machine console.
 * 
 * @param stream The console stream.
 * @param data The data to send.
 */
void VirtualizationFacade::sendDataToConsole(virStreamPtr stream, const std::string& data) const
{
    vmConsoleManager->sendDataToStream(stream, data.c_str(), data.length());
}

/**
 * @brief Closes a console stream.
 * 
 * @param stream The stream to close.
 */
void VirtualizationFacade::closeStream(virStreamPtr stream) const
{
    vmConsoleManager->closeStream(stream);
}

/**
 * @brief Creates a virtual network from an XML definition.
 * 
 * @param networkDefinition XML definition of the network.
 * @return virNetworkPtr Pointer to the created network.
 * @throws VirtualizationException if network creation fails.
 */
virNetworkPtr VirtualizationFacade::createVirtualNetworkFromXml(const std::string& networkDefinition) const
{
    return networkManager->createNetworkFromXml(networkDefinition);
}

/**
 * @brief Attaches a device to a virtual machine.
 * 
 * @param uuid The UUID of the virtual machine.
 * @param deviceDefinition XML definition of the device.
 * @throws VirtualizationException if device attachment fails.
 */
void VirtualizationFacade::attachDeviceToVm(const std::string& uuid, const std::string& deviceDefinition) const
{
    vmManager->attachDeviceToVirtualMachine(uuid, deviceDefinition);
}

/**
 * @brief Detaches a device from a virtual machine.
 * 
 * @param uuid The UUID of the virtual machine.
 * @param deviceDefinition XML definition of the device.
 * @throws VirtualizationException if device detachment fails.
 */
void VirtualizationFacade::detachDeviceFromVm(const std::string& uuid, const std::string& deviceDefinition) const
{
    vmManager->detachDeviceFromVirtualMachine(uuid, deviceDefinition);
}

/**
 * @brief Updates a device configuration on a virtual machine.
 * 
 * @param uuid The UUID of the virtual machine.
 * @param deviceDefinition Updated XML definition of the device.
 * @throws VirtualizationException if device update fails.
 */
void VirtualizationFacade::updateVmDevice(const std::string& uuid, const std::string& deviceDefinition) const
{
    vmManager->updateVmDevice(uuid, deviceDefinition);
}

/**
 * @brief Destroys (deletes) a virtual network.
 * 
 * @param name The name of the network.
 * @throws VirtualizationException if network destruction fails.
 */
void VirtualizationFacade::destroyNetwork(const std::string& name) const
{
    networkManager->destroyNetwork(name);
}

/**
 * @brief Retrieves the XML definition of a network.
 * 
 * @param name The name of the network.
 * @return std::string The XML definition of the network.
 * @throws VirtualizationException if retrieval fails.
 */
std::string VirtualizationFacade::getNetworkDefinition(const std::string& name) const
{
    return networkManager->getNetworkXmlDefinition(name);
}
