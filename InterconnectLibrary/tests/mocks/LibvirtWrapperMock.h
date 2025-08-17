#ifndef LIBVIRTWRAPPERMOCK_H
#define LIBVIRTWRAPPERMOCK_H
#include <gmock/gmock.h>
#include "interfaces/ILibvirtWrapper.h"

class LibvirtWrapperMock : public ILibvirtWrapper
{
public:
    MOCK_METHOD(virConnectPtr, connectOpen, (const char *name), (override));
    MOCK_METHOD(virDomainPtr, createVirtualMachineFromXml, (virConnectPtr conn, const char *xmlConfig), (override));
    MOCK_METHOD(void, getUuidFromDomain, (virDomainPtr domain, char *uuid), (override));
    MOCK_METHOD(int, getNodeInfo, (virConnectPtr conn, virNodeInfoPtr info), (override));
    MOCK_METHOD(int, getLibVersion, (virConnectPtr conn, unsigned long *version), (override));
    MOCK_METHOD(int, getDriverVersion, (virConnectPtr conn, unsigned long *version), (override));
    MOCK_METHOD(std::string, getConnectUrl, (virConnectPtr conn), (override));
    MOCK_METHOD(std::string, getDriverType, (virConnectPtr conn), (override));
    MOCK_METHOD(std::string, getLastError, (), (override));
    MOCK_METHOD(virDomainPtr, domainLookupByName, (virConnectPtr conn, std::string name), (override));
    MOCK_METHOD(int, domainGetInfo, (virDomainPtr domain, virDomainInfo& domainInfo), (override));
    MOCK_METHOD(int, getDomainUUID, (virDomainPtr domain, std::string& uuid), (override));
    MOCK_METHOD(int, getListOfAllDomains, (virConnectPtr conn, virDomainPtr **domains), (override));
    MOCK_METHOD(std::string, getDomainName, (virDomainPtr domain), (override));
    MOCK_METHOD(void, freeDomain, (virDomainPtr domain), (override));
    MOCK_METHOD(int, connectionIsAlive, (virConnectPtr conn), (override));
    MOCK_METHOD(virStreamPtr, createNewStream, (virConnectPtr conn), (override));
    MOCK_METHOD(int, openDomainConsole, (virDomainPtr domain, virStreamPtr stream), (override));
    MOCK_METHOD(virDomainPtr, domainLookupByUuid, (virConnectPtr conn, std::string& uuid), (override));
    MOCK_METHOD(int, receiveDataFromStream, (virStreamPtr stream, char* buffer, int bufferSize), (override));
    MOCK_METHOD(void, sendDataToStream, (virStreamPtr stream, char* buffer, int bufferSize), (override));
    MOCK_METHOD(void, finishAndFreeStream, (virStreamPtr stream), (override));
};


#endif //LIBVIRTWRAPPERMOCK_H
