#ifndef INTERCONNECTLIBRARYEXTERN_H
#define INTERCONNECTLIBRARYEXTERN_H

#include "VirtualMachineManager.h"
#include "utils/ExecutionInfoObtainer.h"

ConnectionInfo info;

extern "C" {
VirtualMachineManager* VirtualMachineManager_Create()
{
    return new VirtualMachineManager();
}

void VirtualMachineManager_Destroy(const VirtualMachineManager* manager)
{
    delete manager;
}

void VirtualMachineManager_InitializeConnection(ExecutionInfo* executionInfo, VirtualMachineManager* manager,
                                                const char* customConnectionUrl)
{
    ExecutionInfoObtainer::runAndObtainExecutionInfo(executionInfo, [manager, customConnectionUrl]
    {
        std::optional<std::string> connectionUrl = std::nullopt;

        if (customConnectionUrl != nullptr)
        {
            connectionUrl = std::make_optional(std::string(customConnectionUrl));
        }

        manager->initializeConnection(connectionUrl);
    });
}

void VirtualMachineManager_GetConnectionInfo(ExecutionInfo* executionInfo, VirtualMachineManager* manager,
                                             ConnectionInfo* infoPtr)
{
    ExecutionInfoObtainer::runAndObtainExecutionInfo(executionInfo, [infoPtr, manager]
    {
        *infoPtr = manager->getConnectionInfo();
    });
}

void VirtualMachineManager_CreateVirtualMachine(ExecutionInfo* executionInfo, VirtualMachineManager* manager,
                                                const char* virtualMachineXml)
{
    ExecutionInfoObtainer::runAndObtainExecutionInfo(executionInfo, [manager, virtualMachineXml]
    {
        manager->createVirtualMachine(virtualMachineXml);
    });
}

void VirtualMachineManager_GetInfoAboutVirtualMachine(ExecutionInfo* executionInfo, VirtualMachineManager* manager,
                                                      const char* name, VirtualMachineInfo* vmInfo)
{
    ExecutionInfoObtainer::runAndObtainExecutionInfo(executionInfo, [manager, name, vmInfo]
    {
        *vmInfo = manager->getInfoAboutVirtualMachine(name);
    });
}

void VirtualMachineManager_GetListOfVirtualMachinesWithInfo(ExecutionInfo* executionInfo,
                                                            VirtualMachineManager* manager,
                                                            VirtualMachineInfo** arrayOfVirtualMachines,
                                                            int* numberOfVirtualMachines)
{
    ExecutionInfoObtainer::runAndObtainExecutionInfo(executionInfo,
                                                     [manager, arrayOfVirtualMachines, numberOfVirtualMachines]
                                                     {
                                                         static auto vectorOfVirtualMachines = manager->
                                                             getListOfVirtualMachinesWithInfo();
                                                         *arrayOfVirtualMachines = vectorOfVirtualMachines.data();
                                                         *numberOfVirtualMachines = static_cast<int>(
                                                             vectorOfVirtualMachines.size());
                                                     });
}

void VirtualMachineManager_IsConnectionAlive(ExecutionInfo* executionInfo, VirtualMachineManager* manager,
                                             bool* isAlive)
{
    ExecutionInfoObtainer::runAndObtainExecutionInfo(executionInfo, [manager, isAlive]
    {
        *isAlive = manager->isConnectionAlive();
    });
}
}

#endif // INTERCONNECTLIBRARYEXTERN_H
