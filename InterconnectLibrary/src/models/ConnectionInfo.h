#ifndef CONNECTIONINFO_H
#define CONNECTIONINFO_H
#include "Version.h"

/**
 * @brief Structure containing information about a hypervisor connection.
 *
 * This structure holds detailed information about the connection to a libvirt hypervisor,
 * including hardware resources and version information.
 */
struct ConnectionInfo {
    /**
     * @brief Number of CPUs available on the host.
     */
    unsigned int cpuCount;
    
    /**
     * @brief CPU frequency in MHz.
     */
    unsigned int cpuFreq;
    
    /**
     * @brief Total memory available on the host in bytes.
     */
    unsigned long totalMemory;
    
    /**
     * @brief Connection URL to the hypervisor.
     */
    char connectionUrl[256];
    
    /**
     * @brief Type of the hypervisor driver (e.g., QEMU, KVM).
     */
    char driverType[256];
    
    /**
     * @brief Version of the libvirt library.
     */
    Version libVersion;
    
    /**
     * @brief Version of the hypervisor driver.
     */
    Version driverVersion;
};

#endif //CONNECTIONINFO_H
