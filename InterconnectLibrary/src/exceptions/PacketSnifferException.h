#ifndef SNIFFEREXCEPTION_H
#define SNIFFEREXCEPTION_H
#include <string>
#include <utility>

/**
 * @brief Exception class for packet sniffer-related errors.
 *
 * This exception is thrown when errors occur during packet capture operations.
 */
class PacketSnifferException final : public std::exception
{
    std::string message;

public:
    /**
     * @brief Constructs a PacketSnifferException with a descriptive message.
     *
     * @param msg Error message describing the exception.
     */
    explicit PacketSnifferException(std::string  msg) : message(std::move(msg))
    {
    }

    /**
     * @brief Returns the explanatory error message.
     *
     * @return const char* Pointer to the error message string.
     */
    [[nodiscard]] const char* what() const noexcept override
    {
        return message.c_str();
    }
};
#endif //SNIFFEREXCEPTION_H
