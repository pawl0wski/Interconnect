#ifndef LIBRARY_LIBRARY_H
#define LIBRARY_LIBRARY_H
#include <optional>
#include <string>
#include <libvirt/libvirt.h>

class VirtualMachineManager {
    virConnectPtr connectPtr = nullptr;

public:
    void initializeConnection(const std::optional<std::string> &customConnectionPath);

    void createVirtualMachine();

    void getInfoAboutVirtualMachine();
};

int addTwoNumbers(int a, int b);

#endif //LIBRARY_LIBRARY_H
