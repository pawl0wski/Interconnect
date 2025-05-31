#ifndef LIBVIRTWRAPPERMOCK_H
#define LIBVIRTWRAPPERMOCK_H
#include <gmock/gmock.h>
#include "interfaces/ILibvirtWrapper.h"


class LibvirtWrapperMock : public ILibvirtWrapper {
public:
    MOCK_METHOD(virConnectPtr, connectOpen, (const char *name), (override));
    MOCK_METHOD(virDomainPtr, createVirtualMachineFromXml, (virConnectPtr conn, const char *xmlConfig), (override));
    MOCK_METHOD(void, getUuidFromDomain, (virDomainPtr domain, char *uuid), (override));
};


#endif //LIBVIRTWRAPPERMOCK_H
