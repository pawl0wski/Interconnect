#ifndef TESTINGUTILS_H
#define TESTINGUTILS_H
#include <functional>
#include <string>


class TestingUtils
{
public:
    static void expectThrowWithMessage(const std::function<void()>& func, const std::string& expectedMessage);
};


#endif //TESTINGUTILS_H
