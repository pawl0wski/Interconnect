#ifndef VIRTUALIZATIONEXCEPTION_H
#define VIRTUALIZATIONEXCEPTION_H
#include <exception>
#include <string>
#include <utility>

/**
 * @brief Exception class for virtualization-related errors.
 *
 * This exception is thrown when errors occur during virtualization operations
 */
class VirtualizationException final : public std::exception {
    std::string message;

public:
    /**
     * @brief Constructs a VirtualizationException with a descriptive message.
     *
     * @param msg Error message describing the exception.
     */
    explicit VirtualizationException(std::string msg) : message(std::move(msg)) {
    }

    /**
     * @brief Returns the explanatory error message.
     *
     * @return const char* Pointer to the error message string.
     */
    [[nodiscard]] const char *what() const noexcept override {
        return message.c_str();
    }
};
#endif //VIRTUALIZATIONEXCEPTION_H
