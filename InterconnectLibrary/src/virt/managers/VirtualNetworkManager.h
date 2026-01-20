#ifndef VIRTUALNETWORKMANAGER_H
#define VIRTUALNETWORKMANAGER_H
#include "BaseManagerWithConnection.h"
#include "../../wrappers/LibvirtWrapper.h"
#include "../../interfaces/ILibvirtWrapper.h"

/**
 * @brief Manages virtual networks.
 *
 * This class provides functionality for creating, destroying, and managing
 * virtual networks using libvirt.
 */
class VirtualNetworkManager : public BaseManagerWithConnection
{
public:
    using BaseManagerWithConnection::BaseManagerWithConnection;

    /**
     * @brief Creates a virtual network from an XML definition.
     *
     * @param networkDefinition XML string defining the network configuration.
     * @return virNetworkPtr Pointer to the created network.
     */
    virNetworkPtr createNetworkFromXml(const std::string& networkDefinition) const;

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
    [[nodiscard]] std::string getNetworkXmlDefinition(const std::string& name) const;
};

#endif //VIRTUALNETWORKMANAGER_H
