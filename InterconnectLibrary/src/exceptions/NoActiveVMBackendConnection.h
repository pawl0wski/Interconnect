#ifndef NOACTIVEVMBACKENDCONNECTION_H
#define NOACTIVEVMBACKENDCONNECTION_H
#include <string>

/**
 * @brief Exception thrown when there is no active connection to the virtual machine backend.
 *
 * This exception is thrown when there is no active connection to the VM backend.
 * To avoid this error, a new connection must be established beforehand.
 */
class NoActiveVMBackendConnection final : public std::exception {
    std::string message;

public:
    explicit NoActiveVMBackendConnection(const std::string &msg) : message(msg) {
    }

    const char *what() const noexcept override {
        return message.c_str();
    }
};

#endif //NOACTIVEVMBACKENDCONNECTION_H
