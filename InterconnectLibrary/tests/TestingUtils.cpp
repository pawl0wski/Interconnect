#include "TestingUtils.h"

#include <gtest/gtest.h>

void TestingUtils::expectThrowWithMessage(const std::function<void()>& func, const std::string& expectedMessage)
{
    try
    {
        func();
        FAIL() << "Expected an exception";
    }
    catch (const std::exception& err)
    {
        EXPECT_STREQ(expectedMessage.c_str(), err.what());
    }
}
