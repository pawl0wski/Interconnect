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
