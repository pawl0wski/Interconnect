#include "BaseManagerWithConnection.h"

#include "../../exceptions/VirtualizationException.h"

void BaseManagerWithConnection::updateConnection(virConnectPtr conn)
{
    this->conn = conn;
}

void BaseManagerWithConnection::checkIfConnectionIsSet() const
{
    if (conn == nullptr)
    {
        throw VirtualizationException("No active connection to the VM backend");
    }
}
