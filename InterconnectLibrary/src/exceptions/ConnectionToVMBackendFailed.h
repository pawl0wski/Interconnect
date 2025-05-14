#ifndef CONNECTIONTOVMBACKENDFAILED_H
#define CONNECTIONTOVMBACKENDFAILED_H
#include <exception>
#include <string>


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
