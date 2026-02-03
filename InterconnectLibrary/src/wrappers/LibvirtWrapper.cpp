#include "LibvirtWrapper.h"

#include <stdexcept>

/**
 * @brief Opens a connection to a hypervisor.
 * 
 * @param connectionUri The URI of the hypervisor to connect to.
 * @return virConnectPtr Pointer to the connection, or nullptr on failure.
 */
virConnectPtr LibvirtWrapper::connectOpen(const char* connectionUri)
{
    return virConnectOpen(connectionUri);
}

/**
 * @brief Creates a virtual machine from an XML definition.
 * 
 * @param conn The hypervisor connection.
 * @param xmlConfig XML definition of the virtual machine.
 * @return virDomainPtr Pointer to the created domain, or nullptr on failure.
 */
virDomainPtr LibvirtWrapper::createVirtualMachineFromXml(const virConnectPtr conn, const char* xmlConfig)
{
    return virDomainCreateXML(conn, xmlConfig, 0);
}

/**
 * @brief Retrieves the UUID from a domain.
 * 
 * @param domain The virtual machine domain.
 * @param uuid Buffer to store the UUID string.
 */
void LibvirtWrapper::getUuidFromDomain(const virDomainPtr domain, char* uuid)
{
    virDomainGetUUIDString(domain, uuid);
}

/**
 * @brief Retrieves node information from the hypervisor.
 * 
 * @param conn The hypervisor connection.
 * @param info Pointer to virNodeInfo structure to be populated.
 * @return int 0 on success, -1 on failure.
 */
int LibvirtWrapper::getNodeInfo(const virConnectPtr conn, const virNodeInfoPtr info)
{
    return virNodeGetInfo(conn, info);
}

/**
 * @brief Gets the version of the libvirt library.
 * 
 * @param conn The hypervisor connection.
 * @param libVersion Pointer to store the library version.
 * @return int 0 on success, -1 on failure.
 */
int LibvirtWrapper::getLibVersion(const virConnectPtr conn, unsigned long* libVersion)
{
    return virConnectGetLibVersion(conn, libVersion);
}

/**
 * @brief Gets the version of the hypervisor driver.
 * 
 * @param conn The hypervisor connection.
 * @param version Pointer to store the driver version.
 * @return int 0 on success, -1 on failure.
 */
int LibvirtWrapper::getDriverVersion(const virConnectPtr conn, unsigned long* version)
{
    return virConnectGetVersion(conn, version);
}

/**
 * @brief Gets the URI of the hypervisor connection.
 * 
 * @param conn The hypervisor connection.
 * @return std::string The connection URI.
 */
std::string LibvirtWrapper::getConnectUrl(const virConnectPtr conn)
{
    return virConnectGetURI(conn);
}

/**
 * @brief Gets the type of hypervisor driver.
 * 
 * @param conn The hypervisor connection.
 * @return std::string The driver type (e.g., "qemu", "xen").
 */
std::string LibvirtWrapper::getDriverType(const virConnectPtr conn)
{
    return virConnectGetType(conn);
}

/**
 * @brief Gets the last error message from libvirt.
 * 
 * @return std::string The error message from the last failed operation.
 */
std::string LibvirtWrapper::getLastError()
{
    const virErrorPtr err = virGetLastError();
    return std::string(err->message);
}

/**
 * @brief Looks up a domain by its name.
 * 
 * @param conn The hypervisor connection.
 * @param name The name of the virtual machine to look up.
 * @return virDomainPtr Pointer to the domain, or nullptr if not found.
 */
virDomainPtr LibvirtWrapper::domainLookupByName(const virConnectPtr conn, std::string name)
{
    return virDomainLookupByName(conn, name.c_str());
}

/**
 * @brief Gets information about a domain.
 * 
 * @param domain The virtual machine domain.
 * @param domainInfo Reference to virDomainInfo structure to be populated.
 * @return int 0 on success, -1 on failure.
 */
int LibvirtWrapper::domainGetInfo(const virDomainPtr domain, virDomainInfo& domainInfo)
{
    return virDomainGetInfo(domain, &domainInfo);
}

/**
 * @brief Gets the UUID of a domain as a string.
 * 
 * @param domain The virtual machine domain.
 * @param uuid Reference to string to store the UUID.
 * @return int 0 on success, -1 on failure.
 */
int LibvirtWrapper::getDomainUUID(const virDomainPtr domain, std::string& uuid)
{
    char uuid_cstr[VIR_UUID_STRING_BUFLEN];
    const int result = virDomainGetUUIDString(domain, uuid_cstr);
    if (result == 0)
    {
        uuid = std::string(uuid_cstr);
    }
    return result;
}

/**
 * @brief Gets a list of all domains (both active and inactive).
 * 
 * @param conn The hypervisor connection.
 * @param domains Pointer to array of virDomainPtr to be allocated and populated.
 * @return int Number of domains retrieved, or -1 on error.
 */
int LibvirtWrapper::getListOfAllDomains(const virConnectPtr conn, virDomainPtr** domains)
{
    return virConnectListAllDomains(conn, domains,
                                    VIR_CONNECT_LIST_DOMAINS_ACTIVE | VIR_CONNECT_LIST_DOMAINS_INACTIVE);
}

/**
 * @brief Gets the name of a domain.
 * 
 * @param domain The virtual machine domain.
 * @return std::string The domain name, or empty string if retrieval fails.
 */
std::string LibvirtWrapper::getDomainName(const virDomainPtr domain)
{
    auto name = virDomainGetName(domain);
    if (name == nullptr)
    {
        return "";
    }
    return {name};
}

/**
 * @brief Frees resources associated with a domain pointer.
 * 
 * @param domain The domain pointer to free.
 * @throws std::runtime_error if freeing the domain fails.
 */
void LibvirtWrapper::freeDomain(const virDomainPtr domain)
{
    if (virDomainFree(domain) != 0)
    {
        throw std::runtime_error("freeDomain failed");
    }
}

/**
 * @brief Checks if the hypervisor connection is still alive.
 * 
 * @param conn The hypervisor connection.
 * @return int 1 if alive, 0 if dead, -1 on error.
 */
int LibvirtWrapper::connectionIsAlive(const virConnectPtr conn)
{
    return virConnectIsAlive(conn);
}

/**
 * @brief Creates a new stream for communication with a domain.
 * 
 * @param conn The hypervisor connection.
 * @return virStreamPtr Pointer to the new stream, or nullptr on failure.
 */
virStreamPtr LibvirtWrapper::createNewStream(const virConnectPtr conn)
{
    return virStreamNew(conn, 0);
}

/**
 * @brief Opens a console to a domain via a stream.
 * 
 * @param domain The virtual machine domain.
 * @param stream The stream to connect to the console.
 * @return int 0 on success, -1 on failure.
 */
int LibvirtWrapper::openDomainConsole(const virDomainPtr domain, const virStreamPtr stream)
{
    return virDomainOpenConsole(domain, nullptr, stream, 0);
}

/**
 * @brief Looks up a domain by its UUID.
 * 
 * @param conn The hypervisor connection.
 * @param uuid The UUID string of the virtual machine.
 * @return virDomainPtr Pointer to the domain, or nullptr if not found.
 */
virDomainPtr LibvirtWrapper::domainLookupByUuid(const virConnectPtr conn, const std::string& uuid)
{
    return virDomainLookupByUUIDString(conn, uuid.c_str());
}

/**
 * @brief Receives data from a stream.
 * 
 * @param stream The stream to read from.
 * @param buffer Buffer to store received data.
 * @param bufferSize Size of the buffer.
 * @return int Number of bytes received, 0 if stream was closed, -1 on error.
 */
int LibvirtWrapper::receiveDataFromStream(const virStreamPtr stream, char* buffer, const int bufferSize)
{
    return virStreamRecv(stream, buffer, bufferSize);
}

/**
 * @brief Sends data to a stream.
 * 
 * @param stream The stream to write to.
 * @param buffer Data to send.
 * @param bufferSize Size of the data.
 */
void LibvirtWrapper::sendDataToStream(const virStreamPtr stream, const char* buffer, const int bufferSize)
{
    virStreamSend(stream, buffer, bufferSize);
}

/**
 * @brief Finishes and frees a stream.
 * 
 * Closes the stream and releases all associated resources.
 * 
 * @param stream The stream to finish and free.
 */
void LibvirtWrapper::finishAndFreeStream(const virStreamPtr stream)
{
    virStreamFinish(stream);
    virStreamFree(stream);
}

/**
 * @brief Creates a network from an XML definition.
 * 
 * @param conn The hypervisor connection.
 * @param networkDefinition XML definition of the network.
 * @return virNetworkPtr Pointer to the created network, or nullptr on failure.
 */
virNetworkPtr LibvirtWrapper::createNetworkFromXml(virConnectPtr conn, const std::string& networkDefinition)
{
    return virNetworkCreateXML(conn, networkDefinition.c_str());
}

/**
 * @brief Attaches a device to a virtual machine.
 * 
 * @param domain The virtual machine domain.
 * @param deviceDefinition XML definition of the device to attach.
 * @return int 0 on success, -1 on failure.
 */
int LibvirtWrapper::attachDeviceToVm(virDomainPtr domain, const std::string& deviceDefinition)
{
    return virDomainAttachDevice(domain, deviceDefinition.c_str());
}

/**
 * @brief Detaches a device from a virtual machine.
 * 
 * @param domain The virtual machine domain.
 * @param deviceDefinition XML definition of the device to detach.
 * @return int 0 on success, -1 on failure.
 */
int LibvirtWrapper::detachDeviceFromVm(virDomainPtr domain, const std::string& deviceDefinition)
{
    return virDomainDetachDevice(domain, deviceDefinition.c_str());
}

/**
 * @brief Updates a device configuration on a virtual machine.
 * 
 * @param domain The virtual machine domain.
 * @param deviceDefinition Updated XML definition of the device.
 * @return int 0 on success, -1 on failure.
 */
int LibvirtWrapper::updateVmDevice(virDomainPtr domain, const std::string& deviceDefinition)
{
    return virDomainUpdateDeviceFlags(domain, deviceDefinition.c_str(), VIR_DOMAIN_AFFECT_LIVE);
}

/**
 * @brief Looks up a network by name.
 * 
 * @param conn The hypervisor connection.
 * @param name The name of the network.
 * @return virNetworkPtr Pointer to the network, or nullptr if not found.
 */
virNetworkPtr LibvirtWrapper::getNetworkByName(virConnectPtr conn, const std::string& name)
{
    return virNetworkLookupByName(conn, name.c_str());
}

/**
 * @brief Destroys (deletes) a network.
 * 
 * @param network The network to destroy.
 * @return int 0 on success, -1 on failure.
 */
int LibvirtWrapper::destroyNetwork(virNetworkPtr network)
{
    return virNetworkDestroy(network);
}

/**
 * @brief Gets the XML definition of a network.
 * 
 * @param network The network.
 * @return std::string The XML definition.
 */
std::string LibvirtWrapper::getNetworkDefinition(virNetworkPtr network)
{
    return virNetworkGetXMLDesc(network, 0);
}
