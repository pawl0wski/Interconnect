#include <gtest/gtest.h>
#include <gmock/gmock-matchers.h>
#include "utils/ExecutionInfoObtainer.h"

class ExecutionInfoObtainerTests : public testing::Test
{
protected:
    ExecutionInfoObtainerTests() = default;
};

TEST_F(ExecutionInfoObtainerTests,
       runAndObtainExecutionInfo_WhenInvokedWithFunctionThatNotThrowsException_ShouldReturnExecutionInfoWithoutError)
{
    ExecutionInfo executionInfo;
    const auto mockFunc = []
    {
    };

    ExecutionInfoObtainer::runAndObtainExecutionInfo(&executionInfo, mockFunc);

    EXPECT_THAT(executionInfo, ::testing::Field(&ExecutionInfo::errorOccurred, ::testing::Eq(false)));
}

TEST_F(ExecutionInfoObtainerTests,
       runAndObtainExecutionInfo_WhenInvokedWithFunctionThatThrowsException_ShouldReturnExecutionInfoWithError)
{
    ExecutionInfo executionInfo;
    const auto mockFunc = []
    {
        throw std::runtime_error("TestException");
    };

    ExecutionInfoObtainer::runAndObtainExecutionInfo(&executionInfo, mockFunc);

    EXPECT_THAT(executionInfo, ::testing::Field(&ExecutionInfo::errorOccurred, ::testing::Eq(true)));
    EXPECT_THAT(executionInfo, ::testing::Field(&ExecutionInfo::msg, ::testing::StrEq("TestException")));
}
