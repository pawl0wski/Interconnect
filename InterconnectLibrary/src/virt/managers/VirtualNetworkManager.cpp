#include "VirtualNetworkManager.h"

#include "../../exceptions/VirtualMachineManagerException.h"

virNetworkPtr VirtualNetworkManager::createNetworkFromXml(const std::string& networkDefinition) const
{
    checkIfConnectionIsSet();

    const auto network = libvirt->createNetworkFromXml(conn, networkDefinition);

    if (network == nullptr)
    {
        throw VirtualMachineManagerException("Failed to create network from XML");
    }

    return network;
}

void VirtualNetworkManager::destroyNetwork(const std::string& name) const
{
    checkIfConnectionIsSet();

    const auto network = libvirt->getNetworkByName(conn, name);

    if (libvirt->destroyNetwork(network) == -1)
    {
        throw VirtualMachineManagerException("Failed to destroy network");
    }
}
