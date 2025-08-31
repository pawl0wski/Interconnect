#ifndef VIRTUALMACHINECONSOLEMANAGER_H
#define VIRTUALMACHINECONSOLEMANAGER_H
#include "BaseManagerWithConnection.h"
#include "../../LibvirtWrapper.h"
#include "../../interfaces/ILibvirtWrapper.h"
#include "../../models/StreamData.h"

class VirtualMachineConsoleManager : public BaseManagerWithConnection
{
public:
    using BaseManagerWithConnection::BaseManagerWithConnection;

    virStreamPtr openVirtualMachineConsole(const std::string& vmUuid) const;

    void getDataFromStream(virStreamPtr stream, StreamData* streamData) const;

    void sendDataToStream(virStreamPtr stream, const char* data, int dataSize) const;

    void closeStream(virStreamPtr stream) const;
};


#endif //VIRTUALMACHINECONSOLEMANAGER_H
