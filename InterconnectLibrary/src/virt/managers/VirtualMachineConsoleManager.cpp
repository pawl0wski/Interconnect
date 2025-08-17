#include "VirtualMachineConsoleManager.h"

#include "../../exceptions/VirtualMachineManagerException.h"

void VirtualMachineConsoleManager::updateConnection(const virConnectPtr conn)
{
    this->conn = conn;
}

int VirtualMachineConsoleManager::openVirtualMachineConsole(const std::string& vmUuid)
{
    if (conn == nullptr)
    {
        throw VirtualMachineManagerException("No active connection to the VM backend");
    }

    const auto domain = libvirt->domainLookupByUuid(conn, vmUuid);
    if (domain == nullptr)
    {
        throw VirtualMachineManagerException("Can not find domain");
    }

    const auto stream = libvirt->createNewStream(conn);
    if (libvirt->openDomainConsole(domain, stream))
    {
        throw VirtualMachineManagerException("Cannot open console");
    }

    return addNewStream(stream);
}

int VirtualMachineConsoleManager::addNewStream(const virStreamPtr stream)
{
    const auto streamId = static_cast<int>(this->streams.size() + 1);
    this->streams.push_back(IdentifiedStream(streamId, stream));
    return streamId;
}

void VirtualMachineConsoleManager::removeStream(const int streamId)
{
    const auto stream = getStreamById(streamId);
    this->streams.erase(this->streams.begin() + (streamId - 1));
    libvirt->finishAndFreeStream(stream);
}

void VirtualMachineConsoleManager::getDataFromStream(const int streamId, char* data, const int dataSize) const
{
    const auto stream = getStreamById(streamId);
    libvirt->receiveDataFromStream(stream, data, dataSize);
}

void VirtualMachineConsoleManager::sendDataToStream(const int streamId, const char* data, const int dataSize) const
{
    const auto stream = getStreamById(streamId);
    libvirt->sendDataToStream(stream, data, dataSize);
}

virStreamPtr VirtualMachineConsoleManager::getStreamById(const int streamId) const
{
    return this->streams.at(streamId).stream;
}
