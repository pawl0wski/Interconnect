#include "StringUtils.h"

#include <string>
#include <cstring>

/**
 * @brief Copies a C++ string to a fixed-size character array.
 * 
 * Safely copies the contents of a std::string to a C-style character array,
 * ensuring null-termination and preventing buffer overflow.
 * 
 * @param src The source std::string to copy from.
 * @param charArray The destination character array.
 * @param length The size of the destination array (including space for null terminator).
 */
void StringUtils::copyStringToCharArray(const std::string& src, char* charArray, const int length)
{
    if (length <= 0 || charArray == nullptr)
    {
        return;
    }

    strncpy(charArray, src.c_str(), length - 1);

    charArray[length - 1] = '\0';
}
