#ifndef VIRTUALMACHINEMANAGER_H
#define VIRTUALMACHINEMANAGER_H
#include <optional>
#include <string>
#include <libvirt/libvirt.h>

#include "interfaces/ILibvirtWrapper.h"
#include "models/VirtualMachineInfo.h"

/**
 * @brief Manages virtual machines.
 *
 * This class handles the connection to the libvirt backend
 * and operations related to virtual machines such as creation
 * and retrieving information.
 */
class VirtualMachineManager {
    virConnectPtr connectPtr = nullptr; ///< Pointer to the active libvirt connection
    ILibvirtWrapper &libvirt; ///< Reference to the libvirt wrapper interface

public:
    virtual ~VirtualMachineManager() = default;

    /**
     * @param libvirt Reference to an ILibvirtWrapper implementation.
     */
    explicit VirtualMachineManager(ILibvirtWrapper &libvirt)
        : libvirt(libvirt) {
    }

    /**
     * @brief Initializes the connection to the libvirt backend.
     *
     * @param customConnectionUri Optional URI for a custom hypervisor connection.
     *                           If not provided, default connection is used.
     */
    void initializeConnection(const std::optional<std::string> &customConnectionUri = std::nullopt);

    /**
     * @brief Creates a virtual machine based on an XML configuration.
     *
     * @param virtualMachineXml XML string describing the virtual machine configuration.
     * @return VirtualMachineInfo Information about the created virtual machine.
     */
    VirtualMachineInfo createVirtualMachine(const std::string &virtualMachineXml);

    /**
     * @brief Retrieves information about a virtual machine identified by its UUID.
     *
     * @param uuid UUID string of the virtual machine.
     * @return VirtualMachineInfo Information about the virtual machine.
     */
    virtual VirtualMachineInfo getInfoAboutVirtualMachine(const std::string &uuid);
};

#endif //VIRTUALMACHINEMANAGER_H
