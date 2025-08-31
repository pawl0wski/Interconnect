#include "BaseManagerWithConnection.h"

#include "../../exceptions/VirtualMachineManagerException.h"

void BaseManagerWithConnection::updateConnection(virConnectPtr conn)
{
    this->conn = conn;
}

void BaseManagerWithConnection::checkIfConnectionIsSet() const
{
    if (conn == nullptr)
    {
        throw VirtualMachineManagerException("No active connection to the VM backend");
    }
}
