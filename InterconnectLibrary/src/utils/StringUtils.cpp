#include "StringUtils.h"

#include <string>
#include <cstring>

const char* StringUtils::toConstCharPointer(const std::string& in)
{
    char* strPointer = new char[in.size() + 1];
    std::copy(in.begin(), in.end(), strPointer);
    strPointer[in.size()] = '\0';
    return strPointer;
}

void StringUtils::copyStringToCharArray(const std::string& src, char* charArray, const int length)
{
    if (length <= 0 || charArray == nullptr)
    {
        return;
    }

    strncpy(charArray, src.c_str(), length - 1);

    charArray[length - 1] = '\0';
}
