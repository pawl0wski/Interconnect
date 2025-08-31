#ifndef BASEMANAGERWITHCONNECTION_H
#define BASEMANAGERWITHCONNECTION_H
#include "../../LibvirtWrapper.h"
#include "../../interfaces/ILibvirtWrapper.h"

class BaseManagerWithConnection
{
protected:
    ILibvirtWrapper* libvirt;
    virConnectPtr conn;

public:
    virtual ~BaseManagerWithConnection() = default;

    /**
     * @param libvirt Reference to an ILibvirtWrapper implementation.
     */
    explicit BaseManagerWithConnection(ILibvirtWrapper* libvirt)
        : libvirt(libvirt), conn(nullptr)
    {
    }

    explicit BaseManagerWithConnection(): conn(nullptr)
    {
        libvirt = new LibvirtWrapper();
    }

    void updateConnection(virConnectPtr conn);

protected:
    void checkIfConnectionIsSet() const;
};

#endif //BASEMANAGERWITHCONNECTION_H
