#include "ConnectionManager.h"

#include "exceptions/VirtualMachineManagerException.h"
#include "utils/StringUtils.h"
#include "utils/VersionUtils.h"

void ConnectionManager::initializeConnection(const std::optional<std::string>& customConnectionUrl)
{
    const auto connectionUri = customConnectionUrl.has_value() ? customConnectionUrl.value() : "qemu:///system";

    conn = libvirt->connectOpen(connectionUri.c_str());
    if (conn == nullptr)
    {
        throw VirtualMachineManagerException("An error occurred while connecting to " + connectionUri);
    }
}

ConnectionInfo ConnectionManager::getConnectionInfo() const
{
    if (conn == nullptr)
    {
        throw VirtualMachineManagerException("No connected to any hypervisor");
    }

    virNodeInfo info;
    unsigned long libVersion;
    unsigned long driverVersion;

    const auto driverType = libvirt->getDriverType(conn);
    const auto connectionUrl = libvirt->getConnectUrl(conn);
    if (libvirt->getNodeInfo(conn, &info) != 0)
    {
        throw VirtualMachineManagerException("An error occurred while getting node information ");
    }
    if (libvirt->getLibVersion(conn, &libVersion) != 0)
    {
        throw VirtualMachineManagerException("Failed to get lib version");
    }
    if (libvirt->getDriverVersion(conn, &driverVersion) != 0)
    {
        throw VirtualMachineManagerException("Failed to get driver version");
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

bool ConnectionManager::isConnectionAlive() const
{
    if (conn == nullptr)
    {
        return false;
    }

    const auto connectionStatus = libvirt->connectionIsAlive(conn);

    if (connectionStatus == -1)
    {
        throw VirtualMachineManagerException("Error while retrieving connection status");
    }

    return connectionStatus;
}

virConnectPtr ConnectionManager::getConnection() const
{
    return conn;
}
