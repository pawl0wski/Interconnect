#include <gtest/gtest.h>
#include "VirtualMachineManager.h"
#include "exceptions/VirtualMachineManagerException.h"

#include "mocks/LibvirtWrapperMockTests.h"
#include "mocks/VirtualMachineManagerMockGetInfoAboutVirtualMachine.h"

class VirtualMachineManagerTest : public testing::Test {
protected:
    LibvirtWrapperMockTests mockLibvirt;
    VirtualMachineManager manager;

    VirtualMachineManagerTest()
        : manager(&mockLibvirt) {
    }
};

TEST_F(VirtualMachineManagerTest, initializeConnection_WhenConnectionPathIsNotSet_ShouldConnectToDefaultVMBackend) {
    EXPECT_CALL(mockLibvirt, connectOpen(testing::StrEq("qemu:///system")))
            .WillOnce(testing::Return(reinterpret_cast<virConnectPtr>(0x1234)));

    EXPECT_NO_THROW(manager.initializeConnection());
}

TEST_F(VirtualMachineManagerTest,
       initializeConnection_WhenCustomConnectionPathProvided_ShouldConnectToCustomVMBackend) {
    EXPECT_CALL(mockLibvirt, connectOpen(testing::StrEq("test:///testing"))).WillOnce(
        testing::Return(reinterpret_cast<virConnectPtr>(0x1234)));

    EXPECT_NO_THROW(manager.initializeConnection("test:///testing"));
}

TEST_F(VirtualMachineManagerTest, initializeConnection_WhenConnectionFails_ShouldThrowConnectionToVMBackendFailed) {
    EXPECT_CALL(mockLibvirt, connectOpen(testing::StrEq("qemu:///system"))).WillOnce(
        testing::Return(reinterpret_cast<virConnectPtr>(0x0)));

    EXPECT_THROW(manager.initializeConnection(), VirtualMachineManagerException);
}

TEST_F(VirtualMachineManagerTest, createVirtualMachine_WhenNoBackendConnection_ShouldThrowNoActiveVMBackendConnection) {
    EXPECT_THROW(manager.createVirtualMachine("<xml>test</xml>"), VirtualMachineManagerException);
}


TEST_F(VirtualMachineManagerTest,
       createVirtualMachine_WhenXMLProvidedButErrorInLibvirt_ShouldThrowCreateVirtualMachineFailed) {
    EXPECT_CALL(mockLibvirt, connectOpen(testing::_))
            .WillOnce(testing::Return(reinterpret_cast<virConnectPtr>(0x1234)));
    EXPECT_CALL(mockLibvirt, createVirtualMachineFromXml(testing::_, testing::_))
            .WillOnce(testing::Return(nullptr));

    manager.initializeConnection();
    EXPECT_THROW(manager.createVirtualMachine("<xml>test</xml>"), VirtualMachineManagerException);
}

TEST_F(VirtualMachineManagerTest, createVirtualMachine_WhenVirtualMachineCreated_ShouldCallForVirtualMachineInfo) {
    VirtualMachineManagerMockGetInfoAboutVirtualMachine customManager(&mockLibvirt);

    EXPECT_CALL(mockLibvirt, connectOpen(testing::_))
            .WillOnce(testing::Return(reinterpret_cast<virConnectPtr>(0x1234)));
    EXPECT_CALL(mockLibvirt, createVirtualMachineFromXml(testing::_, testing::_))
            .WillOnce(testing::Return(reinterpret_cast<virDomainPtr>(0x1234)));
    EXPECT_CALL(mockLibvirt, getUuidFromDomain(testing::_, testing::_)).WillOnce([&](virDomainPtr, char *uuid) {
        strcpy(uuid, "034e885e-780c-4e14-b83c-fab5f1f33b86");
    });
    EXPECT_CALL(customManager,
                getInfoAboutVirtualMachine(testing::StrEq("034e885e-780c-4e14-b83c-fab5f1f33b86"))).Times(1).WillOnce(
        testing::Return(VirtualMachineInfo{"034e885e-780c-4e14-b83c-fab5f1f33b86"}));

    customManager.initializeConnection();
    const auto [uuid] = customManager.createVirtualMachine("<xml>test</xml>");
    EXPECT_STREQ(uuid.c_str(), "034e885e-780c-4e14-b83c-fab5f1f33b86");
}

TEST_F(VirtualMachineManagerTest, getConnectionInfo_WhenInvoked_ShouldReturnConnectionInfo) {
    virNodeInfo mockInfo{
        .memory = 500,
        .cpus = 10,
        .mhz = 1000,
    };
    EXPECT_CALL(mockLibvirt, getDriverType(testing::_)).WillOnce(testing::Return("QEMU"));
    EXPECT_CALL(mockLibvirt, getConnectUrl(testing::_)).WillOnce(testing::Return("test:///testing"));
    EXPECT_CALL(mockLibvirt, getNodeInfo(testing::_, testing::_)).WillOnce(
        [&](virConnectPtr, virNodeInfoPtr info) {
            *info = mockInfo;
            return 0;
        });
    EXPECT_CALL(mockLibvirt, getLibVersion(testing::_, testing::_)).WillOnce(
        [&](virConnectPtr, unsigned long *version) {
            *version = 3014159;
            return 0;
        });
    EXPECT_CALL(mockLibvirt, getDriverVersion(testing::_, testing::_)).WillOnce(
        [&](virConnectPtr, unsigned long *version) {
            *version = 5016152;
            return 0;
        });

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

TEST_F(VirtualMachineManagerTest, getConnectionInfo_WhenGetNodeInfoFails_ShouldThrow) {
    EXPECT_CALL(mockLibvirt, getDriverType(testing::_)).WillOnce(testing::Return("QEMU"));
    EXPECT_CALL(mockLibvirt, getConnectUrl(testing::_)).WillOnce(testing::Return("test:///testing"));
    EXPECT_CALL(mockLibvirt, getNodeInfo(testing::_, testing::_)).WillOnce(testing::Return(1)); // simulate failure

    EXPECT_THROW({
                 manager.getConnectionInfo();
                 }, VirtualMachineManagerException);
}

TEST_F(VirtualMachineManagerTest, getConnectionInfo_WhenGetLibVersionFails_ShouldThrow) {
    virNodeInfo mockInfo{
        .memory = 500,
        .cpus = 10,
        .mhz = 1000,
    };

    EXPECT_CALL(mockLibvirt, getDriverType(testing::_)).WillOnce(testing::Return("QEMU"));
    EXPECT_CALL(mockLibvirt, getConnectUrl(testing::_)).WillOnce(testing::Return("test:///testing"));
    EXPECT_CALL(mockLibvirt, getNodeInfo(testing::_, testing::_)).WillOnce(
        [&](virConnectPtr, virNodeInfoPtr info) {
            *info = mockInfo;
            return 0;
        });
    EXPECT_CALL(mockLibvirt, getLibVersion(testing::_, testing::_)).WillOnce(testing::Return(1)); // simulate failure

    EXPECT_THROW({
                 manager.getConnectionInfo();
                 }, VirtualMachineManagerException);
}

TEST_F(VirtualMachineManagerTest, getConnectionInfo_WhenGetDriverVersionFails_ShouldThrow) {
    virNodeInfo mockInfo{
        .memory = 500,
        .cpus = 10,
        .mhz = 1000,
    };

    EXPECT_CALL(mockLibvirt, getDriverType(testing::_)).WillOnce(testing::Return("QEMU"));
    EXPECT_CALL(mockLibvirt, getConnectUrl(testing::_)).WillOnce(testing::Return("test:///testing"));
    EXPECT_CALL(mockLibvirt, getNodeInfo(testing::_, testing::_)).WillOnce(
        [&](virConnectPtr, virNodeInfoPtr info) {
            *info = mockInfo;
            return 0;
        });
    EXPECT_CALL(mockLibvirt, getLibVersion(testing::_, testing::_)).WillOnce(
        [&](virConnectPtr, unsigned long *version) {
            *version = 3014159;
            return 0;
        });
    EXPECT_CALL(mockLibvirt, getDriverVersion(testing::_, testing::_)).WillOnce(testing::Return(1)); // simulate failure

    EXPECT_THROW({
                 manager.getConnectionInfo();
                 }, VirtualMachineManagerException);
}
