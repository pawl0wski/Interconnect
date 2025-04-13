cmake_minimum_required(VERSION 3.30)
project(interconnect_library)

set(CMAKE_CXX_STANDARD 20)

add_subdirectory(src)

option(BUILD_TESTING "Build tests" OFF)
if (BUILD_TESTING)
    enable_testing()
    add_subdirectory(tests)
endif ()