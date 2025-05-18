cmake_minimum_required(VERSION 3.22)
project(interconnect)

set(CMAKE_CXX_STANDARD 20)

find_package(PkgConfig REQUIRED)
pkg_check_modules(LIBVIRT REQUIRED libvirt)
pkg_check_modules(PCAP REQUIRED libpcap)


add_subdirectory(src)

target_include_directories(interconnect PRIVATE
        ${LIBVIRT_INCLUDE_DIRS}
        ${PCAP_INCLUDE_DIRS}
)

target_link_libraries(interconnect
        ${LIBVIRT_LIBRARIES}
        ${PCAP_LIBRARIES}
)

option(BUILD_TESTING "Build tests" OFF)
if (BUILD_TESTING)
    add_subdirectory(tests)
endif ()