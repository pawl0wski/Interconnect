#include "StringUtils.h"

#include <string>
#include <cstring>

void StringUtils::copyStringToCharArray(const std::string& src, char* charArray, const int length)
{
    if (length <= 0 || charArray == nullptr)
    {
        return;
    }

    strncpy(charArray, src.c_str(), length - 1);

    charArray[length - 1] = '\0';
}
