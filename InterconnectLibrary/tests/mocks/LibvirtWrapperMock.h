#ifndef LIBVIRTWRAPPERMOCK_H
#define LIBVIRTWRAPPERMOCK_H
#include <gmock/gmock.h>
#include "interfaces/ILibvirtWrapper.h"


class LibvirtWrapperMock : public ILibvirtWrapper {
public:
    MOCK_METHOD(virConnectPtr, connectOpen, (const char *name), (override));
};


#endif //LIBVIRTWRAPPERMOCK_H
