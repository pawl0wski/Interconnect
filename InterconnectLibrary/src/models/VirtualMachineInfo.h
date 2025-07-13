#ifndef VIRTUALMACHINEINFO_H
#define VIRTUALMACHINEINFO_H

/**
 * @brief Structure containing basic information about a virtual machine
 */
struct VirtualMachineInfo
{
    /**
    * @brief The identifier of the virtual machine in libvirt system
    */
    char uuid[128];
    unsigned long usedMemory;
    unsigned char state;
};

#endif //VIRTUALMACHINEINFO_H
