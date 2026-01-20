#ifndef STRINGTOCHARUTILS_H
#define STRINGTOCHARUTILS_H
#include <string>

/**
 * @brief Utility class for string manipulation operations.
 *
 * This class provides static helper methods for converting and copying strings.
 */
class StringUtils
{
public:
    /**
     * @brief Copies a std::string to a character array with specified length.
     *
     * This method safely copies a string to a fixed-size character array,
     * ensuring null-termination and preventing buffer overflow.
     *
     * @param src Source string to copy.
     * @param charArray Destination character array.
     * @param length Maximum length of the destination array including null terminator.
     */
    static void copyStringToCharArray(const std::string& src, char* charArray, int length);
};


#endif //STRINGTOCHARUTILS_H
