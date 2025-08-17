#ifndef VIRTUALMACHINECONSOLEMANAGER_H
#define VIRTUALMACHINECONSOLEMANAGER_H
#include <vector>

#include "../../LibvirtWrapper.h"
#include "../../interfaces/ILibvirtWrapper.h"
#include "../../models/IdentifiedStream.h"

class VirtualMachineConsoleManager
{
    ILibvirtWrapper* libvirt;
    virConnectPtr conn;
    std::vector<IdentifiedStream> streams;

public:
    virtual ~VirtualMachineConsoleManager() = default;

    /**
     * @param libvirt Reference to an ILibvirtWrapper implementation.
     */
    explicit VirtualMachineConsoleManager(ILibvirtWrapper* libvirt)
        : libvirt(libvirt)
    {
    }

    explicit VirtualMachineConsoleManager()
    {
        libvirt = new LibvirtWrapper();
    }

    void updateConnection(virConnectPtr conn);

    int openVirtualMachineConsole(std::string& vmUuid);

    void removeStream(int streamId);

    void getDataFromStream(int streamId, char* data, int dataSize) const;

    void sendDataToStream(int streamId, const char* data, int dataSize) const;

private:
    virStreamPtr getStreamById(int streamId) const;
    int addNewStream(virStreamPtr stream);
};


#endif //VIRTUALMACHINECONSOLEMANAGER_H
