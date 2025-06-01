#ifndef LIBVIRTWRAPPERMOCK_H
#define LIBVIRTWRAPPERMOCK_H
#include <gmock/gmock.h>
#include "interfaces/ILibvirtWrapper.h"

class LibvirtWrapperMockTests : public ILibvirtWrapper {
public:
    MOCK_METHOD(virConnectPtr, connectOpen, (const char *name), (override));
    MOCK_METHOD(virDomainPtr, createVirtualMachineFromXml, (virConnectPtr conn, const char *xmlConfig), (override));
    MOCK_METHOD(void, getUuidFromDomain, (virDomainPtr domain, char *uuid), (override));
    MOCK_METHOD(int, getNodeInfo, (virConnectPtr conn, virNodeInfoPtr info), (override));
    MOCK_METHOD(int, getLibVersion, (virConnectPtr conn, unsigned long *version), (override));
    MOCK_METHOD(int, getDriverVersion, (virConnectPtr conn, unsigned long *version), (override));
    MOCK_METHOD(std::string, getConnectUrl, (virConnectPtr conn), (override));
    MOCK_METHOD(std::string, getDriverType, (virConnectPtr conn), (override));
};


#endif //LIBVIRTWRAPPERMOCK_H
