#include <gtest/gtest.h>

#include "TestingUtils.h"
#include "VirtualMachineManager.h"
#include "exceptions/VirtualMachineManagerException.h"

#include "mocks/LibvirtWrapperMock.h"
#include "mocks/VirtualMachineManagerMockGetInfoAboutVirtualMachine.h"

class VirtualMachineManagerTests : public testing::Test
{
protected:
    LibvirtWrapperMock mockLibvirt;
    VirtualMachineManager manager;

    VirtualMachineManagerTests()
        : manager(&mockLibvirt)
    {
    }
};

TEST_F(VirtualMachineManagerTests, initializeConnection_WhenConnectionPathIsNotSet_ShouldConnectToDefaultVMBackend)
{
    EXPECT_CALL(mockLibvirt, connectOpen(testing::StrEq("qemu:///system")))
            .WillOnce(testing::Return(reinterpret_cast<virConnectPtr>(0x1234)));

    EXPECT_NO_THROW(manager.initializeConnection());
}

TEST_F(VirtualMachineManagerTests,
       initializeConnection_WhenCustomConnectionPathProvided_ShouldConnectToCustomVMBackend)
{
    EXPECT_CALL(mockLibvirt, connectOpen(testing::StrEq("test:///testing"))).WillOnce(
        testing::Return(reinterpret_cast<virConnectPtr>(0x1234)));

    EXPECT_NO_THROW(manager.initializeConnection("test:///testing"));
}

TEST_F(VirtualMachineManagerTests, initializeConnection_WhenConnectionFails_ShouldThrowConnectionToVMBackendFailed)
{
    EXPECT_CALL(mockLibvirt, connectOpen(testing::StrEq("qemu:///system"))).WillOnce(
        testing::Return(reinterpret_cast<virConnectPtr>(0x0)));

    TestingUtils::expectThrowWithMessage([this]
    {
        manager.initializeConnection();
    }, "An error occurred while connecting to qemu:///system");
}

TEST_F(VirtualMachineManagerTests, createVirtualMachine_WhenNoBackendConnection_ShouldThrowNoActiveVMBackendConnection)
{
    TestingUtils::expectThrowWithMessage([this]
    {
        manager.createVirtualMachine("<xml>test</xml>");
    }, "No active connection to the VM backend.");
}


TEST_F(VirtualMachineManagerTests,
       createVirtualMachine_WhenXMLProvidedButErrorInLibvirt_ShouldThrowCreateVirtualMachineFailed)
{
    EXPECT_CALL(mockLibvirt, connectOpen(testing::_))
            .WillOnce(testing::Return(reinterpret_cast<virConnectPtr>(0x1234)));
    EXPECT_CALL(mockLibvirt, createVirtualMachineFromXml(testing::_, testing::_))
        .WillOnce(testing::Return(nullptr));
    EXPECT_CALL(mockLibvirt, getLastError()).WillOnce(testing::Throw(std::runtime_error("MockLibVirtException")));

    manager.initializeConnection();
    TestingUtils::expectThrowWithMessage(
        [this]
        {
            manager.createVirtualMachine("<xml>test</xml>");
        }, "MockLibVirtException");
}

TEST_F(VirtualMachineManagerTests, createVirtualMachine_WhenVirtualMachineCreated_ShouldCallForVirtualMachineInfo)
{
    VirtualMachineManagerMockGetInfoAboutVirtualMachine customManager(&mockLibvirt);

    EXPECT_CALL(mockLibvirt, connectOpen(testing::_))
            .WillOnce(testing::Return(reinterpret_cast<virConnectPtr>(0x1234)));
    EXPECT_CALL(mockLibvirt, createVirtualMachineFromXml(testing::_, testing::_))
        .WillOnce(testing::Return(reinterpret_cast<virDomainPtr>(0x1234)));

    customManager.initializeConnection();
    customManager.createVirtualMachine("<xml>test</xml>");
}

TEST_F(VirtualMachineManagerTests, getConnectionInfo_WhenInvoked_ShouldReturnConnectionInfo)
{
    virNodeInfo mockInfo{
        .memory = 500,
        .cpus = 10,
        .mhz = 1000,
    };
    EXPECT_CALL(mockLibvirt, connectOpen(testing::StrEq("test:///testing"))).WillOnce(
        testing::Return(reinterpret_cast<virConnectPtr>(0x1234)));
    EXPECT_CALL(mockLibvirt, getDriverType(testing::_)).WillOnce(testing::Return("QEMU"));
    EXPECT_CALL(mockLibvirt, getConnectUrl(testing::_)).WillOnce(testing::Return("test:///testing"));
    EXPECT_CALL(mockLibvirt, getNodeInfo(testing::_, testing::_)).WillOnce(
        [&](virConnectPtr, virNodeInfoPtr info)
        {
            *info = mockInfo;
            return 0;
        });
    EXPECT_CALL(mockLibvirt, getLibVersion(testing::_, testing::_)).WillOnce(
        [&](virConnectPtr, unsigned long* version)
        {
            *version = 3014159;
            return 0;
        });
    EXPECT_CALL(mockLibvirt, getDriverVersion(testing::_, testing::_)).WillOnce(
        [&](virConnectPtr, unsigned long* version)
        {
            *version = 5016152;
            return 0;
        });

    manager.initializeConnection("test:///testing");
    const auto info = manager.getConnectionInfo();

    EXPECT_EQ(info.cpuCount, 10);
    EXPECT_EQ(info.cpuFreq, 1000);
    EXPECT_EQ(info.totalMemory, 500);
    EXPECT_STREQ(info.connectionUrl, "test:///testing");
    EXPECT_STREQ(info.driverType, "QEMU");
    EXPECT_THAT(info.libVersion, ::testing::Field(&Version::major, 3));
    EXPECT_THAT(info.libVersion, ::testing::Field(&Version::minor, 14));
    EXPECT_THAT(info.libVersion, ::testing::Field(&Version::patch, 159));
    EXPECT_THAT(info.driverVersion, ::testing::Field(&Version::major, 5));
    EXPECT_THAT(info.driverVersion, ::testing::Field(&Version::minor, 16));
    EXPECT_THAT(info.driverVersion, ::testing::Field(&Version::patch, 152));
}

TEST_F(VirtualMachineManagerTests, getConnectionInfo_WhenGetNodeInfoFails_ShouldThrowException)
{
    EXPECT_CALL(mockLibvirt, connectOpen(testing::StrEq("test:///testing"))).WillOnce(
        testing::Return(reinterpret_cast<virConnectPtr>(0x1234)));
    EXPECT_CALL(mockLibvirt, getDriverType(testing::_)).WillOnce(testing::Return("QEMU"));
    EXPECT_CALL(mockLibvirt, getConnectUrl(testing::_)).WillOnce(testing::Return("test:///testing"));
    EXPECT_CALL(mockLibvirt, getNodeInfo(testing::_, testing::_)).WillOnce(testing::Return(1)); // simulate failure

    manager.initializeConnection("test:///testing");

    TestingUtils::expectThrowWithMessage([this]
    {
        manager.getConnectionInfo();
    }, "An error occurred while getting node information ");
}

TEST_F(VirtualMachineManagerTests, getConnectionInfo_WhenGetLibVersionFails_ShouldThrowException)
{
    virNodeInfo mockInfo{
        .memory = 500,
        .cpus = 10,
        .mhz = 1000,
    };

    EXPECT_CALL(mockLibvirt, connectOpen(testing::StrEq("test:///testing"))).WillOnce(
        testing::Return(reinterpret_cast<virConnectPtr>(0x1234)));
    EXPECT_CALL(mockLibvirt, getDriverType(testing::_)).WillOnce(testing::Return("QEMU"));
    EXPECT_CALL(mockLibvirt, getConnectUrl(testing::_)).WillOnce(testing::Return("test:///testing"));
    EXPECT_CALL(mockLibvirt, getNodeInfo(testing::_, testing::_)).WillOnce(
        [&](virConnectPtr, virNodeInfoPtr info)
        {
            *info = mockInfo;
            return 0;
        });
    EXPECT_CALL(mockLibvirt, getLibVersion(testing::_, testing::_)).WillOnce(testing::Return(1)); // simulate failure

    manager.initializeConnection("test:///testing");


    TestingUtils::expectThrowWithMessage(
        [this]
        {
            manager.getConnectionInfo();
        }, "Failed to get lib version");
}

TEST_F(VirtualMachineManagerTests, getConnectionInfo_WhenGetDriverVersionFails_ShouldThrowException)
{
    virNodeInfo mockInfo{
        .memory = 500,
        .cpus = 10,
        .mhz = 1000,
    };

    EXPECT_CALL(mockLibvirt, connectOpen(testing::StrEq("test:///testing"))).WillOnce(
        testing::Return(reinterpret_cast<virConnectPtr>(0x1234)));
    EXPECT_CALL(mockLibvirt, getDriverType(testing::_)).WillOnce(testing::Return("QEMU"));
    EXPECT_CALL(mockLibvirt, getConnectUrl(testing::_)).WillOnce(testing::Return("test:///testing"));
    EXPECT_CALL(mockLibvirt, getNodeInfo(testing::_, testing::_)).WillOnce(
        [&](virConnectPtr, virNodeInfoPtr info)
        {
            *info = mockInfo;
            return 0;
        });
    EXPECT_CALL(mockLibvirt, getLibVersion(testing::_, testing::_)).WillOnce(
        [&](virConnectPtr, unsigned long* version)
        {
            *version = 3014159;
            return 0;
        });
    EXPECT_CALL(mockLibvirt, getDriverVersion(testing::_, testing::_)).WillOnce(testing::Return(1));

    manager.initializeConnection("test:///testing");

    TestingUtils::expectThrowWithMessage([this]
    {
        manager.getConnectionInfo();
    }, "Failed to get driver version");
}

TEST_F(VirtualMachineManagerTests, getConnectionInfo_WhenNotConnected_ShouldThrowException)
{
    TestingUtils::expectThrowWithMessage([this]
    {
        manager.getConnectionInfo();
    }, "No connected to any hypervisor");
}

TEST_F(VirtualMachineManagerTests, getInfoAboutVirtualMachine_WhenVmNotExist_ShouldThrowException)
{
    EXPECT_CALL(mockLibvirt, connectOpen(testing::StrEq("test:///testing"))).WillOnce(
        testing::Return(reinterpret_cast<virConnectPtr>(0x1234)));
    EXPECT_CALL(mockLibvirt, domainLookupByName(testing::_, testing::_)).WillOnce(testing::Return(nullptr));

    manager.initializeConnection("test:///testing");
    TestingUtils::expectThrowWithMessage([this]
    {
        manager.getInfoAboutVirtualMachine("test");
    }, "Error while obtaining pointer to virtual machine");
}

TEST_F(VirtualMachineManagerTests, getInfoAboutVirtualMachine_WhenVmExistButCantGetPointerToVmInfo_ShouldThrowException)
{
    EXPECT_CALL(mockLibvirt, connectOpen(testing::StrEq("test:///testing"))).WillOnce(
        testing::Return(reinterpret_cast<virConnectPtr>(0x1234)));
    EXPECT_CALL(mockLibvirt, domainLookupByName(testing::_, testing::_)).WillOnce(testing::Return(reinterpret_cast<virDomainPtr>(1)));
    EXPECT_CALL(mockLibvirt, domainGetInfo(testing::_, testing::_)).WillOnce(testing::Return(1));

    manager.initializeConnection("test:///testing");
    TestingUtils::expectThrowWithMessage([this]
    {
        manager.getInfoAboutVirtualMachine("test");
    }, "Virtual machine was found but error occurred while obtaining its info");
}

TEST_F(VirtualMachineManagerTests, getInfoAboutVirtualMachine_WhenVmExistButCantGetUuid_ShouldThrowException)
{
    EXPECT_CALL(mockLibvirt, connectOpen(testing::StrEq("test:///testing"))).WillOnce(
        testing::Return(reinterpret_cast<virConnectPtr>(0x1234)));
    EXPECT_CALL(mockLibvirt, domainLookupByName(testing::_, testing::_)).WillOnce(testing::Return(reinterpret_cast<virDomainPtr>(1)));
    EXPECT_CALL(mockLibvirt, domainGetInfo(testing::_, testing::_)).WillOnce(testing::Return(0));
    EXPECT_CALL(mockLibvirt, getDomainUUID(testing::_, testing::_)).WillOnce(testing::Return(1));

    manager.initializeConnection("test:///testing");
    TestingUtils::expectThrowWithMessage([this]
    {
        manager.getInfoAboutVirtualMachine("test");
    }, "Virtual machine was found but error occurred while obtaining its uuid");
}

TEST_F(VirtualMachineManagerTests, getInfoAboutVirtualMachine_WhenVmExistAndSuccessfulyObtainedInfo_ShouldReturnInfo)
{
    EXPECT_CALL(mockLibvirt, connectOpen(testing::StrEq("test:///testing"))).WillOnce(
        testing::Return(reinterpret_cast<virConnectPtr>(0x1234)));
    EXPECT_CALL(mockLibvirt, domainLookupByName(testing::_, testing::_)).WillOnce(testing::Return(reinterpret_cast<virDomainPtr>(1)));
    EXPECT_CALL(mockLibvirt, domainGetInfo(testing::_, testing::_)).WillOnce(testing::Invoke([](virDomainPtr _, virDomainInfo& domainInfo)
    {
        domainInfo.memory = 454;
        domainInfo.state = 5;
        return 0;
    }));
    EXPECT_CALL(mockLibvirt, getDomainUUID(testing::_, testing::_)).WillOnce(testing::Invoke([](virDomainPtr _, std::string& uuid)
    {
        uuid = "testUuid";
        return 0;
    }));

    manager.initializeConnection("test:///testing");
    auto info = manager.getInfoAboutVirtualMachine("test");

    EXPECT_STREQ(info.uuid, "testUuid");
    EXPECT_EQ(info.state, 5);
    EXPECT_EQ(info.usedMemory, 454);
}


