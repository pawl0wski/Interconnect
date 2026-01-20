#ifndef VIRTUALIZATIONFACADE_H
#define VIRTUALIZATIONFACADE_H
#include "managers/ConnectionManager.h"
#include "managers/VirtualMachineConsoleManager.h"
#include "managers/VirtualMachineManager.h"
#include "../models/StreamData.h"
#include "managers/VirtualNetworkManager.h"

/**
 * @brief Facade class providing a unified interface for virtualization operations.
 *
 * This class serves as a high-level interface that coordinates multiple manager classes
 * to provide comprehensive virtualization functionality including connection management,
 * virtual machine operations, console access, and network management.
 */
class VirtualizationFacade
{
    ConnectionManager* connManager = nullptr;
    VirtualMachineManager* vmManager = nullptr;
    VirtualMachineConsoleManager* vmConsoleManager = nullptr;
    VirtualNetworkManager* networkManager = nullptr;

public:
    virtual ~VirtualizationFacade() = default;

    /**
     * @brief Constructs a VirtualizationFacade with a specific libvirt wrapper.
     *
     * @param libvirt Pointer to an ILibvirtWrapper implementation.
     */
    explicit VirtualizationFacade(ILibvirtWrapper* libvirt);

    /**
     * @brief Constructs a VirtualizationFacade with default libvirt wrapper.
     */
    explicit VirtualizationFacade();

    /**
     * @brief Initializes the connection to the hypervisor.
     *
     * @param customConnectionUrl Optional custom connection URL. If nullptr, default connection is used.
     */
    void initializeConnection(const char* customConnectionUrl) const;

    /**
     * @brief Retrieves detailed information about the hypervisor connection.
     *
     * @param infoPtr Pointer to ConnectionInfo structure to be filled with connection data.
     */
    void getConnectionInfo(ConnectionInfo* infoPtr) const;

    /**
     * @brief Creates a new virtual machine from XML definition.
     *
     * @param virtualMachineXml XML string defining the virtual machine configuration.
     */
    void createVirtualMachine(const std::string& virtualMachineXml) const;

    /**
     * @brief Retrieves information about a specific virtual machine.
     *
     * @param virtualMachineInfo Pointer to VirtualMachineInfo structure to be filled.
     * @param name Name of the virtual machine.
     */
    void getInfoAboutVirtualMachine(VirtualMachineInfo* virtualMachineInfo, const std::string& name) const;

    /**
     * @brief Retrieves a list of all virtual machines with their information.
     *
     * @param arrayOfVirtualMachines Pointer to array that will be allocated and filled with VM info.
     * @param numberOfVirtualMachines Pointer to integer that will store the count of VMs.
     */
    void getListOfVirtualMachinesWithInfo(VirtualMachineInfo** arrayOfVirtualMachines,
                                          int* numberOfVirtualMachines) const;

    /**
     * @brief Checks if the hypervisor connection is alive.
     *
     * @param isAlive Pointer to boolean that will store the connection status.
     */
    void isConnectionAlive(bool* isAlive) const;

    /**
     * @brief Opens a console stream to a virtual machine.
     *
     * @param vmUuid UUID of the virtual machine to connect to.
     * @return virStreamPtr Pointer to the opened console stream.
     */
    [[nodiscard]] virStreamPtr openVirtualMachineConsole(const std::string& vmUuid) const;

    /**
     * @brief Receives data from a virtual machine console.
     *
     * @param stream Active console stream to read from.
     * @param streamData Pointer to StreamData structure to be filled with received data.
     */
    void receiveDataFromConsole(virStreamPtr stream, StreamData* streamData) const;

    /**
     * @brief Sends data to a virtual machine console.
     *
     * @param stream Active console stream to write to.
     * @param data Data string to send to the console.
     */
    void sendDataToConsole(virStreamPtr stream, const std::string& data) const;

    /**
     * @brief Closes a console stream.
     *
     * @param stream Console stream to close.
     */
    void closeStream(virStreamPtr stream) const;

    /**
     * @brief Creates a virtual network from XML definition.
     *
     * @param networkDefinition XML string defining the network configuration.
     * @return virNetworkPtr Pointer to the created network.
     */
    [[nodiscard]] virNetworkPtr createVirtualNetworkFromXml(const std::string& networkDefinition) const;

    /**
     * @brief Attaches a device to a virtual machine.
     *
     * @param uuid UUID of the virtual machine.
     * @param deviceDefinition XML string defining the device to attach.
     */
    void attachDeviceToVm(const std::string& uuid, const std::string& deviceDefinition) const;

    /**
     * @brief Detaches a device from a virtual machine.
     *
     * @param uuid UUID of the virtual machine.
     * @param deviceDefinition XML string defining the device to detach.
     */
    void detachDeviceFromVm(const std::string& uuid, const std::string& deviceDefinition) const;

    /**
     * @brief Updates a device configuration in a virtual machine.
     *
     * @param uuid UUID of the virtual machine.
     * @param deviceDefinition XML string defining the new device configuration.
     */
    void updateVmDevice(const std::string& uuid, const std::string& deviceDefinition) const;

    /**
     * @brief Destroys (stops and removes) a virtual network.
     *
     * @param name Name of the network to destroy.
     */
    void destroyNetwork(const std::string& name) const;

    /**
     * @brief Retrieves the XML definition of a virtual network.
     *
     * @param name Name of the network.
     * @return std::string XML definition of the network.
     */
    [[nodiscard]] std::string getNetworkDefinition(const std::string& name) const;
};

#endif //VIRTUALIZATIONFACADE_H
