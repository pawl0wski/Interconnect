#include "VirtualMachineConsoleManager.h"

#include <iostream>

#include "../../exceptions/VirtualizationException.h"

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

void VirtualMachineConsoleManager::getDataFromStream(virStreamPtr stream, StreamData* streamData) const
{
    streamData->isStreamBroken = false;
    if (libvirt->receiveDataFromStream(stream, streamData->buffer, 255) <= 0)
    {
        streamData->isStreamBroken = true;
    }
}

void VirtualMachineConsoleManager::sendDataToStream(virStreamPtr stream, const char* data,
                                                    const int dataSize) const
{
    libvirt->sendDataToStream(stream, data, dataSize);
}

void VirtualMachineConsoleManager::closeStream(virStreamPtr stream) const
{
    libvirt->finishAndFreeStream(stream);
}
