#include <gtest/gtest.h>

#include "TestingUtils.h"
#include "mocks/LibvirtWrapperMock.h"
#include "virt/managers/ConnectionManager.h"

class ConnectionManagerTests : public testing::Test
{
protected:
    LibvirtWrapperMock mockLibvirt;
    ConnectionManager manager;

    ConnectionManagerTests()
        : manager(&mockLibvirt)
    {
    }
};

TEST_F(ConnectionManagerTests, initializeConnection_WhenConnectionPathIsNotSet_ShouldConnectToDefaultVMBackend)
{
    EXPECT_CALL(mockLibvirt, connectOpen(testing::StrEq("qemu:///system")))
            .WillOnce(testing::Return(reinterpret_cast<virConnectPtr>(0x1234)));

    EXPECT_NO_THROW(manager.initializeConnection());
}

TEST_F(ConnectionManagerTests,
       initializeConnection_WhenCustomConnectionPathProvided_ShouldConnectToCustomVMBackend)
{
    EXPECT_CALL(mockLibvirt, connectOpen(testing::StrEq("test:///testing"))).WillOnce(
        testing::Return(reinterpret_cast<virConnectPtr>(0x1234)));

    EXPECT_NO_THROW(manager.initializeConnection("test:///testing"));
}

TEST_F(ConnectionManagerTests, initializeConnection_WhenConnectionFails_ShouldThrowConnectionToVMBackendFailed)
{
    EXPECT_CALL(mockLibvirt, connectOpen(testing::StrEq("qemu:///system"))).WillOnce(
        testing::Return(reinterpret_cast<virConnectPtr>(0x0)));

    TestingUtils::expectThrowWithMessage([this]
    {
        manager.initializeConnection();
    }, "An error occurred while connecting to qemu:///system");
}

TEST_F(ConnectionManagerTests, getConnectionInfo_WhenInvoked_ShouldReturnConnectionInfo)
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

TEST_F(ConnectionManagerTests, getConnectionInfo_WhenGetNodeInfoFails_ShouldThrowException)
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

TEST_F(ConnectionManagerTests, getConnectionInfo_WhenGetLibVersionFails_ShouldThrowException)
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

TEST_F(ConnectionManagerTests, getConnectionInfo_WhenGetDriverVersionFails_ShouldThrowException)
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

TEST_F(ConnectionManagerTests, getConnectionInfo_WhenNotConnected_ShouldThrowException)
{
    TestingUtils::expectThrowWithMessage([this]
    {
        manager.getConnectionInfo();
    }, "No connected to any hypervisor");
}

TEST_F(ConnectionManagerTests, isConnectionAlive_WhenWhileRetrievingStatusErrorOccured_ShouldThrowException)
{
    EXPECT_CALL(mockLibvirt, connectOpen(testing::StrEq("test:///testing"))).WillOnce(
        testing::Return(reinterpret_cast<virConnectPtr>(0x1234)));
    EXPECT_CALL(mockLibvirt, connectionIsAlive(testing::_)).WillOnce(testing::Return(-1));

    manager.initializeConnection("test:///testing");

    TestingUtils::expectThrowWithMessage([this]
    {
        manager.isConnectionAlive();
    }, "Error while retrieving connection status");
}

TEST_F(ConnectionManagerTests, isConnectionAlive_WhenConnectionIsDead_ShouldReturnFalse)
{
    EXPECT_CALL(mockLibvirt, connectOpen(testing::StrEq("test:///testing"))).WillOnce(
        testing::Return(reinterpret_cast<virConnectPtr>(0x1234)));
    EXPECT_CALL(mockLibvirt, connectionIsAlive(testing::_)).WillOnce(testing::Return(0));

    manager.initializeConnection("test:///testing");

    EXPECT_EQ(manager.isConnectionAlive(), false);
}

TEST_F(ConnectionManagerTests, isConnectionAlive_WhenConnectionIsAlive_ShouldReturnTrue)
{
    EXPECT_CALL(mockLibvirt, connectOpen(testing::StrEq("test:///testing"))).WillOnce(
        testing::Return(reinterpret_cast<virConnectPtr>(0x1234)));
    EXPECT_CALL(mockLibvirt, connectionIsAlive(testing::_)).WillOnce(testing::Return(1));

    manager.initializeConnection("test:///testing");

    EXPECT_EQ(manager.isConnectionAlive(), true);
}