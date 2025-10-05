#ifndef SNIFFEREXCEPTION_H
#define SNIFFEREXCEPTION_H
#include <string>
#include <utility>

class PacketSnifferException final : public std::exception
{
    std::string message;

public:
    explicit PacketSnifferException(std::string  msg) : message(std::move(msg))
    {
    }

    [[nodiscard]] const char* what() const noexcept override
    {
        return message.c_str();
    }
};
#endif //SNIFFEREXCEPTION_H
