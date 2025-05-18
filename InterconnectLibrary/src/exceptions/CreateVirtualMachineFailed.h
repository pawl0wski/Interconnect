#ifndef CREATEVIRTUALMACHINEFAILED_H
#define CREATEVIRTUALMACHINEFAILED_H
#include <exception>

/**
 * @brief Exception thrown when a virtual machine cannot be created.
 *
 * This exception is thrown when a virtual machine cannot be created
 * despite a successful connection to the virtual machine backend.
 */
class CreateVirtualMachineFailed : public std::exception {
    std::string message;

public:
    explicit CreateVirtualMachineFailed(const std::string &msg) : message(msg) {
    }

    const char *what() const noexcept override {
        return message.c_str();
    }
};

#endif //CREATEVIRTUALMACHINEFAILED_H
