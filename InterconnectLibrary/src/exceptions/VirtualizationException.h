#ifndef VIRTUALIZATIONEXCEPTION_H
#define VIRTUALIZATIONEXCEPTION_H
#include <exception>
#include <string>
#include <utility>

class VirtualizationException final : public std::exception {
    std::string message;

public:
    explicit VirtualizationException(std::string msg) : message(std::move(msg)) {
    }

    [[nodiscard]] const char *what() const noexcept override {
        return message.c_str();
    }
};
#endif //VIRTUALIZATIONEXCEPTION_H
