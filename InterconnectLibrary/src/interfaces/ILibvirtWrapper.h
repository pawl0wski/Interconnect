#ifndef ILIBVIRTWRAPPER_H
#define ILIBVIRTWRAPPER_H
#include <string>
#include <libvirt/libvirt.h>

/**
 * @brief Interface for Libvirt wrapper functionality.
 *
 * This abstract class defines an interface for interacting with
 * the libvirt API.
 */
class ILibvirtWrapper
{
public:
    virtual ~ILibvirtWrapper() = default;

    /**
     * @brief Opens a connection to a hypervisor.
     *
     * @param name URI or name of the hypervisor connection.
     * @return virConnectPtr Connection handle, or nullptr on failure.
     */
    virtual virConnectPtr connectOpen(const char* name) = 0;

    /**
     * @brief Creates a virtual machine from XML configuration.
     *
     * @param conn Active connection to the hypervisor.
     * @param xmlConfig XML string describing the VM configuration.
     * @return virDomainPtr Domain handle for the created VM.
     */
    virtual virDomainPtr createVirtualMachineFromXml(virConnectPtr conn, const char* xmlConfig) = 0;

    /**
     * @brief Retrieves the UUID of a domain.
     *
     * @param domain Domain to get UUID from.
     * @param uuid Buffer to store the UUID string (must be at least VIR_UUID_STRING_BUFLEN bytes).
     */
    virtual void getUuidFromDomain(virDomainPtr domain, char* uuid) = 0;

    /**
     * @brief Gets information about the physical host node.
     *
     * @param conn Active connection to the hypervisor.
     * @param info Pointer to structure to be filled with node information.
     * @return int 0 on success, -1 on failure.
     */
    virtual int getNodeInfo(virConnectPtr conn, virNodeInfoPtr info) = 0;

    /**
     * @brief Gets the version of the libvirt library.
     *
     * @param conn Active connection to the hypervisor.
     * @param version Pointer to store the version number.
     * @return int 0 on success, -1 on failure.
     */
    virtual int getLibVersion(virConnectPtr conn, unsigned long* version) = 0;

    /**
     * @brief Gets the version of the hypervisor driver.
     *
     * @param conn Active connection to the hypervisor.
     * @param version Pointer to store the driver version number.
     * @return int 0 on success, -1 on failure.
     */
    virtual int getDriverVersion(virConnectPtr conn, unsigned long* version) = 0;

    /**
     * @brief Gets the connection URI.
     *
     * @param conn Active connection to the hypervisor.
     * @return std::string Connection URI string.
     */
    virtual std::string getConnectUrl(virConnectPtr conn) = 0;

    /**
     * @brief Gets the type of hypervisor driver.
     *
     * @param conn Active connection to the hypervisor.
     * @return std::string Driver type (e.g., "QEMU", "KVM").
     */
    virtual std::string getDriverType(virConnectPtr conn) = 0;

    /**
     * @brief Gets the last error message from libvirt.
     *
     * @return std::string Last error message.
     */
    virtual std::string getLastError() = 0;

    /**
     * @brief Looks up a domain by its name.
     *
     * @param conn Active connection to the hypervisor.
     * @param name Name of the domain to look up.
     * @return virDomainPtr Domain handle, or nullptr if not found.
     */
    virtual virDomainPtr domainLookupByName(virConnectPtr conn, std::string name) = 0;

    /**
     * @brief Gets information about a domain.
     *
     * @param domain Domain to get information from.
     * @param domainInfo Reference to structure to be filled with domain information.
     * @return int 0 on success, -1 on failure.
     */
    virtual int domainGetInfo(virDomainPtr domain, virDomainInfo& domainInfo) = 0;

    /**
     * @brief Gets the UUID of a domain.
     *
     * @param domain Domain to get UUID from.
     * @param uuid Reference to string to store the UUID.
     * @return int 0 on success, -1 on failure.
     */
    virtual int getDomainUUID(virDomainPtr domain, std::string& uuid) = 0;

    /**
     * @brief Gets a list of all domains on the connection.
     *
     * @param conn Active connection to the hypervisor.
     * @param domains Pointer to array that will be allocated and filled with domain handles.
     * @return int Number of domains found, or -1 on failure.
     */
    virtual int getListOfAllDomains(virConnectPtr conn, virDomainPtr** domains) = 0;

    /**
     * @brief Gets the name of a domain.
     *
     * @param domain Domain to get name from.
     * @return std::string Domain name.
     */
    virtual std::string getDomainName(virDomainPtr domain) = 0;

    /**
     * @brief Frees a domain handle.
     *
     * @param domain Domain handle to free.
     */
    virtual void freeDomain(virDomainPtr domain) = 0;

    /**
     * @brief Checks if a connection is alive.
     *
     * @param conn Connection to check.
     * @return int 1 if alive, 0 if not, -1 on error.
     */
    virtual int connectionIsAlive(virConnectPtr conn) = 0;

    /**
     * @brief Creates a new stream object.
     *
     * @param conn Active connection to the hypervisor.
     * @return virStreamPtr Stream handle, or nullptr on failure.
     */
    virtual virStreamPtr createNewStream(virConnectPtr conn) = 0;

    /**
     * @brief Opens a console connection to a domain.
     *
     * @param domain Domain to open console for.
     * @param stream Stream to use for console I/O.
     * @return int 0 on success, -1 on failure.
     */
    virtual int openDomainConsole(virDomainPtr domain, virStreamPtr stream) = 0;

    /**
     * @brief Looks up a domain by its UUID.
     *
     * @param conn Active connection to the hypervisor.
     * @param uuid UUID string of the domain to find.
     * @return virDomainPtr Domain handle, or nullptr if not found.
     */
    virtual virDomainPtr domainLookupByUuid(virConnectPtr conn, const std::string& uuid) = 0;

    /**
     * @brief Receives data from a stream.
     *
     * @param stream Active stream to read from.
     * @param buffer Buffer to store received data.
     * @param bufferSize Size of the buffer.
     * @return int Number of bytes read, 0 on EOF, -1 on error.
     */
    virtual int receiveDataFromStream(virStreamPtr stream, char* buffer, int bufferSize) = 0;

    /**
     * @brief Sends data to a stream.
     *
     * @param stream Active stream to write to.
     * @param buffer Data to send.
     * @param bufferSize Size of data to send.
     */
    virtual void sendDataToStream(virStreamPtr stream, const char* buffer, int bufferSize) = 0;

    /**
     * @brief Finishes and frees a stream.
     *
     * @param stream Stream to finish and free.
     */
    virtual void finishAndFreeStream(virStreamPtr stream) = 0;

    /**
     * @brief Creates a virtual network from XML definition.
     *
     * @param conn Active connection to the hypervisor.
     * @param networkDefinition XML string defining the network.
     * @return virNetworkPtr Network handle, or nullptr on failure.
     */
    virtual virNetworkPtr createNetworkFromXml(virConnectPtr conn, const std::string& networkDefinition) = 0;

    /**
     * @brief Attaches a device to a virtual machine.
     *
     * @param domain Domain to attach device to.
     * @param deviceDefinition XML string defining the device.
     * @return int 0 on success, -1 on failure.
     */
    virtual int attachDeviceToVm(virDomainPtr domain, const std::string& deviceDefinition) = 0;

    /**
     * @brief Detaches a device from a virtual machine.
     *
     * @param domain Domain to detach device from.
     * @param deviceDefinition XML string defining the device.
     * @return int 0 on success, -1 on failure.
     */
    virtual int detachDeviceFromVm(virDomainPtr domain, const std::string& deviceDefinition) = 0;

    /**
     * @brief Updates a device configuration in a virtual machine.
     *
     * @param domain Domain to update device in.
     * @param deviceDefinition XML string defining the new device configuration.
     * @return int 0 on success, -1 on failure.
     */
    virtual int updateVmDevice(virDomainPtr domain, const std::string& deviceDefinition) = 0;

    /**
     * @brief Gets a network by its name.
     *
     * @param conn Active connection to the hypervisor.
     * @param name Name of the network to find.
     * @return virNetworkPtr Network handle, or nullptr if not found.
     */
    virtual virNetworkPtr getNetworkByName(virConnectPtr conn, const std::string& name) = 0;

    /**
     * @brief Destroys (stops and removes) a virtual network.
     *
     * @param network Network to destroy.
     * @return int 0 on success, -1 on failure.
     */
    virtual int destroyNetwork(virNetworkPtr network) = 0;

    /**
     * @brief Gets the XML definition of a network.
     *
     * @param network Network to get definition from.
     * @return std::string XML definition of the network.
     */
    virtual std::string getNetworkDefinition(virNetworkPtr network) = 0;
};


#endif // ILIBVIRTWRAPPER_H
