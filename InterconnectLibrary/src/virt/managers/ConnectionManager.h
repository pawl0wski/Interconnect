#ifndef CONNECTIONMANAGER_H
#define CONNECTIONMANAGER_H
#include <optional>
#include <string>

#include "../../wrappers/LibvirtWrapper.h"
#include "../../models/ConnectionInfo.h"

/**
 * @brief Manages connections to libvirt hypervisors.
 *
 * This class handles the lifecycle of connections to libvirt, including
 * initialization, status checking, and retrieving connection information.
 */
class ConnectionManager
{
    ILibvirtWrapper* libvirt;
    virConnectPtr conn = nullptr;

public:
    virtual ~ConnectionManager() = default;

    /**
     * @brief Constructs a ConnectionManager with a specific libvirt wrapper.
     *
     * @param libvirt Pointer to an ILibvirtWrapper implementation.
     */
    explicit ConnectionManager(ILibvirtWrapper* libvirt)
        : libvirt(libvirt)
    {
    }

    /**
     * @brief Constructs a ConnectionManager with default libvirt wrapper.
     */
    explicit ConnectionManager()
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

    /**
     * @brief Retrieves detailed information about the current connection.
     *
     * @return ConnectionInfo Structure containing connection and host information.
     */
    ConnectionInfo getConnectionInfo() const;

    /**
     * @brief Checks if the connection is alive and active.
     *
     * @return bool True if connection is alive, false otherwise.
     */
    bool isConnectionAlive() const;

    /**
     * @brief Gets the raw libvirt connection pointer.
     *
     * @return virConnectPtr Pointer to the libvirt connection.
     */
    virConnectPtr getConnection() const;
};


#endif //CONNECTIONMANAGER_H
