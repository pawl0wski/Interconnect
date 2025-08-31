include(FetchContent)

enable_testing()

find_package(GTest REQUIRED)

set(TEST_FILES
        ConnectionManagerTests.cpp
        VirtualMachineConsoleManager.cpp
        VirtualMachineManagerTests.cpp
        VirtualNetworkManagerTests.cpp
        utils/VersionUtilsTests.cpp
        utils/StringUtilsTests.cpp
        utils/ExecutionInfoObtainerTests.cpp
)

set(MOCK_FILES
        mocks/VirtualMachineManagerMockGetInfoAboutVirtualMachine.h
        mocks/LibvirtWrapperMock.h
)

set(UTIL_FILES
        TestingUtils.cpp
        TestingUtils.h
)

add_executable(
        interconnect_unit_tests
        ${UTIL_FILES}
        ${MOCK_FILES}
        ${TEST_FILES}
)

# Include Interconnect library
target_link_libraries(
        interconnect_unit_tests interconnect
)

target_link_libraries(
        interconnect_unit_tests
        GTest::gtest_main
        GTest::gmock
)

include(GoogleTest)
gtest_discover_tests(interconnect_unit_tests)