#include <gtest/gtest.h>
#include "library.h"

TEST(AddTwoNumberTest, ShouldAddTwoNumbers) {
    EXPECT_EQ(addTwoNumbers(2, 5), 7);
}
