#include "VirtualNetworkManager.h"

#include "../../exceptions/VirtualizationException.h"

/**
 * @brief Creates a virtual network from an XML definition.
 * 
 * @param networkDefinition XML definition of the network.
 * @return virNetworkPtr Pointer to the created network.
 * @throws VirtualizationException if connection is not set or creation fails.
 */
virNetworkPtr VirtualNetworkManager::createNetworkFromXml(const std::string& networkDefinition) const
{
    checkIfConnectionIsSet();

    const auto network = libvirt->createNetworkFromXml(conn, networkDefinition);

    if (network == nullptr)
    {
        throw VirtualizationException("Failed to create network from XML");
    }

    return network;
}

/**
 * @brief Destroys (deletes) a virtual network.
 * 
 * @param name The name of the network to destroy.
 * @throws VirtualizationException if connection is not set or destruction fails.
 */
void VirtualNetworkManager::destroyNetwork(const std::string& name) const
{
    checkIfConnectionIsSet();

    const auto network = libvirt->getNetworkByName(conn, name);

    if (libvirt->destroyNetwork(network) == -1)
    {
        throw VirtualizationException("Failed to destroy network");
    }
}

/**
 * @brief Retrieves the XML definition of a virtual network.
 * 
 * @param name The name of the network.
 * @return std::string The XML definition of the network.
 * @throws VirtualizationException if connection is not set or retrieval fails.
 */
std::string VirtualNetworkManager::getNetworkXmlDefinition(const std::string& name) const
{
    checkIfConnectionIsSet();

    const auto network = libvirt->getNetworkByName(conn, name);

    return libvirt->getNetworkDefinition(network);
}
