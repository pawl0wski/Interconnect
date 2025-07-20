#ifndef VIRTUALMACHINEMANAGER_H
#define VIRTUALMACHINEMANAGER_H
#include <optional>
#include <string>
#include <vector>
#include <libvirt/libvirt.h>

#include "LibvirtWrapper.h"
#include "interfaces/ILibvirtWrapper.h"
#include "models/ConnectionInfo.h"
#include "models/VirtualMachineInfo.h"

/**
 * @brief Manages virtual machines.
 *
 * This class handles the connection to the libvirt backend
 * and operations related to virtual machines such as creation
 * and retrieving information.
 */
class VirtualMachineManager
{
    virConnectPtr conn = nullptr; ///< Pointer to the active libvirt connection
    ILibvirtWrapper* libvirt; ///< Reference to the libvirt wrapper interface

public:
    virtual ~VirtualMachineManager() = default;

    /**
     * @param libvirt Reference to an ILibvirtWrapper implementation.
     */
    explicit VirtualMachineManager(ILibvirtWrapper* libvirt)
        : libvirt(libvirt)
    {
    }

    explicit VirtualMachineManager()
    {
        libvirt = new LibvirtWrapper();
    }

    /**
     * @brief Initializes the connection to the libvirt backend.
     *
     * @param customConnectionUrl Optional URL for a custom hypervisor connection.
     *                           If not provided, default connection is used.
     */
    void initializeConnection(const std::optional<std::string>& customConnectionUrl = std::nullopt);

    ConnectionInfo getConnectionInfo() const;

    /**
     * @brief Creates a virtual machine based on an XML configuration.
     *
     * @param virtualMachineXml XML string describing the virtual machine configuration.
     */
    void createVirtualMachine(const std::string& virtualMachineXml) const;

    /**
     * @brief Retrieves information about a virtual machine identified by its name.
     *
     * @param name name string of the virtual machine.
     * @return VirtualMachineInfo Information about the virtual machine.
     */
    virtual VirtualMachineInfo getInfoAboutVirtualMachine(const std::string& name);

    std::vector<VirtualMachineInfo> getListOfVirtualMachinesWithInfo();

    bool isConnectionAlive() const;
};

#endif //VIRTUALMACHINEMANAGER_H
