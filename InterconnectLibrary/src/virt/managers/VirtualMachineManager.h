#ifndef VIRTUALMACHINEMANAGER_H
#define VIRTUALMACHINEMANAGER_H
#include <string>
#include <vector>
#include <libvirt/libvirt.h>

#include "BaseManagerWithConnection.h"
#include "../../models/VirtualMachineInfo.h"

/**
 * @brief Manages virtual machines.
 *
 * This class handles the connection to the libvirt backend
 * and operations related to virtual machines such as creation
 * and retrieving information.
 */
class VirtualMachineManager : public BaseManagerWithConnection
{
public:
    using BaseManagerWithConnection::BaseManagerWithConnection;

    /**
     * @brief Creates a virtual machine based on an XML configuration.
     *
     * @param virtualMachineXml XML string describing the virtual machine configuration.
     */
    void createVirtualMachine(const std::string& virtualMachineXml) const;

    /**
     * @brief Retrieves information about a virtual machine identified by its name.
     *
     * @param name Name string of the virtual machine.
     * @return VirtualMachineInfo Information about the virtual machine.
     */
    virtual VirtualMachineInfo getInfoAboutVirtualMachine(const std::string& name);

    /**
     * @brief Retrieves a list of all virtual machines with their information.
     *
     * @return std::vector<VirtualMachineInfo> Vector containing information about all VMs.
     */
    std::vector<VirtualMachineInfo> getListOfVirtualMachinesWithInfo();

    /**
     * @brief Attaches a device to a virtual machine.
     *
     * @param uuid UUID of the virtual machine.
     * @param deviceDefinition XML string defining the device to attach.
     */
    void attachDeviceToVirtualMachine(const std::string& uuid, const std::string& deviceDefinition) const;

    /**
     * @brief Detaches a device from a virtual machine.
     *
     * @param uuid UUID of the virtual machine.
     * @param deviceDefinition XML string defining the device to detach.
     */
    void detachDeviceFromVirtualMachine(const std::string& uuid, const std::string& deviceDefinition) const;

    /**
     * @brief Updates a device configuration in a virtual machine.
     *
     * @param uuid UUID of the virtual machine.
     * @param deviceDefinition XML string defining the new device configuration.
     */
    void updateVmDevice(const std::string& uuid, const std::string& deviceDefinition) const;

private:
    /**
     * @brief Retrieves a domain handle by virtual machine name.
     *
     * @param name Name of the virtual machine.
     * @return virDomainPtr Domain handle.
     */
    [[nodiscard]] virDomainPtr getVirtualMachineByName(const std::string& name) const;

    /**
     * @brief Retrieves a domain handle by virtual machine UUID.
     *
     * @param uuid UUID of the virtual machine.
     * @return virDomainPtr Domain handle.
     */
    [[nodiscard]] virDomainPtr getVirtualMachineByUuid(const std::string& uuid) const;
};

#endif //VIRTUALMACHINEMANAGER_H
