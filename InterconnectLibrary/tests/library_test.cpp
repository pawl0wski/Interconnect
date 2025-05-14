#include <gtest/gtest.h>
#include "virtual_machine_manager.h"

TEST(AddTwoNumberTest, ShouldAddTwoNumbers) {
    EXPECT_EQ(addTwoNumbers(2, 5), 7);
}
