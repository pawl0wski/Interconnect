#include "BaseManagerWithConnection.h"

#include "../../exceptions/VirtualizationException.h"

/**
 * @brief Updates the hypervisor connection for this manager.
 * 
 * Sets the connection pointer that will be used by this manager instance
 * for all virtualization operations.
 * 
 * @param conn The hypervisor connection pointer.
 */
void BaseManagerWithConnection::updateConnection(virConnectPtr conn)
{
    this->conn = conn;
}

/**
 * @brief Verifies that a hypervisor connection has been set.
 * 
 * Checks if the connection pointer is valid (not nullptr).
 * 
 * @throws VirtualizationException if no active connection has been set.
 */
void BaseManagerWithConnection::checkIfConnectionIsSet() const
{
    if (conn == nullptr)
    {
        throw VirtualizationException("No active connection to the VM backend");
    }
}
