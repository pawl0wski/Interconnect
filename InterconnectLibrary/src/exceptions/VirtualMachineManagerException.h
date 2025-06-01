#ifndef LIBVIRTEXCEPTION_H
#define LIBVIRTEXCEPTION_H
#include <exception>
#include <string>

class VirtualMachineManagerException final : public std::exception {
    std::string message;

public:
    explicit VirtualMachineManagerException(const std::string &msg) : message(msg) {
    }

    const char *what() const noexcept override {
        return message.c_str();
    }
};
#endif //LIBVIRTEXCEPTION_H
