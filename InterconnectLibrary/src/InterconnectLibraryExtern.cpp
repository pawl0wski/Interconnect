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

char* VirtualMachineManager_CreateVirtualMachine(const char* virtualMachineXml);

char* VirtualMachineManager_GetInfoAboutVirtualMachine(const char* uuid);
}

#endif // INTERCONNECTLIBRARYEXTERN_H
