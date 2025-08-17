#ifndef VIRTUALIZATIONFACADE_H
#define VIRTUALIZATIONFACADE_H
#include "managers/ConnectionManager.h"
#include "managers/VirtualMachineConsoleManager.h"
#include "managers/VirtualMachineManager.h"

class VirtualizationFacade
{
    ConnectionManager* connManager = nullptr;
    VirtualMachineManager* vmManager = nullptr;
    VirtualMachineConsoleManager* vmConsoleManager = nullptr;

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

    // void openVirtualMachineConsole(const char* vmUuid) const;
};

#endif //VIRTUALIZATIONFACADE_H
