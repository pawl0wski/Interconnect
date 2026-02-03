#include "VirtualMachineConsoleManager.h"

#include <iostream>

#include "../../exceptions/VirtualizationException.h"

/**
 * @brief Opens a console connection to a virtual machine.
 * 
 * Creates a stream and opens a console connection to the specified VM,
 * allowing text I/O with the VM console.
 * 
 * @param vmUuid The UUID of the virtual machine.
 * @return virStreamPtr Stream pointer for console communication.
 * @throws VirtualizationException if connection is not set, VM not found, or console opening fails.
 */
virStreamPtr VirtualMachineConsoleManager::openVirtualMachineConsole(const std::string& vmUuid) const
{
    checkIfConnectionIsSet();

    const auto domain = libvirt->domainLookupByUuid(conn, vmUuid);
    if (domain == nullptr)
    {
        throw VirtualizationException("Can not find domain");
    }

    const auto stream = libvirt->createNewStream(conn);
    if (libvirt->openDomainConsole(domain, stream) == -1)
    {
        throw VirtualizationException("Cannot open console");
    }

    return stream;
}

/**
 * @brief Receives data from a console stream.
 * 
 * Attempts to read data from the stream. Sets isStreamBroken flag if
 * the stream is closed or an error occurs.
 * 
 * @param stream The console stream.
 * @param streamData Pointer to StreamData structure to store received data and status.
 */
void VirtualMachineConsoleManager::getDataFromStream(virStreamPtr stream, StreamData* streamData) const
{
    streamData->isStreamBroken = false;
    if (libvirt->receiveDataFromStream(stream, streamData->buffer, 255) <= 0)
    {
        streamData->isStreamBroken = true;
    }
}

/**
 * @brief Sends data to a console stream.
 * 
 * Writes data to the stream for transmission to the VM console.
 * 
 * @param stream The console stream.
 * @param data The data to send.
 * @param dataSize Size of the data in bytes.
 */
void VirtualMachineConsoleManager::sendDataToStream(virStreamPtr stream, const char* data,
                                                    const int dataSize) const
{
    libvirt->sendDataToStream(stream, data, dataSize);
}

/**
 * @brief Closes and frees a console stream.
 * 
 * Properly closes the stream and releases all associated resources.
 * 
 * @param stream The stream to close.
 */
void VirtualMachineConsoleManager::closeStream(virStreamPtr stream) const
{
    libvirt->finishAndFreeStream(stream);
}
