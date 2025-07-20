#ifndef ILIBVIRTWRAPPER_H
#define ILIBVIRTWRAPPER_H
#include <string>
#include <libvirt/libvirt.h>

/**
 * @brief Interface for Libvirt wrapper functionality.
 *
 * This abstract class defines an interface for interacting with
 * the libvirt API.
 *
 * It is primarily used for mocking during tests.
 */
class ILibvirtWrapper
{
public:
    virtual ~ILibvirtWrapper() = default;

    virtual virConnectPtr connectOpen(const char* name) = 0;

    virtual virDomainPtr createVirtualMachineFromXml(virConnectPtr conn, const char* xmlConfig) = 0;

    virtual void getUuidFromDomain(virDomainPtr domain, char* uuid) = 0;

    virtual int getNodeInfo(virConnectPtr conn, virNodeInfoPtr info) = 0;

    virtual int getLibVersion(virConnectPtr conn, unsigned long* version) = 0;

    virtual int getDriverVersion(virConnectPtr conn, unsigned long* version) = 0;

    virtual std::string getConnectUrl(virConnectPtr conn) = 0;

    virtual std::string getDriverType(virConnectPtr conn) = 0;

    virtual std::string getLastError() = 0;

    virtual virDomainPtr domainLookupByName(virConnectPtr conn, std::string name) = 0;

    virtual int domainGetInfo(virDomainPtr domain, virDomainInfo& domainInfo) = 0;

    virtual int getDomainUUID(virDomainPtr domain, std::string& uuid) = 0;

    virtual int getListOfAllDomains(virConnectPtr conn, virDomainPtr** domains) = 0;

    virtual std::string getDomainName(virDomainPtr domain) = 0;

    virtual void freeDomain(virDomainPtr domain) = 0;

    virtual int connectionIsAlive(virConnectPtr conn) = 0;
};


#endif // ILIBVIRTWRAPPER_H
