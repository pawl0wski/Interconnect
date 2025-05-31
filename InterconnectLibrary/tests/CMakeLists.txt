include(FetchContent)

enable_testing()

find_package(GTest REQUIRED)

add_executable(
        interconnect_unit_tests
        VirtualMachineManager.cpp
)

# Include Interconnect library
target_link_libraries(
        interconnect_unit_tests
        interconnect_library
)

target_link_libraries(
        interconnect_unit_tests
        GTest::gtest_main
        GTest::gmock
)

include(GoogleTest)
gtest_discover_tests(interconnect_unit_tests)