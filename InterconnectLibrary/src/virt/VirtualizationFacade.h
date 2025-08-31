#ifndef VIRTUALIZATIONFACADE_H
#define VIRTUALIZATIONFACADE_H
#include "managers/ConnectionManager.h"
#include "managers/VirtualMachineConsoleManager.h"
#include "managers/VirtualMachineManager.h"
#include "../models/StreamData.h"
#include "managers/VirtualNetworkManager.h"

class VirtualizationFacade
{
    ConnectionManager* connManager = nullptr;
    VirtualMachineManager* vmManager = nullptr;
    VirtualMachineConsoleManager* vmConsoleManager = nullptr;
    VirtualNetworkManager* networkManager = nullptr;

public:
    virtual ~VirtualizationFacade() = default;

    /**
     * @param libvirt Reference to an ILibvirtWrapper implementation.
     */
    explicit VirtualizationFacade(ILibvirtWrapper* libvirt);

    explicit VirtualizationFacade();

    void initializeConnection(const char* customConnectionUrl) const;

    void getConnectionInfo(ConnectionInfo* infoPtr) const;

    void createVirtualMachine(const std::string& virtualMachineXml) const;

    void getInfoAboutVirtualMachine(VirtualMachineInfo* virtualMachineInfo, const std::string& name) const;

    void getListOfVirtualMachinesWithInfo(VirtualMachineInfo** arrayOfVirtualMachines,
                                          int* numberOfVirtualMachines) const;

    void isConnectionAlive(bool* isAlive) const;

    [[nodiscard]] virStreamPtr openVirtualMachineConsole(const std::string& vmUuid) const;

    void receiveDataFromConsole(virStreamPtr stream, StreamData* streamData) const;

    void sendDataToConsole(virStreamPtr stream, const std::string& data) const;

    void closeStream(virStreamPtr stream) const;

    [[nodiscard]] virNetworkPtr createVirtualNetworkFromXml(const std::string& networkDefinition) const;

    void attachDeviceToVm(const std::string& uuid, const std::string& deviceDefinition) const;
};

#endif //VIRTUALIZATIONFACADE_H
