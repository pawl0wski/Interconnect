#include <gmock/gmock.h>
#include "utils/StringUtils.h"

class StringUtilsTests : public testing::Test
{
protected:
    StringUtilsTests() = default;
};

TEST_F(StringUtilsTests, toConstCharPointer_WhenInvoked_ShouldReturnStringConstCharPtr)
{
    const auto ccStr = StringUtils::toConstCharPointer("Test TEST test");

    EXPECT_STREQ(ccStr, "Test TEST test");
}
