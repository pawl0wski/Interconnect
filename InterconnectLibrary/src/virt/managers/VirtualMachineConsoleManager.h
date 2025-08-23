#ifndef VIRTUALMACHINECONSOLEMANAGER_H
#define VIRTUALMACHINECONSOLEMANAGER_H
#include "../../LibvirtWrapper.h"
#include "../../interfaces/ILibvirtWrapper.h"
#include "../../models/StreamData.h"

class VirtualMachineConsoleManager
{
    ILibvirtWrapper* libvirt;
    virConnectPtr conn;

public:
    virtual ~VirtualMachineConsoleManager() = default;

    /**
     * @param libvirt Reference to an ILibvirtWrapper implementation.
     */
    explicit VirtualMachineConsoleManager(ILibvirtWrapper* libvirt)
        : libvirt(libvirt), conn(nullptr)
    {
    }

    explicit VirtualMachineConsoleManager(): conn(nullptr)
    {
        libvirt = new LibvirtWrapper();
    }

    void updateConnection(virConnectPtr conn);

    virStreamPtr openVirtualMachineConsole(const std::string& vmUuid) const;

    void getDataFromStream(virStreamPtr stream, StreamData* streamData) const;

    void sendDataToStream(virStreamPtr stream, const char* data, int dataSize) const;

    void closeStream(virStreamPtr stream) const;
};


#endif //VIRTUALMACHINECONSOLEMANAGER_H
