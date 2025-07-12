#include "utils/VersionUtils.h"

#include <gmock/gmock-matchers.h>
#include <gtest/gtest.h>

class VersionUtilsTests : public testing::Test {
protected:
    VersionUtilsTests() = default;
};

TEST_F(VersionUtilsTests, getVersion_WhenLongProvided_ShouldReturnVersionStruct) {
    const auto version = VersionUtils::getVersion(1222333);

    EXPECT_THAT(version, ::testing::Field(&Version::major, 1));
    EXPECT_THAT(version, ::testing::Field(&Version::minor, 222));
    EXPECT_THAT(version, ::testing::Field(&Version::patch, 333));
}
