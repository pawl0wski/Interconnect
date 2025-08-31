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
     * @param name name string of the virtual machine.
     * @return VirtualMachineInfo Information about the virtual machine.
     */
    virtual VirtualMachineInfo getInfoAboutVirtualMachine(const std::string& name);

    std::vector<VirtualMachineInfo> getListOfVirtualMachinesWithInfo();

    void attachDeviceToVirtualMachine(const std::string& name, const std::string& deviceDefinition) const;

private:
    [[nodiscard]] virDomainPtr getVirtualMachineByName(const std::string& name) const;
};

#endif //VIRTUALMACHINEMANAGER_H
