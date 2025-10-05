#ifndef VIRTUALIZATIONLIBRARYEXTERN_H
#define VIRTUALIZATIONLIBRARYEXTERN_H

#include "models/ConnectionInfo.h"
#include "models/NetworkDefinition.h"
#include "models/StreamData.h"
#include "utils/ExecutionInfoObtainer.h"
#include "utils/StringUtils.h"
#include "virt/VirtualizationFacade.h"

extern "C" {
VirtualizationFacade* CreateVirtualizationFacade()
{
    return new VirtualizationFacade();
}

void DestroyVirtualizationFacade(const VirtualizationFacade* virtualization)
{
    delete virtualization;
}

void Virtualization_InitializeConnection(ExecutionInfo* executionInfo, VirtualizationFacade* virtualization,
                          const char* customConnectionUrl)
{
    ExecutionInfoObtainer::runAndObtainExecutionInfo(executionInfo, [virtualization, customConnectionUrl]
    {
        virtualization->initializeConnection(customConnectionUrl);
    });
}

void Virtualization_GetConnectionInfo(ExecutionInfo* executionInfo, VirtualizationFacade* virtualization,
                       ConnectionInfo* infoPtr)
{
    ExecutionInfoObtainer::runAndObtainExecutionInfo(executionInfo, [infoPtr, virtualization]
    {
        virtualization->getConnectionInfo(infoPtr);
    });
}

void Virtualization_CreateVirtualMachine(ExecutionInfo* executionInfo, VirtualizationFacade* virtualization,
                          const char* virtualMachineXml)
{
    ExecutionInfoObtainer::runAndObtainExecutionInfo(executionInfo, [virtualization, virtualMachineXml]
    {
        virtualization->createVirtualMachine(virtualMachineXml);
    });
}

void Virtualization_GetInfoAboutVirtualMachine(ExecutionInfo* executionInfo,
                                VirtualizationFacade* virtualization,
                                const char* name, VirtualMachineInfo* vmInfo)
{
    ExecutionInfoObtainer::runAndObtainExecutionInfo(executionInfo, [virtualization, name, vmInfo]
    {
        virtualization->getInfoAboutVirtualMachine(vmInfo, name);
    });
}

void Virtualization_GetListOfVirtualMachinesWithInfo(ExecutionInfo* executionInfo,
                                      VirtualizationFacade* virtualization,
                                      VirtualMachineInfo** arrayOfVms,
                                      int* numberOfVms)
{
    ExecutionInfoObtainer::runAndObtainExecutionInfo(executionInfo, [virtualization, arrayOfVms, numberOfVms]
    {
        virtualization->getListOfVirtualMachinesWithInfo(arrayOfVms, numberOfVms);
    });
}

void Virtualization_IsConnectionAlive(ExecutionInfo* executionInfo, VirtualizationFacade* virtualization,
                       bool* isAlive)
{
    ExecutionInfoObtainer::runAndObtainExecutionInfo(executionInfo, [virtualization, isAlive]
    {
        virtualization->isConnectionAlive(isAlive);
    });
}

void Virtualization_OpenVirtualMachineConsole(ExecutionInfo* executionInfo, VirtualizationFacade* virtualization, virStreamPtr* stream,
                               const char* vmUuid)
{
    ExecutionInfoObtainer::runAndObtainExecutionInfo(executionInfo, [virtualization, stream, vmUuid]
    {
        *stream = virtualization->openVirtualMachineConsole(vmUuid);
    });
}

void Virtualization_ReceiveDataFromConsole(ExecutionInfo* executionInfo, VirtualizationFacade* virtualization, virStreamPtr stream,
                            StreamData* streamData)
{
    ExecutionInfoObtainer::runAndObtainExecutionInfo(executionInfo, [virtualization, stream, streamData]
    {
        virtualization->receiveDataFromConsole(stream, streamData);
    });
}

void Virtualization_SendDataToConsole(ExecutionInfo* executionInfo, VirtualizationFacade* virtualization, virStreamPtr stream,
                       const char* data)
{
    ExecutionInfoObtainer::runAndObtainExecutionInfo(executionInfo, [virtualization, stream, data]
    {
        virtualization->sendDataToConsole(stream, data);
    });
}

void Virtualization_CloseStream(ExecutionInfo* executionInfo, VirtualizationFacade* virtualization, virStreamPtr stream)
{
    ExecutionInfoObtainer::runAndObtainExecutionInfo(executionInfo, [virtualization, stream]
    {
        virtualization->closeStream(stream);
    });
}

void Virtualization_CreateVirtualNetwork(ExecutionInfo* executionInfo, VirtualizationFacade* virtualization, virNetworkPtr* network,
                          const char* networkDefinition)
{
    ExecutionInfoObtainer::runAndObtainExecutionInfo(executionInfo, [virtualization, network, networkDefinition]
    {
        *network = virtualization->createVirtualNetworkFromXml(networkDefinition);
    });
}

void Virtualization_AttachDeviceToVirtualMachine(ExecutionInfo* executionInfo, VirtualizationFacade* virtualization, const char* uuid,
                                  const char* deviceDefinition)
{
    ExecutionInfoObtainer::runAndObtainExecutionInfo(executionInfo, [virtualization, uuid, deviceDefinition]
    {
        virtualization->attachDeviceToVm(uuid, deviceDefinition);
    });
}

void Virtualization_DetachDeviceFromVirtualMachine(ExecutionInfo* executionInfo, VirtualizationFacade* virtualization,
                                    const char* uuid, const char* deviceDefinition)
{
    ExecutionInfoObtainer::runAndObtainExecutionInfo(executionInfo, [virtualization, uuid, deviceDefinition]
    {
        virtualization->detachDeviceFromVm(uuid, deviceDefinition);
    });
}

void Virtualization_UpdateVmDevice(ExecutionInfo* executionInfo, VirtualizationFacade* virtualization, const char* uuid,
                    const char* deviceDefinition)
{
    ExecutionInfoObtainer::runAndObtainExecutionInfo(executionInfo, [virtualization, uuid, deviceDefinition]
    {
        virtualization->updateVmDevice(uuid, deviceDefinition);
    });
}

void Virtualization_DestroyNetwork(ExecutionInfo* executionInfo, VirtualizationFacade* virtualization, const char* name)
{
    ExecutionInfoObtainer::runAndObtainExecutionInfo(executionInfo, [virtualization, name]
    {
        virtualization->destroyNetwork(name);
    });
}

void Virtualization_GetNetworkDefinition(ExecutionInfo* executionInfo, VirtualizationFacade* virtualization, const char* name,
                          NetworkDefinition* definition)
{
    ExecutionInfoObtainer::runAndObtainExecutionInfo(executionInfo, [virtualization, name, definition]
    {
        const auto networkDefinition = virtualization->getNetworkDefinition(name);
        StringUtils::copyStringToCharArray(networkDefinition, definition->content, 4096);
    });
}
}

#endif // VIRTUALIZATIONLIBRARYEXTERN_H
