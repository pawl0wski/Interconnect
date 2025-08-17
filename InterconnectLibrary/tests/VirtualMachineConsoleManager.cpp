#include "virt/managers/VirtualMachineConsoleManager.h"

#include <cstring>
#include <gtest/gtest.h>

#include "TestingUtils.h"
#include "mocks/LibvirtWrapperMock.h"
#include "mocks/VirtualMachineConsoleManagerMockedStreams.h"

class VirtualMachineConsoleManagerTests : public testing::Test
{
protected:
    LibvirtWrapperMock mockLibvirt;
    VirtualMachineConsoleManager manager;

    VirtualMachineConsoleManagerTests()
        : manager(&mockLibvirt)
    {
    }
};

TEST_F(VirtualMachineConsoleManagerTests, updateConnection_WhenCalled_ShouldUpdateConnection)
{
    std::string uuid = "Test";
    EXPECT_CALL(mockLibvirt, domainLookupByUuid(testing::_, testing::_)).WillOnce(
        testing::Return(reinterpret_cast<virDomainPtr>(0x123)));
    EXPECT_CALL(mockLibvirt, createNewStream(testing::_)).WillOnce(
        testing::Return(reinterpret_cast<virStreamPtr>(0x123)));
    EXPECT_CALL(mockLibvirt, openDomainConsole(testing::_, testing::_)).WillOnce(
        testing::Return(0));

    TestingUtils::expectThrowWithMessage([this, uuid]
    {
        manager.openVirtualMachineConsole(uuid);
    }, "No active connection to the VM backend");

    manager.updateConnection(reinterpret_cast<virConnectPtr>(0x123));
    manager.openVirtualMachineConsole(uuid);
}

TEST_F(VirtualMachineConsoleManagerTests, openVirtualMachineConsole_WhenCalled_ShouldCreateNewStreamAndOpenVmConsole)
{
    const std::string uuid = "Test";
    EXPECT_CALL(mockLibvirt, domainLookupByUuid(testing::_, testing::_)).WillOnce(
        testing::Return(reinterpret_cast<virDomainPtr>(0x123)));
    EXPECT_CALL(mockLibvirt, createNewStream(testing::_)).WillOnce(
        testing::Return(reinterpret_cast<virStreamPtr>(0x123)));
    EXPECT_CALL(mockLibvirt, openDomainConsole(testing::_, testing::_)).WillOnce(
        testing::Return(0));

    manager.updateConnection(reinterpret_cast<virConnectPtr>(0x123));
    const auto streamId = manager.openVirtualMachineConsole(uuid);

    EXPECT_EQ(streamId, 1);
}

TEST_F(VirtualMachineConsoleManagerTests, openVirtualMachineConsole_WhenCantOpenConsole_ShouldThrowException)
{
    const std::string uuid = "Test";
    EXPECT_CALL(mockLibvirt, domainLookupByUuid(testing::_, testing::_)).WillOnce(
        testing::Return(reinterpret_cast<virDomainPtr>(0x123)));
    EXPECT_CALL(mockLibvirt, createNewStream(testing::_)).WillOnce(
        testing::Return(reinterpret_cast<virStreamPtr>(0x123)));
    EXPECT_CALL(mockLibvirt, openDomainConsole(testing::_, testing::_)).WillOnce(
        testing::Return(1));

    manager.updateConnection(reinterpret_cast<virConnectPtr>(0x123));

    TestingUtils::expectThrowWithMessage([this, uuid]
    {
        manager.openVirtualMachineConsole(uuid);
    }, "Cannot open console");
}

TEST_F(VirtualMachineConsoleManagerTests, openVirtualMachineConsole_WhenCantFindDomain_ShouldThrowException)
{
    const std::string uuid = "Test";
    EXPECT_CALL(mockLibvirt, domainLookupByUuid(testing::_, testing::_)).WillOnce(
        testing::ReturnNull());

    manager.updateConnection(reinterpret_cast<virConnectPtr>(0x123));

    TestingUtils::expectThrowWithMessage([this, uuid]
    {
        manager.openVirtualMachineConsole(uuid);
    }, "Can not find domain");
}

TEST_F(VirtualMachineConsoleManagerTests, removeStream_WhenCalled_ShouldRemoveStream)
{
    auto mockConsoleManager = VirtualMachineConsoleManagerMockedStreams(&mockLibvirt);
    const auto expectedStream = reinterpret_cast<virStreamPtr>(0x123);

    EXPECT_CALL(mockConsoleManager, getStreamById(testing::Eq(1)))
        .WillOnce(testing::Return(expectedStream));

    mockConsoleManager.removeStream(1);
}

TEST_F(VirtualMachineConsoleManagerTests, removeStream_WhenTryToRemoveWrongStream_ShouldThrowException)
{
    TestingUtils::expectThrowWithMessage([this]
    {
        manager.removeStream(100);
    }, "vector::_M_range_check: __n (which is 100) >= this->size() (which is 0)");
}

TEST_F(VirtualMachineConsoleManagerTests, getDataFromStream_WhenCalled_ShouldGetDataFromStream)
{
    const auto expectedStream = reinterpret_cast<virStreamPtr>(0x123);
    char buff[255];
    VirtualMachineConsoleManagerMockedStreams mockedConsoleManager(&mockLibvirt);

    EXPECT_CALL(mockedConsoleManager, getStreamById(testing::Eq(1)))
        .WillOnce(testing::Return(expectedStream));
    EXPECT_CALL(mockLibvirt,
                receiveDataFromStream(testing::Eq(expectedStream), testing::_, testing::_))
        .WillOnce([](virStreamPtr stream, char* buffer, int bufferSize) -> int
        {
            const char* msg = "Test123";
            std::strncpy(buffer, msg, bufferSize);
            buffer[bufferSize - 1] = '\0';
            return static_cast<int>(std::strlen(msg));
        });

    mockedConsoleManager.getDataFromStream(1, buff, 255);

    EXPECT_STREQ("Test123", buff);
}

TEST_F(VirtualMachineConsoleManagerTests, sendDataToStream_WhenCalled_ShouldSendDataToStream)
{
    const auto expectedStream = reinterpret_cast<virStreamPtr>(0x123);
    char receivedBuffer[255];
    constexpr char buff[] = "Test";
    VirtualMachineConsoleManagerMockedStreams mockedConsoleManager(&mockLibvirt);

    EXPECT_CALL(mockedConsoleManager, getStreamById(testing::Eq(1)))
        .WillOnce(testing::Return(expectedStream));
    EXPECT_CALL(mockLibvirt, sendDataToStream(testing::Eq(expectedStream), testing::_, testing::_))
        .WillOnce([&receivedBuffer](virStreamPtr stream, const char* buffer, const int bufferSize)
        {
            std::strncpy(receivedBuffer, buffer, bufferSize);
        });

    mockedConsoleManager.sendDataToStream(1, buff, 255);

    EXPECT_STREQ("Test", receivedBuffer);
}

TEST_F(VirtualMachineConsoleManagerTests, getStreamById_WhenStreamNotExist_ShouldThrowException)
{
    TestingUtils::expectThrowWithMessage([this]
    {
        manager.sendDataToStream(100, nullptr, 0);
    }, "vector::_M_range_check: __n (which is 100) >= this->size() (which is 0)");
}
