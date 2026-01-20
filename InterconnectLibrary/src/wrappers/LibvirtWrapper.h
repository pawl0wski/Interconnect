#ifndef LIBVIRTWRAPPER_H
#define LIBVIRTWRAPPER_H
#include "../interfaces/ILibvirtWrapper.h"
#include <libvirt/virterror.h>

/**
 * @brief Concrete implementation of the ILibvirtWrapper interface.
 *
 * This class provides actual implementations for connecting to the libvirt C API.
 */
class LibvirtWrapper final : public ILibvirtWrapper
{
public:
    /**
     * @brief Opens a connection to the hypervisor.
     *
     * @param connectionUri The name or URI of the hypervisor to connect to.
     * @return virConnectPtr A pointer to the connection object on success.
     */
    virConnectPtr connectOpen(const char* connectionUri) override;

    /**
     * @brief Creates a virtual machine from an XML configuration.
     *
     * @param conn The active connection to the hypervisor.
     * @param xmlConfig The XML string describing the VM configuration.
     * @return virDomainPtr A pointer to the newly created virtual machine domain.
     */
    virDomainPtr createVirtualMachineFromXml(virConnectPtr conn, const char* xmlConfig) override;

    /**
     * @brief Retrieves the UUID of the given domain.
     *
     * @param domain The domain to get the UUID from.
     * @param uuid A buffer to store the resulting UUID string.
     */
    void getUuidFromDomain(virDomainPtr domain, char* uuid) override;

    /**
     * @brief Gets information about the physical host node.
     *
     * @param conn Active connection to the hypervisor.
     * @param info Pointer to structure to be filled with node information.
     * @return int 0 on success, -1 on failure.
     */
    int getNodeInfo(virConnectPtr conn, virNodeInfoPtr info) override;

    /**
     * @brief Gets the version of the libvirt library.
     *
     * @param conn Active connection to the hypervisor.
     * @param libVersion Pointer to store the version number.
     * @return int 0 on success, -1 on failure.
     */
    int getLibVersion(virConnectPtr conn, unsigned long* libVersion) override;

    /**
     * @brief Gets the version of the hypervisor driver.
     *
     * @param conn Active connection to the hypervisor.
     * @param version Pointer to store the driver version number.
     * @return int 0 on success, -1 on failure.
     */
    int getDriverVersion(virConnectPtr conn, unsigned long* version) override;

    /**
     * @brief Gets the connection URI.
     *
     * @param conn Active connection to the hypervisor.
     * @return std::string Connection URI string.
     */
    std::string getConnectUrl(virConnectPtr conn) override;

    /**
     * @brief Gets the type of hypervisor driver.
     *
     * @param conn Active connection to the hypervisor.
     * @return std::string Driver type (e.g., "QEMU", "KVM").
     */
    std::string getDriverType(virConnectPtr conn) override;

    /**
     * @brief Gets the last error message from libvirt.
     *
     * @return std::string Last error message.
     */
    std::string getLastError() override;

    /**
     * @brief Looks up a domain by its name.
     *
     * @param conn Active connection to the hypervisor.
     * @param name Name of the domain to look up.
     * @return virDomainPtr Domain handle, or nullptr if not found.
     */
    virDomainPtr domainLookupByName(virConnectPtr conn, std::string name) override;

    /**
     * @brief Gets information about a domain.
     *
     * @param domain Domain to get information from.
     * @param domainInfo Reference to structure to be filled with domain information.
     * @return int 0 on success, -1 on failure.
     */
    int domainGetInfo(virDomainPtr domain, virDomainInfo& domainInfo) override;

    /**
     * @brief Gets the UUID of a domain.
     *
     * @param domain Domain to get UUID from.
     * @param uuid Reference to string to store the UUID.
     * @return int 0 on success, -1 on failure.
     */
    int getDomainUUID(virDomainPtr domain, std::string& uuid) override;

    /**
     * @brief Gets a list of all domains on the connection.
     *
     * @param conn Active connection to the hypervisor.
     * @param domains Pointer to array that will be allocated and filled with domain handles.
     * @return int Number of domains found, or -1 on failure.
     */
    int getListOfAllDomains(virConnectPtr conn, virDomainPtr** domains) override;

    /**
     * @brief Gets the name of a domain.
     *
     * @param domain Domain to get name from.
     * @return std::string Domain name.
     */
    std::string getDomainName(virDomainPtr domain) override;

    /**
     * @brief Frees a domain handle.
     *
     * @param domain Domain handle to free.
     */
    void freeDomain(virDomainPtr domain) override;

    /**
     * @brief Checks if a connection is alive.
     *
     * @param conn Connection to check.
     * @return int 1 if alive, 0 if not, -1 on error.
     */
    int connectionIsAlive(virConnectPtr conn) override;

    /**
     * @brief Creates a new stream object.
     *
     * @param conn Active connection to the hypervisor.
     * @return virStreamPtr Stream handle, or nullptr on failure.
     */
    virStreamPtr createNewStream(virConnectPtr conn) override;

    /**
     * @brief Opens a console connection to a domain.
     *
     * @param domain Domain to open console for.
     * @param stream Stream to use for console I/O.
     * @return int 0 on success, -1 on failure.
     */
    int openDomainConsole(virDomainPtr domain, virStreamPtr stream) override;

    /**
     * @brief Looks up a domain by its UUID.
     *
     * @param conn Active connection to the hypervisor.
     * @param uuid UUID string of the domain to find.
     * @return virDomainPtr Domain handle, or nullptr if not found.
     */
    virDomainPtr domainLookupByUuid(virConnectPtr conn, const std::string& uuid) override;

    /**
     * @brief Receives data from a stream.
     *
     * @param stream Active stream to read from.
     * @param buffer Buffer to store received data.
     * @param bufferSize Size of the buffer.
     * @return int Number of bytes read, 0 on EOF, -1 on error.
     */
    int receiveDataFromStream(virStreamPtr stream, char* buffer, int bufferSize) override;

    /**
     * @brief Sends data to a stream.
     *
     * @param stream Active stream to write to.
     * @param buffer Data to send.
     * @param bufferSize Size of data to send.
     */
    void sendDataToStream(virStreamPtr stream, const char* buffer, int bufferSize) override;

    /**
     * @brief Finishes and frees a stream.
     *
     * @param stream Stream to finish and free.
     */
    void finishAndFreeStream(virStreamPtr stream) override;

    /**
     * @brief Creates a virtual network from XML definition.
     *
     * @param conn Active connection to the hypervisor.
     * @param networkDefinition XML string defining the network.
     * @return virNetworkPtr Network handle, or nullptr on failure.
     */
    virNetworkPtr createNetworkFromXml(virConnectPtr conn, const std::string& networkDefinition) override;

    /**
     * @brief Attaches a device to a virtual machine.
     *
     * @param domain Domain to attach device to.
     * @param deviceDefinition XML string defining the device.
     * @return int 0 on success, -1 on failure.
     */
    int attachDeviceToVm(virDomainPtr domain, const std::string& deviceDefinition) override;

    /**
     * @brief Detaches a device from a virtual machine.
     *
     * @param domain Domain to detach device from.
     * @param deviceDefinition XML string defining the device.
     * @return int 0 on success, -1 on failure.
     */
    int detachDeviceFromVm(virDomainPtr domain, const std::string& deviceDefinition) override;

    /**
     * @brief Updates a device configuration in a virtual machine.
     *
     * @param domain Domain to update device in.
     * @param deviceDefinition XML string defining the new device configuration.
     * @return int 0 on success, -1 on failure.
     */
    int updateVmDevice(virDomainPtr domain, const std::string& deviceDefinition) override;

    /**
     * @brief Gets a network by its name.
     *
     * @param conn Active connection to the hypervisor.
     * @param name Name of the network to find.
     * @return virNetworkPtr Network handle, or nullptr if not found.
     */
    virNetworkPtr getNetworkByName(virConnectPtr conn, const std::string& name) override;

    /**
     * @brief Destroys (stops and removes) a virtual network.
     *
     * @param network Network to destroy.
     * @return int 0 on success, -1 on failure.
     */
    int destroyNetwork(virNetworkPtr network) override;

    /**
     * @brief Gets the XML definition of a network.
     *
     * @param network Network to get definition from.
     * @return std::string XML definition of the network.
     */
    std::string getNetworkDefinition(virNetworkPtr network) override;
};


#endif //LIBVIRTWRAPPER_H
