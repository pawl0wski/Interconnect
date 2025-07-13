#ifndef STRINGTOCHARUTILS_H
#define STRINGTOCHARUTILS_H
#include <string>

class StringUtils
{
public:
    static const char* toConstCharPointer(const std::string& in);
    static void copyStringToCharArray(const std::string& src, char* charArray, int length);
};


#endif //STRINGTOCHARUTILS_H
