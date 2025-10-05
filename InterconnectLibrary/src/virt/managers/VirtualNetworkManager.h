#ifndef VIRTUALNETWORKMANAGER_H
#define VIRTUALNETWORKMANAGER_H
#include "BaseManagerWithConnection.h"
#include "../../wrappers/LibvirtWrapper.h"
#include "../../interfaces/ILibvirtWrapper.h"

class VirtualNetworkManager : public BaseManagerWithConnection
{
public:
    using BaseManagerWithConnection::BaseManagerWithConnection;

    virNetworkPtr createNetworkFromXml(const std::string& networkDefinition) const;

    void destroyNetwork(const std::string& name) const;

    [[nodiscard]] std::string getNetworkXmlDefinition(const std::string& name) const;
};

#endif //VIRTUALNETWORKMANAGER_H
