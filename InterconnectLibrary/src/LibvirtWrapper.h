#ifndef LIBVIRTWRAPPER_H
#define LIBVIRTWRAPPER_H
#include "interfaces/ILibvirtWrapper.h"
#include <libvirt/virterror.h>

/**
 * @brief Concrete implementation of the ILibvirtWrapper interface.
 *
 * This class provides actual implementations for connecting to the libvirt C API.
 */
class LibvirtWrapper : public ILibvirtWrapper
{
public:
    /**
     * @brief Opens a connection to the hypervisor.
     *
     * @param connectionUri The name or URI of the hypervisor to connect to.
     * @return virConnectPtr A pointer to the connection object on success.
     */
    virConnectPtr connectOpen(const char* connectionUri) override;

    /**
     * @brief Creates a virtual machine from an XML configuration.
     *
     * @param conn The active connection to the hypervisor.
     * @param xmlConfig The XML string describing the VM configuration.
     * @return virDomainPtr A pointer to the newly created virtual machine domain.
     */
    virDomainPtr createVirtualMachineFromXml(virConnectPtr conn, const char* xmlConfig) override;

    /**
     * @brief Retrieves the UUID of the given domain.
     *
     * @param domain The domain to get the UUID from.
     * @param uuid A buffer to store the resulting UUID string.
     */
    void getUuidFromDomain(virDomainPtr domain, char* uuid) override;

    int getNodeInfo(virConnectPtr conn, virNodeInfoPtr info) override;

    int getLibVersion(virConnectPtr conn, unsigned long* libVersion) override;

    int getDriverVersion(virConnectPtr conn, unsigned long* version) override;

    std::string getConnectUrl(virConnectPtr conn) override;

    std::string getDriverType(virConnectPtr conn) override;

    std::string getLastError() override;

    virDomainPtr domainLookupByName(virConnectPtr conn, std::string name) override;

    int domainGetInfo(virDomainPtr domain, virDomainInfo& domainInfo) override;

    int getDomainUUID(virDomainPtr domain, std::string& uuid) override;
};


#endif //LIBVIRTWRAPPER_H
