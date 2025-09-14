#ifndef INTERCONNECTLIBRARYEXTERN_H
#define INTERCONNECTLIBRARYEXTERN_H

#include <iostream>

#include "models/ConnectionInfo.h"
#include "models/StreamData.h"
#include "utils/ExecutionInfoObtainer.h"
#include "virt/VirtualizationFacade.h"

extern "C" {
VirtualizationFacade* Create()
{
    return new VirtualizationFacade();
}

void Destroy(const VirtualizationFacade* virtualization)
{
    delete virtualization;
}

void InitializeConnection(ExecutionInfo* executionInfo, VirtualizationFacade* virtualization,
                          const char* customConnectionUrl)
{
    ExecutionInfoObtainer::runAndObtainExecutionInfo(executionInfo, [virtualization, customConnectionUrl]
    {
        virtualization->initializeConnection(customConnectionUrl);
    });
}

void GetConnectionInfo(ExecutionInfo* executionInfo, VirtualizationFacade* virtualization,
                       ConnectionInfo* infoPtr)
{
    ExecutionInfoObtainer::runAndObtainExecutionInfo(executionInfo, [infoPtr, virtualization]
    {
        virtualization->getConnectionInfo(infoPtr);
    });
}

void CreateVirtualMachine(ExecutionInfo* executionInfo, VirtualizationFacade* virtualization,
                          const char* virtualMachineXml)
{
    ExecutionInfoObtainer::runAndObtainExecutionInfo(executionInfo, [virtualization, virtualMachineXml]
    {
        virtualization->createVirtualMachine(virtualMachineXml);
    });
}

void GetInfoAboutVirtualMachine(ExecutionInfo* executionInfo,
                                VirtualizationFacade* virtualization,
                                const char* name, VirtualMachineInfo* vmInfo)
{
    ExecutionInfoObtainer::runAndObtainExecutionInfo(executionInfo, [virtualization, name, vmInfo]
    {
        virtualization->getInfoAboutVirtualMachine(vmInfo, name);
    });
}

void GetListOfVirtualMachinesWithInfo(ExecutionInfo* executionInfo,
                                      VirtualizationFacade* virtualization,
                                      VirtualMachineInfo** arrayOfVms,
                                      int* numberOfVms)
{
    ExecutionInfoObtainer::runAndObtainExecutionInfo(executionInfo, [virtualization, arrayOfVms, numberOfVms]
    {
        virtualization->getListOfVirtualMachinesWithInfo(arrayOfVms, numberOfVms);
    });
}

void IsConnectionAlive(ExecutionInfo* executionInfo, VirtualizationFacade* virtualization,
                       bool* isAlive)
{
    ExecutionInfoObtainer::runAndObtainExecutionInfo(executionInfo, [virtualization, isAlive]
    {
        virtualization->isConnectionAlive(isAlive);
    });
}

void OpenVirtualMachineConsole(ExecutionInfo* executionInfo, VirtualizationFacade* virtualization, virStreamPtr* stream,
                               const char* vmUuid)
{
    ExecutionInfoObtainer::runAndObtainExecutionInfo(executionInfo, [virtualization, stream, vmUuid]
    {
        *stream = virtualization->openVirtualMachineConsole(vmUuid);
    });
}

void ReceiveDataFromConsole(ExecutionInfo* executionInfo, VirtualizationFacade* virtualization, virStreamPtr stream,
                            StreamData* streamData)
{
    ExecutionInfoObtainer::runAndObtainExecutionInfo(executionInfo, [virtualization, stream, streamData]
    {
        virtualization->receiveDataFromConsole(stream, streamData);
    });
}

void SendDataToConsole(ExecutionInfo* executionInfo, VirtualizationFacade* virtualization, virStreamPtr stream,
                       const char* data)
{
    ExecutionInfoObtainer::runAndObtainExecutionInfo(executionInfo, [virtualization, stream, data]
    {
        virtualization->sendDataToConsole(stream, data);
    });
}

void CloseStream(ExecutionInfo* executionInfo, VirtualizationFacade* virtualization, virStreamPtr stream)
{
    ExecutionInfoObtainer::runAndObtainExecutionInfo(executionInfo, [virtualization, stream]
    {
        virtualization->closeStream(stream);
    });
}

void CreateVirtualNetwork(ExecutionInfo* executionInfo, VirtualizationFacade* virtualization, virNetworkPtr* network,
                          const char* networkDefinition)
{
    ExecutionInfoObtainer::runAndObtainExecutionInfo(executionInfo, [virtualization, network, networkDefinition]
    {
        *network = virtualization->createVirtualNetworkFromXml(networkDefinition);
    });
}

void AttachDeviceToVirtualMachine(ExecutionInfo* executionInfo, VirtualizationFacade* virtualization, const char* uuid,
                                  const char* deviceDefinition)
{
    ExecutionInfoObtainer::runAndObtainExecutionInfo(executionInfo, [virtualization, uuid, deviceDefinition]
    {
        virtualization->attachDeviceToVm(uuid, deviceDefinition);
    });
}

void DetachDeviceFromVirtualMachine(ExecutionInfo* executionInfo, VirtualizationFacade* virtualization,
                                    const char* uuid, const char* deviceDefinition)
{
    ExecutionInfoObtainer::runAndObtainExecutionInfo(executionInfo, [virtualization, uuid, deviceDefinition]
    {
        virtualization->detachDeviceFromVm(uuid, deviceDefinition);
    });
}

void UpdateVmDevice(ExecutionInfo* executionInfo, VirtualizationFacade* virtualization, const char* uuid,
                    const char* deviceDefinition)
{
    ExecutionInfoObtainer::runAndObtainExecutionInfo(executionInfo, [virtualization, uuid, deviceDefinition]
    {
        virtualization->updateVmDevice(uuid, deviceDefinition);
    });
}

void DestroyNetwork(ExecutionInfo* executionInfo, VirtualizationFacade* virtualization, const char* uuid)
{
    ExecutionInfoObtainer::runAndObtainExecutionInfo(executionInfo, [virtualization, uuid]
    {
        virtualization->destroyNetwork(uuid);
    });
}
}

#endif // INTERCONNECTLIBRARYEXTERN_H
