#include <format>
#include <gtest/gtest.h>

#include "TestingUtils.h"
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

TEST_F(VirtualMachineManagerTests, updateConnection_WhenCalled_ShouldUpdateConnection)
{
    EXPECT_CALL(mockLibvirt, createVirtualMachineFromXml(testing::_, testing::_))
        .WillOnce(testing::Return(reinterpret_cast<virDomainPtr>(0x1234)));

    TestingUtils::expectThrowWithMessage(
        [this]
        {
            manager.createVirtualMachine("<xml>test</xml>");
        },
        "No active connection to the VM backend"
    );

    manager.updateConnection(reinterpret_cast<virConnectPtr>(0x123));

    EXPECT_NO_THROW(manager.createVirtualMachine("<xml>test</xml>"));
}

TEST_F(VirtualMachineManagerTests, createVirtualMachine_WhenNoBackendConnection_ShouldThrowNoActiveVMBackendConnection)
{
    TestingUtils::expectThrowWithMessage([this]
    {
        manager.createVirtualMachine("<xml>test</xml>");
    }, "No active connection to the VM backend");
}


TEST_F(VirtualMachineManagerTests,
       createVirtualMachine_WhenXMLProvidedButErrorInLibvirt_ShouldThrowCreateVirtualMachineFailed)
{
    EXPECT_CALL(mockLibvirt, createVirtualMachineFromXml(testing::_, testing::_))
        .WillOnce(testing::Return(nullptr));
    EXPECT_CALL(mockLibvirt, getLastError()).WillOnce(testing::Throw(std::runtime_error("MockLibVirtException")));

    manager.updateConnection(reinterpret_cast<virConnectPtr>(0x123));
    TestingUtils::expectThrowWithMessage(
        [this]
        {
            manager.createVirtualMachine("<xml>test</xml>");
        }, "MockLibVirtException");
}

TEST_F(VirtualMachineManagerTests, createVirtualMachine_WhenVirtualMachineCreated_ShouldCallForVirtualMachineInfo)
{
    VirtualMachineManagerMockGetInfoAboutVirtualMachine customManager(&mockLibvirt);

    EXPECT_CALL(mockLibvirt, createVirtualMachineFromXml(testing::_, testing::_))
        .WillOnce(testing::Return(reinterpret_cast<virDomainPtr>(0x1234)));

    customManager.updateConnection(reinterpret_cast<virConnectPtr>(0x123));
    customManager.createVirtualMachine("<xml>test</xml>");
}

TEST_F(VirtualMachineManagerTests, getInfoAboutVirtualMachine_WhenVmNotExist_ShouldThrowException)
{
    EXPECT_CALL(mockLibvirt, domainLookupByName(testing::_, testing::_)).WillOnce(testing::Return(nullptr));

    manager.updateConnection(reinterpret_cast<virConnectPtr>(0x123));
    TestingUtils::expectThrowWithMessage([this]
    {
        manager.getInfoAboutVirtualMachine("test");
    }, "Error while obtaining pointer to virtual machine");
}

TEST_F(VirtualMachineManagerTests, getInfoAboutVirtualMachine_WhenVmExistButCantGetPointerToVmInfo_ShouldThrowException)
{
    EXPECT_CALL(mockLibvirt, domainLookupByName(testing::_, testing::_)).WillOnce(
        testing::Return(reinterpret_cast<virDomainPtr>(1)));
    EXPECT_CALL(mockLibvirt, domainGetInfo(testing::_, testing::_)).WillOnce(testing::Return(1));

    manager.updateConnection(reinterpret_cast<virConnectPtr>(0x123));
    TestingUtils::expectThrowWithMessage([this]
    {
        manager.getInfoAboutVirtualMachine("test");
    }, "Virtual machine was found but error occurred while obtaining its info");
}

TEST_F(VirtualMachineManagerTests, getInfoAboutVirtualMachine_WhenVmExistButCantGetUuid_ShouldThrowException)
{
    EXPECT_CALL(mockLibvirt, domainLookupByName(testing::_, testing::_)).WillOnce(
        testing::Return(reinterpret_cast<virDomainPtr>(1)));
    EXPECT_CALL(mockLibvirt, domainGetInfo(testing::_, testing::_)).WillOnce(testing::Return(0));
    EXPECT_CALL(mockLibvirt, getDomainUUID(testing::_, testing::_)).WillOnce(testing::Return(1));

    manager.updateConnection(reinterpret_cast<virConnectPtr>(0x123));
    TestingUtils::expectThrowWithMessage([this]
    {
        manager.getInfoAboutVirtualMachine("test");
    }, "Virtual machine was found but error occurred while obtaining its uuid");
}

TEST_F(VirtualMachineManagerTests, getInfoAboutVirtualMachine_WhenVmExistAndSuccessfulyObtainedInfo_ShouldReturnInfo)
{
    EXPECT_CALL(mockLibvirt, domainLookupByName(testing::_, testing::_)).WillOnce(
        testing::Return(reinterpret_cast<virDomainPtr>(1)));
    EXPECT_CALL(mockLibvirt, domainGetInfo(testing::_, testing::_)).WillOnce(testing::Invoke(
        [](virDomainPtr _, virDomainInfo& domainInfo)
        {
            domainInfo.memory = 454;
            domainInfo.state = 5;
            return 0;
        }));
    EXPECT_CALL(mockLibvirt, getDomainUUID(testing::_, testing::_)).WillOnce(testing::Invoke(
        [](virDomainPtr _, std::string& uuid)
        {
            uuid = "testUuid";
            return 0;
        }));

    manager.updateConnection(reinterpret_cast<virConnectPtr>(0x123));
    auto info = manager.getInfoAboutVirtualMachine("test");

    EXPECT_STREQ(info.uuid, "testUuid");
    EXPECT_STREQ(info.name, "test");
    EXPECT_EQ(info.state, 5);
    EXPECT_EQ(info.usedMemory, 454);
}

TEST_F(VirtualMachineManagerTests,
       getListOfVirtualMachinesWithInfo_WhenErrorWhileRetrievingListOfVirtualMachines_ShouldThrowException)
{
    EXPECT_CALL(mockLibvirt, getListOfAllDomains(testing::_, testing::_)).WillOnce(
        testing::Invoke([](virConnectPtr _, virDomainPtr** domains)
        {
            domains = nullptr;

            return -1;
        }));

    manager.updateConnection(reinterpret_cast<virConnectPtr>(0x123));
    TestingUtils::expectThrowWithMessage([this]
    {
        manager.getListOfVirtualMachinesWithInfo();
    }, "Error retrieving the list of virtual machines.");
}

TEST_F(VirtualMachineManagerTests,
       getListOfVirtualMachinesWithInfo_WhenRecievedListOfVirtualMachines_ShouldReturnListOfVirtualMachines)
{
    VirtualMachineManagerMockGetInfoAboutVirtualMachine mockManager(&mockLibvirt);

    EXPECT_CALL(mockLibvirt, getListOfAllDomains(testing::_, testing::_)).WillOnce(
        testing::Invoke([](virConnectPtr, virDomainPtr** domains)
        {
            auto* domainArray = new virDomainPtr[2];
            domainArray[0] = reinterpret_cast<virDomainPtr>(0x1);
            domainArray[1] = reinterpret_cast<virDomainPtr>(0x2);

            *domains = domainArray;
            return 2;
        })
    );
    EXPECT_CALL(mockLibvirt, getDomainName(testing::_)).WillRepeatedly(testing::Invoke([](virDomainPtr)
    {
        static int vmNumber = 1;
        return std::format("test{}", vmNumber++);
    }));
    EXPECT_CALL(mockManager, getInfoAboutVirtualMachine(testing::_)).WillRepeatedly(testing::Invoke([](std::string name)
    {
        auto vmInfo = VirtualMachineInfo{
            .uuid = {},
            .usedMemory = 123,
            .state = 1
        };
        strcpy(vmInfo.uuid, name.c_str());

        return vmInfo;
    }));
    EXPECT_CALL(mockLibvirt, freeDomain(testing::_)).WillRepeatedly(testing::Return());

    mockManager.updateConnection(reinterpret_cast<virConnectPtr>(0x123));
    const auto vms = mockManager.getListOfVirtualMachinesWithInfo();

    EXPECT_EQ(vms.size(), 2);
    EXPECT_STREQ(vms.at(0).uuid, "test1");
    EXPECT_STREQ(vms.at(1).uuid, "test2");
}

TEST_F(VirtualMachineManagerTests, attachDeviceToVirtualMachine_WhenConnectionIsNotSet_ShouldThrowException)
{
    TestingUtils::expectThrowWithMessage([this]
    {
        manager.attachDeviceToVirtualMachine("test", "test");
    }, "No active connection to the VM backend");
}

TEST_F(VirtualMachineManagerTests, attachDeviceToVirtualMachine_WhenCannotFindDevice_ShouldThrowException)
{
    const auto mockConn = reinterpret_cast<virConnectPtr>(0x123);
    EXPECT_CALL(mockLibvirt, domainLookupByName(mockConn, "test")).WillOnce(testing::ReturnNull());

    manager.updateConnection(mockConn);
    TestingUtils::expectThrowWithMessage([this]
    {
        manager.attachDeviceToVirtualMachine("test", "test");
    }, "Error while obtaining pointer to virtual machine");
}

TEST_F(VirtualMachineManagerTests, attachDeviceToVirtualMachine_WhenCannotAttachDeviceToVm_ShouldThrowException)
{
    const auto mockConn = reinterpret_cast<virConnectPtr>(0x123);
    const auto mockDomain = reinterpret_cast<virDomainPtr>(0x456);
    EXPECT_CALL(mockLibvirt, domainLookupByName(mockConn, "test")).WillOnce(testing::Return(mockDomain));
    EXPECT_CALL(mockLibvirt, attachDeviceToVm(mockDomain, "test")).WillOnce(testing::Return(-1));

    manager.updateConnection(mockConn);
    TestingUtils::expectThrowWithMessage([this]
    {
        manager.attachDeviceToVirtualMachine("test", "test");
    }, "Can't attach device");
}

TEST_F(VirtualMachineManagerTests, attachDeviceToVirtualMachine_WhenInvoked_ShouldAttachDeviceToVm)
{
    const auto mockConn = reinterpret_cast<virConnectPtr>(0x123);
    const auto mockDomain = reinterpret_cast<virDomainPtr>(0x456);
    EXPECT_CALL(mockLibvirt, domainLookupByName(mockConn, "test")).WillOnce(testing::Return(mockDomain));
    EXPECT_CALL(mockLibvirt, attachDeviceToVm(mockDomain, "test")).WillOnce(testing::Return(0));

    manager.updateConnection(mockConn);
    manager.attachDeviceToVirtualMachine("test", "test");
}
