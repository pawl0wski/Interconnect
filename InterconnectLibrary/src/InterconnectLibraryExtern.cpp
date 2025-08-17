#ifndef INTERCONNECTLIBRARYEXTERN_H
#define INTERCONNECTLIBRARYEXTERN_H

#include "models/ConnectionInfo.h"
#include "utils/ExecutionInfoObtainer.h"
#include "virt/VirtualizationFacade.h"

extern "C" {
VirtualizationFacade* VirtualizationFacade_Create()
{
    return new VirtualizationFacade();
}

void VirtualizationFacade_Destroy(const VirtualizationFacade* virtualization)
{
    delete virtualization;
}

void VirtualMachineManager_InitializeConnection(ExecutionInfo* executionInfo, VirtualizationFacade* virtualization,
                                                const char* customConnectionUrl)
{
    ExecutionInfoObtainer::runAndObtainExecutionInfo(executionInfo, [virtualization, customConnectionUrl]
    {
        virtualization->initializeConnection(customConnectionUrl);
    });
}

void VirtualMachineManager_GetConnectionInfo(ExecutionInfo* executionInfo, VirtualizationFacade* virtualization,
                                             ConnectionInfo* infoPtr)
{
    ExecutionInfoObtainer::runAndObtainExecutionInfo(executionInfo, [infoPtr, virtualization]
    {
        virtualization->getConnectionInfo(infoPtr);
    });
}

void VirtualMachineManager_CreateVirtualMachine(ExecutionInfo* executionInfo, VirtualizationFacade* virtualization,
                                                const char* virtualMachineXml)
{
    ExecutionInfoObtainer::runAndObtainExecutionInfo(executionInfo, [virtualization, virtualMachineXml]
    {
        virtualization->createVirtualMachine(virtualMachineXml);
    });
}

void VirtualMachineManager_GetInfoAboutVirtualMachine(ExecutionInfo* executionInfo,
                                                      VirtualizationFacade* virtualization,
                                                      const char* name, VirtualMachineInfo* vmInfo)
{
    ExecutionInfoObtainer::runAndObtainExecutionInfo(executionInfo, [virtualization, name, vmInfo]
    {
        virtualization->getInfoAboutVirtualMachine(vmInfo, name);
    });
}

void VirtualMachineManager_GetListOfVirtualMachinesWithInfo(ExecutionInfo* executionInfo,
                                                            VirtualizationFacade* virtualization,
                                                            VirtualMachineInfo** arrayOfVms,
                                                            int* numberOfVms)
{
    ExecutionInfoObtainer::runAndObtainExecutionInfo(executionInfo, [virtualization, arrayOfVms, numberOfVms]
    {
        virtualization->getListOfVirtualMachinesWithInfo(arrayOfVms, numberOfVms);
    });
}

void VirtualMachineManager_IsConnectionAlive(ExecutionInfo* executionInfo, VirtualizationFacade* virtualization,
                                             bool* isAlive)
{
    ExecutionInfoObtainer::runAndObtainExecutionInfo(executionInfo, [virtualization, isAlive]
    {
        virtualization->isConnectionAlive(isAlive);
    });
}

// void VirtualMachineManager_OpenVirtualMachineConsole(ExecutionInfo* executionInfo, VirtualizationFacade* virtualization,
//                                                      const char* vmUuid)
// {
//     ExecutionInfoObtainer::runAndObtainExecutionInfo(executionInfo, [virtualization, vmUuid]
//     {
//         virtualization->openVirtualMachineConsole(vmUuid);
//     });
// }
}

#endif // INTERCONNECTLIBRARYEXTERN_H
