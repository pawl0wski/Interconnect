#ifndef ILIBVIRTWRAPPER_H
#define ILIBVIRTWRAPPER_H
#include <libvirt/libvirt.h>

/**
 * @brief Interface for Libvirt wrapper functionality.
 *
 * This abstract class defines an interface for interacting with
 * the libvirt API.
 *
 * It is primarily used for mocking during tests.
 */
class ILibvirtWrapper {
public:
    virtual ~ILibvirtWrapper() = default;

    virtual virConnectPtr connectOpen(const char *name) = 0;

    virtual virDomainPtr createVirtualMachineFromXml(virConnectPtr conn, const char *xmlConfig) = 0;

    virtual void getUuidFromDomain(virDomainPtr domain, char *uuid) = 0;
};


#endif // ILIBVIRTWRAPPER_H
