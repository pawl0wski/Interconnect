include(FetchContent)

FetchContent_Declare(
        googletest
        URL https://github.com/google/googletest/archive/6910c9d9165801d8827d628cb72eb7ea9dd538c5.zip
)

# For Windows: Prevent overriding the parent project's compiler/linker settings
set(gtest_force_shared_crt ON CACHE BOOL "" FORCE)
FetchContent_MakeAvailable(googletest)

add_executable(
        interconnect_unit_tests
        library_test.cpp
)

# Include Interconnect library
target_link_libraries(
        interconnect_unit_tests
        interconnect_library
)

target_link_libraries(
        interconnect_unit_tests
        GTest::gtest_main
)

include(GoogleTest)
gtest_discover_tests(interconnect_unit_tests)