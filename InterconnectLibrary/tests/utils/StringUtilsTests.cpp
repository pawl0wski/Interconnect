#include <gmock/gmock.h>
#include "utils/StringUtils.h"

class StringUtilsTests : public testing::Test
{
protected:
    StringUtilsTests() = default;
};

TEST_F(StringUtilsTests, copyStringToCharArray_WhenInvoked_ShouldCopyStringToCharArray)
{
    const std::string source = "test string";
    char dest[64];

    StringUtils::copyStringToCharArray(source, dest, sizeof(dest));

    EXPECT_STREQ(dest, "test string");
}
