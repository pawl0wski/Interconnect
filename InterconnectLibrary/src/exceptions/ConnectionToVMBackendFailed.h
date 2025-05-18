#ifndef CONNECTIONTOVMBACKENDFAILED_H
#define CONNECTIONTOVMBACKENDFAILED_H
#include <exception>
#include <string>


/**
* @brief Exception thrown when an error occurs while connecting to the VM backend.
*
* This exception is thrown when it is not possible to connect
* to the virtual machine backend, e.g., due to incorrect
* configuration or the backend not running.
*/
class ConnectionToVMBackendFailed : public std::exception {
    std::string message;

public:
    explicit ConnectionToVMBackendFailed(const std::string &msg) : message(msg) {
    }

    const char *what() const noexcept override {
        return message.c_str();
    }
};


#endif //CONNECTIONTOVMBACKENDFAILED_H
