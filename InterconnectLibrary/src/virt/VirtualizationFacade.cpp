#include "VirtualizationFacade.h"

#include <algorithm>
#include <cstring>
#include <iostream>
#include <optional>

#include "../utils/StringUtils.h"

struct StreamInfo;

VirtualizationFacade::VirtualizationFacade(ILibvirtWrapper* libvirt)
{
    vmManager = new VirtualMachineManager(libvirt);
    vmConsoleManager = new VirtualMachineConsoleManager(libvirt);
    connManager = new ConnectionManager(libvirt);
}

VirtualizationFacade::VirtualizationFacade()
{
    static auto libvirt = LibvirtWrapper();
    vmManager = new VirtualMachineManager(&libvirt);
    vmConsoleManager = new VirtualMachineConsoleManager(&libvirt);
    connManager = new ConnectionManager(&libvirt);
}

void VirtualizationFacade::initializeConnection(const char* customConnectionUrl) const
{
    std::optional<std::string> connectionUrl = std::nullopt;

    if (customConnectionUrl != nullptr)
    {
        connectionUrl = std::make_optional(std::string(customConnectionUrl));
    }

    connManager->initializeConnection(connectionUrl);

    const auto conn = connManager->getConnection();
    vmManager->updateConnection(conn);
    vmConsoleManager->updateConnection(conn);
}

void VirtualizationFacade::getConnectionInfo(ConnectionInfo* infoPtr) const
{
    *infoPtr = connManager->getConnectionInfo();
}

void VirtualizationFacade::createVirtualMachine(const std::string& virtualMachineXml) const
{
    vmManager->createVirtualMachine(virtualMachineXml);
}

void VirtualizationFacade::getInfoAboutVirtualMachine(VirtualMachineInfo* virtualMachineInfo,
                                                      const std::string& name) const
{
    *virtualMachineInfo = vmManager->getInfoAboutVirtualMachine(name);
}

void VirtualizationFacade::getListOfVirtualMachinesWithInfo(VirtualMachineInfo** arrayOfVirtualMachines,
                                                            int* numberOfVirtualMachines) const
{
    static std::vector<VirtualMachineInfo> vectorOfVirtualMachines;
    vectorOfVirtualMachines = vmManager->
        getListOfVirtualMachinesWithInfo();
    *arrayOfVirtualMachines = vectorOfVirtualMachines.data();
    *numberOfVirtualMachines = static_cast<int>(vectorOfVirtualMachines.size());
}

void VirtualizationFacade::isConnectionAlive(bool* isAlive) const
{
    *isAlive = connManager->isConnectionAlive();
}

virStreamPtr VirtualizationFacade::openVirtualMachineConsole(const std::string& vmUuid) const
{
    return vmConsoleManager->openVirtualMachineConsole(vmUuid);
}

void VirtualizationFacade::receiveDataFromConsole(virStreamPtr stream, StreamData* streamData) const
{
    vmConsoleManager->getDataFromStream(stream, streamData);
}

void VirtualizationFacade::sendDataToConsole(virStreamPtr stream, const std::string& data) const
{
    vmConsoleManager->sendDataToStream(stream, data.c_str(), data.length());
}

void VirtualizationFacade::closeStream(virStreamPtr stream) const
{
    vmConsoleManager->closeStream(stream);
}
