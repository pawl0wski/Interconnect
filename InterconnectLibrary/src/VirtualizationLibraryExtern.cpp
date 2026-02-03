#ifndef VIRTUALIZATIONLIBRARYEXTERN_H
#define VIRTUALIZATIONLIBRARYEXTERN_H

#include "models/ConnectionInfo.h"
#include "models/NetworkDefinition.h"
#include "models/StreamData.h"
#include "utils/ExecutionInfoObtainer.h"
#include "utils/StringUtils.h"
#include "virt/VirtualizationFacade.h"

extern "C" {
/**
 * @brief Creates a new VirtualizationFacade instance.
 * 
 * Allocates and initializes a new VirtualizationFacade with default libvirt wrapper.
 * 
 * @return VirtualizationFacade* Pointer to the newly created VirtualizationFacade instance.
 */
VirtualizationFacade* CreateVirtualizationFacade()
{
    return new VirtualizationFacade();
}

/**
 * @brief Destroys a VirtualizationFacade instance.
 * 
 * Deallocates and releases all resources associated with the VirtualizationFacade.
 * 
 * @param virtualization Pointer to the VirtualizationFacade instance to destroy.
 */
void DestroyVirtualizationFacade(const VirtualizationFacade* virtualization)
{
    delete virtualization;
}

/**
 * @brief Initializes a hypervisor connection.
 * 
 * Safely wraps VirtualizationFacade::initializeConnection with error handling.
 * 
 * @param executionInfo Pointer to ExecutionInfo structure for error information.
 * @param virtualization VirtualizationFacade instance.
 * @param customConnectionUrl Optional custom connection URI. If nullptr, uses default.
 */
void Virtualization_InitializeConnection(ExecutionInfo* executionInfo, VirtualizationFacade* virtualization,
                          const char* customConnectionUrl)
{
    ExecutionInfoObtainer::runAndObtainExecutionInfo(executionInfo, [virtualization, customConnectionUrl]
    {
        virtualization->initializeConnection(customConnectionUrl);
    });
}

/**
 * @brief Gets information about the hypervisor connection.
 * 
 * Safely wraps VirtualizationFacade::getConnectionInfo with error handling.
 * 
 * @param executionInfo Pointer to ExecutionInfo structure for error information.
 * @param virtualization VirtualizationFacade instance.
 * @param infoPtr Pointer to ConnectionInfo structure to be populated.
 */
void Virtualization_GetConnectionInfo(ExecutionInfo* executionInfo, VirtualizationFacade* virtualization,
                       ConnectionInfo* infoPtr)
{
    ExecutionInfoObtainer::runAndObtainExecutionInfo(executionInfo, [infoPtr, virtualization]
    {
        virtualization->getConnectionInfo(infoPtr);
    });
}

/**
 * @brief Creates a virtual machine from an XML definition.
 * 
 * Safely wraps VirtualizationFacade::createVirtualMachine with error handling.
 * 
 * @param executionInfo Pointer to ExecutionInfo structure for error information.
 * @param virtualization VirtualizationFacade instance.
 * @param virtualMachineXml XML definition of the virtual machine.
 */
void Virtualization_CreateVirtualMachine(ExecutionInfo* executionInfo, VirtualizationFacade* virtualization,
                          const char* virtualMachineXml)
{
    ExecutionInfoObtainer::runAndObtainExecutionInfo(executionInfo, [virtualization, virtualMachineXml]
    {
        virtualization->createVirtualMachine(virtualMachineXml);
    });
}

/**
 * @brief Gets information about a specific virtual machine.
 * 
 * Safely wraps VirtualizationFacade::getInfoAboutVirtualMachine with error handling.
 * 
 * @param executionInfo Pointer to ExecutionInfo structure for error information.
 * @param virtualization VirtualizationFacade instance.
 * @param name Name of the virtual machine.
 * @param vmInfo Pointer to VirtualMachineInfo structure to be populated.
 */
void Virtualization_GetInfoAboutVirtualMachine(ExecutionInfo* executionInfo,
                                VirtualizationFacade* virtualization,
                                const char* name, VirtualMachineInfo* vmInfo)
{
    ExecutionInfoObtainer::runAndObtainExecutionInfo(executionInfo, [virtualization, name, vmInfo]
    {
        virtualization->getInfoAboutVirtualMachine(vmInfo, name);
    });
}

/**
 * @brief Gets information about all virtual machines.
 * 
 * Safely wraps VirtualizationFacade::getListOfVirtualMachinesWithInfo with error handling.
 * 
 * @param executionInfo Pointer to ExecutionInfo structure for error information.
 * @param virtualization VirtualizationFacade instance.
 * @param arrayOfVms Pointer to array of VirtualMachineInfo structures.
 * @param numberOfVms Pointer to store the count of virtual machines.
 */
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

/**
 * @brief Checks if the hypervisor connection is still active.
 * 
 * Safely wraps VirtualizationFacade::isConnectionAlive with error handling.
 * 
 * @param executionInfo Pointer to ExecutionInfo structure for error information.
 * @param virtualization VirtualizationFacade instance.
 * @param isAlive Pointer to boolean to store the connection status.
 */
void Virtualization_IsConnectionAlive(ExecutionInfo* executionInfo, VirtualizationFacade* virtualization,
                       bool* isAlive)
{
    ExecutionInfoObtainer::runAndObtainExecutionInfo(executionInfo, [virtualization, isAlive]
    {
        virtualization->isConnectionAlive(isAlive);
    });
}

/**
 * @brief Opens a console connection to a virtual machine.
 * 
 * Safely wraps VirtualizationFacade::openVirtualMachineConsole with error handling.
 * 
 * @param executionInfo Pointer to ExecutionInfo structure for error information.
 * @param virtualization VirtualizationFacade instance.
 * @param stream Pointer to receive the console stream.
 * @param vmUuid UUID of the virtual machine.
 */
void Virtualization_OpenVirtualMachineConsole(ExecutionInfo* executionInfo, VirtualizationFacade* virtualization, virStreamPtr* stream,
                               const char* vmUuid)
{
    ExecutionInfoObtainer::runAndObtainExecutionInfo(executionInfo, [virtualization, stream, vmUuid]
    {
        *stream = virtualization->openVirtualMachineConsole(vmUuid);
    });
}

/**
 * @brief Receives data from a virtual machine console.
 * 
 * Safely wraps VirtualizationFacade::receiveDataFromConsole with error handling.
 * 
 * @param executionInfo Pointer to ExecutionInfo structure for error information.
 * @param virtualization VirtualizationFacade instance.
 * @param stream Console stream.
 * @param streamData Pointer to StreamData structure to receive the data.
 */
void Virtualization_ReceiveDataFromConsole(ExecutionInfo* executionInfo, VirtualizationFacade* virtualization, virStreamPtr stream,
                            StreamData* streamData)
{
    ExecutionInfoObtainer::runAndObtainExecutionInfo(executionInfo, [virtualization, stream, streamData]
    {
        virtualization->receiveDataFromConsole(stream, streamData);
    });
}

/**
 * @brief Sends data to a virtual machine console.
 * 
 * Safely wraps VirtualizationFacade::sendDataToConsole with error handling.
 * 
 * @param executionInfo Pointer to ExecutionInfo structure for error information.
 * @param virtualization VirtualizationFacade instance.
 * @param stream Console stream.
 * @param data Data to send to the console.
 */
void Virtualization_SendDataToConsole(ExecutionInfo* executionInfo, VirtualizationFacade* virtualization, virStreamPtr stream,
                       const char* data)
{
    ExecutionInfoObtainer::runAndObtainExecutionInfo(executionInfo, [virtualization, stream, data]
    {
        virtualization->sendDataToConsole(stream, data);
    });
}

/**
 * @brief Closes a virtual machine console stream.
 * 
 * Safely wraps VirtualizationFacade::closeStream with error handling.
 * 
 * @param executionInfo Pointer to ExecutionInfo structure for error information.
 * @param virtualization VirtualizationFacade instance.
 * @param stream Console stream to close.
 */
void Virtualization_CloseStream(ExecutionInfo* executionInfo, VirtualizationFacade* virtualization, virStreamPtr stream)
{
    ExecutionInfoObtainer::runAndObtainExecutionInfo(executionInfo, [virtualization, stream]
    {
        virtualization->closeStream(stream);
    });
}

/**
 * @brief Creates a virtual network from an XML definition.
 * 
 * Safely wraps VirtualizationFacade::createVirtualNetworkFromXml with error handling.
 * 
 * @param executionInfo Pointer to ExecutionInfo structure for error information.
 * @param virtualization VirtualizationFacade instance.
 * @param network Pointer to receive the created network.
 * @param networkDefinition XML definition of the network.
 */
void Virtualization_CreateVirtualNetwork(ExecutionInfo* executionInfo, VirtualizationFacade* virtualization, virNetworkPtr* network,
                          const char* networkDefinition)
{
    ExecutionInfoObtainer::runAndObtainExecutionInfo(executionInfo, [virtualization, network, networkDefinition]
    {
        *network = virtualization->createVirtualNetworkFromXml(networkDefinition);
    });
}

/**
 * @brief Attaches a device to a virtual machine.
 * 
 * Safely wraps VirtualizationFacade::attachDeviceToVm with error handling.
 * 
 * @param executionInfo Pointer to ExecutionInfo structure for error information.
 * @param virtualization VirtualizationFacade instance.
 * @param uuid UUID of the virtual machine.
 * @param deviceDefinition XML definition of the device to attach.
 */
void Virtualization_AttachDeviceToVirtualMachine(ExecutionInfo* executionInfo, VirtualizationFacade* virtualization, const char* uuid,
                                  const char* deviceDefinition)
{
    ExecutionInfoObtainer::runAndObtainExecutionInfo(executionInfo, [virtualization, uuid, deviceDefinition]
    {
        virtualization->attachDeviceToVm(uuid, deviceDefinition);
    });
}

/**
 * @brief Detaches a device from a virtual machine.
 * 
 * Safely wraps VirtualizationFacade::detachDeviceFromVm with error handling.
 * 
 * @param executionInfo Pointer to ExecutionInfo structure for error information.
 * @param virtualization VirtualizationFacade instance.
 * @param uuid UUID of the virtual machine.
 * @param deviceDefinition XML definition of the device to detach.
 */
void Virtualization_DetachDeviceFromVirtualMachine(ExecutionInfo* executionInfo, VirtualizationFacade* virtualization,
                                    const char* uuid, const char* deviceDefinition)
{
    ExecutionInfoObtainer::runAndObtainExecutionInfo(executionInfo, [virtualization, uuid, deviceDefinition]
    {
        virtualization->detachDeviceFromVm(uuid, deviceDefinition);
    });
}

/**
 * @brief Updates a device configuration on a virtual machine.
 * 
 * Safely wraps VirtualizationFacade::updateVmDevice with error handling.
 * 
 * @param executionInfo Pointer to ExecutionInfo structure for error information.
 * @param virtualization VirtualizationFacade instance.
 * @param uuid UUID of the virtual machine.
 * @param deviceDefinition Updated XML definition of the device.
 */
void Virtualization_UpdateVmDevice(ExecutionInfo* executionInfo, VirtualizationFacade* virtualization, const char* uuid,
                    const char* deviceDefinition)
{
    ExecutionInfoObtainer::runAndObtainExecutionInfo(executionInfo, [virtualization, uuid, deviceDefinition]
    {
        virtualization->updateVmDevice(uuid, deviceDefinition);
    });
}

/**
 * @brief Destroys a virtual network.
 * 
 * Safely wraps VirtualizationFacade::destroyNetwork with error handling.
 * 
 * @param executionInfo Pointer to ExecutionInfo structure for error information.
 * @param virtualization VirtualizationFacade instance.
 * @param name Name of the network to destroy.
 */
void Virtualization_DestroyNetwork(ExecutionInfo* executionInfo, VirtualizationFacade* virtualization, const char* name)
{
    ExecutionInfoObtainer::runAndObtainExecutionInfo(executionInfo, [virtualization, name]
    {
        virtualization->destroyNetwork(name);
    });
}

/**
 * @brief Gets the XML definition of a virtual network.
 * 
 * Safely wraps VirtualizationFacade::getNetworkDefinition with error handling.
 * 
 * @param executionInfo Pointer to ExecutionInfo structure for error information.
 * @param virtualization VirtualizationFacade instance.
 * @param name Name of the network.
 * @param definition Pointer to NetworkDefinition structure to receive the XML.
 */
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
