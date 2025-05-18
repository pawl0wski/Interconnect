#ifndef VIRTUALMACHINEINFO_H
#define VIRTUALMACHINEINFO_H
#include <string>

/**
 * @brief Structure containing basic information about a virtual machine
 */
struct VirtualMachineInfo {
    /**
    * @brief The identifier of the virtual machine in libvirt system
    */
    std::string uuid;
};

#endif //VIRTUALMACHINEINFO_H
