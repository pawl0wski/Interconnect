#ifndef CONNECTIONMANAGER_H
#define CONNECTIONMANAGER_H
#include <optional>
#include <string>

#include "../../LibvirtWrapper.h"
#include "../../models/ConnectionInfo.h"


class ConnectionManager
{
    ILibvirtWrapper* libvirt;
    virConnectPtr conn = nullptr;

public:
    virtual ~ConnectionManager() = default;

    /**
     * @param libvirt Reference to an ILibvirtWrapper implementation.
     */
    explicit ConnectionManager(ILibvirtWrapper* libvirt)
        : libvirt(libvirt)
    {
    }

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

    ConnectionInfo getConnectionInfo() const;

    bool isConnectionAlive() const;

    virConnectPtr getConnection() const;
};


#endif //CONNECTIONMANAGER_H
