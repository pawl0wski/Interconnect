#include <gtest/gtest.h>
#include "VirtualMachineManager.h"

#include "exceptions/ConnectionToVMBackendFailed.h"
#include "mocks/LibvirtWrapperMock.h"

class VirtualMachineManagerTest : public ::testing::Test {
protected:
    LibvirtWrapperMock mockLibvirt;
    VirtualMachineManager manager;

    VirtualMachineManagerTest()
        : manager(mockLibvirt) {
    }
};

TEST_F(VirtualMachineManagerTest, ShouldConnectToDefaultVMBackendWhenConnectionPathIsNotSet) {
    EXPECT_CALL(mockLibvirt, connectOpen( testing::StrEq("qemu:///system")))
            .WillOnce(testing::Return(reinterpret_cast<virConnectPtr>(0x1234)));

    EXPECT_NO_THROW(manager.initializeConnection());
}

TEST_F(VirtualMachineManagerTest, ShouldConnectToCustomVMBackendWhenConnectionPathIsProvided) {
    EXPECT_CALL(mockLibvirt, connectOpen(testing::StrEq("test:///testing"))).WillOnce(
        testing::Return(reinterpret_cast<virConnectPtr>(0x1234)));

    EXPECT_NO_THROW(manager.initializeConnection("test:///testing"));
}

TEST_F(VirtualMachineManagerTest, ShouldThrowExceptionIfThereWasErrorConnectingToVMBackend) {
    EXPECT_CALL(mockLibvirt, connectOpen(testing::StrEq("qemu:///system"))).WillOnce(
        testing::Return(reinterpret_cast<virConnectPtr>(0x0)));

    EXPECT_THROW(manager.initializeConnection(), ConnectionToVMBackendFailed);
}
