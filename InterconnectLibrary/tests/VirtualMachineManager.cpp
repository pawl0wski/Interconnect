#include <gtest/gtest.h>
#include "VirtualMachineManager.h"

#include "exceptions/ConnectionToVMBackendFailed.h"
#include "exceptions/CreateVirtualMachineFailed.h"
#include "exceptions/NoActiveVMBackendConnection.h"
#include "mocks/LibvirtWrapperMock.h"
#include "mocks/VirtualMachineManagerMockGetInfoAboutVirtualMachine.h"

class VirtualMachineManagerTest : public ::testing::Test {
protected:
    LibvirtWrapperMock mockLibvirt;
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

    EXPECT_THROW(manager.initializeConnection(), ConnectionToVMBackendFailed);
}

TEST_F(VirtualMachineManagerTest, createVirtualMachine_WhenNoBackendConnection_ShouldThrowNoActiveVMBackendConnection) {
    EXPECT_THROW(manager.createVirtualMachine("<xml>test</xml>"), NoActiveVMBackendConnection);
}


TEST_F(VirtualMachineManagerTest,
       createVirtualMachine_WhenXMLProvidedButErrorInLibvirt_ShouldThrowCreateVirtualMachineFailed) {
    EXPECT_CALL(mockLibvirt, connectOpen(testing::_))
            .WillOnce(testing::Return(reinterpret_cast<virConnectPtr>(0x1234)));
    EXPECT_CALL(mockLibvirt, createVirtualMachineFromXml(testing::_, testing::_))
            .WillOnce(testing::Return(nullptr));

    manager.initializeConnection();
    EXPECT_THROW(manager.createVirtualMachine("<xml>test</xml>"), CreateVirtualMachineFailed);
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
