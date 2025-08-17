#include "VirtualizationFacade.h"

#include <optional>

VirtualizationFacade::VirtualizationFacade(ILibvirtWrapper* libvirt)
{
    vmManager = new VirtualMachineManager(libvirt);
    vmConsoleManager = new VirtualMachineConsoleManager(libvirt);
    connManager = new ConnectionManager(libvirt);
}

VirtualizationFacade::VirtualizationFacade()
{
    const auto libvirt = new LibvirtWrapper();
    vmManager = new VirtualMachineManager(libvirt);
    vmConsoleManager = new VirtualMachineConsoleManager(libvirt);
    connManager = new ConnectionManager(libvirt);
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
    vmConsoleManager->updateConnection(conn);
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
    *numberOfVirtualMachines = static_cast<int>(
        vectorOfVirtualMachines.size());
}

void VirtualizationFacade::isConnectionAlive(bool* isAlive) const
{
    *isAlive = connManager->isConnectionAlive();
}

// void VirtualizationFacade::openVirtualMachineConsole(const char* vmUuid) const
// {
//     auto vmUuidStr = std::string(vmUuid);
//     const auto stream = vmManager->openVirtualMachineConsole(vmUuidStr);
//     vmConsoleManager->addNewStream(stream);
// }
