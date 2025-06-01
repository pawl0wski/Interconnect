#ifndef STRINGTOCHARUTILS_H
#define STRINGTOCHARUTILS_H
#include <string_view>

class StringUtils {
public:
    static const char *toConstCharPointer(const std::string &in);
};


#endif //STRINGTOCHARUTILS_H
