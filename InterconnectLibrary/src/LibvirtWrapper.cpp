#include "LibvirtWrapper.h"

virConnectPtr LibvirtWrapper::connectOpen(const char *connectionUri) {
    return virConnectOpen(connectionUri);
}

virDomainPtr LibvirtWrapper::createVirtualMachineFromXml(const virConnectPtr conn, const char *xmlConfig) {
    return virDomainCreateXML(conn, xmlConfig, 0);
}

void LibvirtWrapper::getUuidFromDomain(const virDomainPtr domain, char *uuid) {
    virDomainGetUUIDString(domain, uuid);
}

int LibvirtWrapper::getNodeInfo(const virConnectPtr conn, const virNodeInfoPtr info) {
    return virNodeGetInfo(conn, info);
}

int LibvirtWrapper::getLibVersion(const virConnectPtr conn, unsigned long *libVersion) {
    return virConnectGetLibVersion(conn, libVersion);
}

int LibvirtWrapper::getDriverVersion(virConnectPtr conn, unsigned long *version) {
    return virConnectGetVersion(conn, version);
}

std::string LibvirtWrapper::getConnectUrl(virConnectPtr conn) {
    return virConnectGetURI(conn);
}

std::string LibvirtWrapper::getDriverType(virConnectPtr conn) {
    return virConnectGetType(conn);
}
