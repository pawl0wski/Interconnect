#include "LibvirtWrapper.h"

virConnectPtr LibvirtWrapper::connectOpen(const char* connectionUri)
{
    return virConnectOpen(connectionUri);
}

virDomainPtr LibvirtWrapper::createVirtualMachineFromXml(const virConnectPtr conn, const char* xmlConfig)
{
    return virDomainCreateXML(conn, xmlConfig, 0);
}

void LibvirtWrapper::getUuidFromDomain(const virDomainPtr domain, char* uuid)
{
    virDomainGetUUIDString(domain, uuid);
}

int LibvirtWrapper::getNodeInfo(const virConnectPtr conn, const virNodeInfoPtr info)
{
    return virNodeGetInfo(conn, info);
}

int LibvirtWrapper::getLibVersion(const virConnectPtr conn, unsigned long* libVersion)
{
    return virConnectGetLibVersion(conn, libVersion);
}

int LibvirtWrapper::getDriverVersion(const virConnectPtr conn, unsigned long* version)
{
    return virConnectGetVersion(conn, version);
}

std::string LibvirtWrapper::getConnectUrl(const virConnectPtr conn)
{
    return virConnectGetURI(conn);
}

std::string LibvirtWrapper::getDriverType(const virConnectPtr conn)
{
    return virConnectGetType(conn);
}

std::string LibvirtWrapper::getLastError()
{
    const virErrorPtr err = virGetLastError();
    return std::string(err->message);
}

virDomainPtr LibvirtWrapper::domainLookupByName(const virConnectPtr conn, std::string name)
{
    return virDomainLookupByName(conn, "test");
}

int LibvirtWrapper::domainGetInfo(const virDomainPtr domain, virDomainInfo& domainInfo)
{
    return virDomainGetInfo(domain, &domainInfo);
}

int LibvirtWrapper::getDomainUUID(const virDomainPtr domain, std::string& uuid)
{
    char uuid_cstr[VIR_UUID_STRING_BUFLEN];
    const int result = virDomainGetUUIDString(domain, uuid_cstr);
    if (result == 0)
    {
        uuid = std::string(uuid_cstr);
    }
    return result;
}
