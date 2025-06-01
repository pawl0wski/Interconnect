#include "StringUtils.h"

#include <string>

const char *StringUtils::toConstCharPointer(const std::string &in) {
    char *strPointer = new char[in.size() + 1];
    std::copy(in.begin(), in.end(), strPointer);
    strPointer[in.size()] = '\0';
    return strPointer;
}
