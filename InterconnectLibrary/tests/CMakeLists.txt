include(FetchContent)

enable_testing()

find_package(GTest REQUIRED)

# For Windows: Prevent overriding the parent project's compiler/linker settings
set(gtest_force_shared_crt ON CACHE BOOL "" FORCE)

add_executable(
        interconnect_unit_tests
        VirtualMachineManager.cpp
        mocks/VirtualMachineManagerMockGetInfoAboutVirtualMachine.h
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