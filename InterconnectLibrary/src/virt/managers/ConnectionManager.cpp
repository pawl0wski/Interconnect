#include "ConnectionManager.h"

#include "../../exceptions/VirtualizationException.h"
#include "../../utils/StringUtils.h"
#include "../../utils/VersionUtils.h"

/**
 * @brief Initializes a connection to a hypervisor.
 * 
 * Establishes a connection to the specified hypervisor URI or uses the default
 * "qemu:///system" if no custom URI is provided.
 * 
 * @param customConnectionUrl Optional custom connection URI.
 * @throws VirtualizationException if connection fails.
 */
void ConnectionManager::initializeConnection(const std::optional<std::string>& customConnectionUrl)
{
    const auto connectionUri = customConnectionUrl.has_value() ? customConnectionUrl.value() : "qemu:///system";

    conn = libvirt->connectOpen(connectionUri.c_str());
    if (conn == nullptr)
    {
        throw VirtualizationException("An error occurred while connecting to " + connectionUri);
    }
}

/**
 * @brief Retrieves detailed information about the hypervisor connection.
 * 
 * Gathers information about the connected hypervisor including CPU count,
 * CPU frequency, memory, and version information for both libvirt and the driver.
 * 
 * @return ConnectionInfo Structure containing connection details.
 * @throws VirtualizationException if no connection is active or retrieval fails.
 */
ConnectionInfo ConnectionManager::getConnectionInfo() const
{
    if (conn == nullptr)
    {
        throw VirtualizationException("No connected to any hypervisor");
    }

    virNodeInfo info;
    unsigned long libVersion;
    unsigned long driverVersion;

    const auto driverType = libvirt->getDriverType(conn);
    const auto connectionUrl = libvirt->getConnectUrl(conn);
    if (libvirt->getNodeInfo(conn, &info) != 0)
    {
        throw VirtualizationException("An error occurred while getting node information ");
    }
    if (libvirt->getLibVersion(conn, &libVersion) != 0)
    {
        throw VirtualizationException("Failed to get lib version");
    }
    if (libvirt->getDriverVersion(conn, &driverVersion) != 0)
    {
        throw VirtualizationException("Failed to get driver version");
    }

    auto connectionInfo = ConnectionInfo{
        .cpuCount = info.cpus,
        .cpuFreq = info.mhz,
        .totalMemory = info.memory,
        .libVersion = VersionUtils::getVersion(libVersion),
        .driverVersion = VersionUtils::getVersion(driverVersion)
    };
    StringUtils::copyStringToCharArray(connectionUrl, connectionInfo.connectionUrl,
                                       sizeof(connectionInfo.connectionUrl));
    StringUtils::copyStringToCharArray(driverType, connectionInfo.driverType,
                                       sizeof(connectionInfo.driverType));

    return connectionInfo;
}

/**
 * @brief Checks if the hypervisor connection is still active.
 * 
 * @return bool True if the connection is alive, false if dead.
 * @throws VirtualizationException if an error occurs during the check.
 */
bool ConnectionManager::isConnectionAlive() const
{
    if (conn == nullptr)
    {
        return false;
    }

    const auto connectionStatus = libvirt->connectionIsAlive(conn);

    if (connectionStatus == -1)
    {
        throw VirtualizationException("Error while retrieving connection status");
    }

    return connectionStatus;
}

/**
 * @brief Gets the underlying hypervisor connection pointer.
 * 
 * @return virConnectPtr The hypervisor connection pointer.
 */
virConnectPtr ConnectionManager::getConnection() const
{
    return conn;
}
