#include "TestingUtils.h"
#include "mocks/LibvirtWrapperMock.h"
#include "virt/managers/VirtualNetworkManager.h"

class VirtualMachineManagerTests : public testing::Test
{
protected:
    LibvirtWrapperMock mockLibvirt;
    VirtualNetworkManager manager;

    VirtualMachineManagerTests()
        : manager(&mockLibvirt)
    {
    }
};

TEST_F(VirtualMachineManagerTests, createNetworkFromXml_WhenInvokedWithoutConnection_ShouldThrowException)
{
    TestingUtils::expectThrowWithMessage([this]
    {
        manager.createNetworkFromXml("<test/>");
    }, "No active connection to the VM backend");
}

TEST_F(VirtualMachineManagerTests, createNetworkFromXml_WhenInvoked_ShouldCreateVirtualNetwork)
{
    const auto networkPointer = reinterpret_cast<virNetworkPtr>(0x123);
    const auto connectionPointer = reinterpret_cast<virConnectPtr>(0x1234);

    manager.updateConnection(connectionPointer);

    EXPECT_CALL(mockLibvirt, createNetworkFromXml(connectionPointer, std::string("<test/>")))
        .WillOnce(testing::Return(networkPointer));

    const auto returnedPointer = manager.createNetworkFromXml("<test/>");

    EXPECT_EQ(networkPointer, returnedPointer);
}


