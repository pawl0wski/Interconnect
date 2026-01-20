#ifndef BASEMANAGERWITHCONNECTION_H
#define BASEMANAGERWITHCONNECTION_H
#include "../../wrappers/LibvirtWrapper.h"
#include "../../interfaces/ILibvirtWrapper.h"

/**
 * @brief Base class for managers that require a libvirt connection.
 *
 * This abstract class provides common functionality for managers that need
 * access to a libvirt connection and wrapper. It serves as a foundation
 * for specific manager classes like VirtualMachineManager and VirtualNetworkManager.
 */
class BaseManagerWithConnection
{
protected:
    ILibvirtWrapper* libvirt;
    virConnectPtr conn;

public:
    virtual ~BaseManagerWithConnection() = default;

    /**
     * @brief Constructs a BaseManagerWithConnection with a specific libvirt wrapper.
     *
     * @param libvirt Pointer to an ILibvirtWrapper implementation.
     */
    explicit BaseManagerWithConnection(ILibvirtWrapper* libvirt)
        : libvirt(libvirt), conn(nullptr)
    {
    }

    /**
     * @brief Constructs a BaseManagerWithConnection with default libvirt wrapper.
     */
    explicit BaseManagerWithConnection(): conn(nullptr)
    {
        libvirt = new LibvirtWrapper();
    }

    /**
     * @brief Updates the libvirt connection pointer.
     *
     * @param conn New connection pointer to use for operations.
     */
    void updateConnection(virConnectPtr conn);

protected:
    /**
     * @brief Checks if the connection is set and throws an exception if not.
     */
    void checkIfConnectionIsSet() const;
};

#endif //BASEMANAGERWITHCONNECTION_H
