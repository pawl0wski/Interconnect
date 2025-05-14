cmake_minimum_required(VERSION 3.22)
project(interconnect_library)

set(CMAKE_CXX_STANDARD 20)

find_package(PkgConfig REQUIRED)
pkg_check_modules(LIBVIRT REQUIRED libvirt)
pkg_check_modules(PCAP REQUIRED libpcap)


add_subdirectory(src)

target_include_directories(interconnect_library PRIVATE
        ${LIBVIRT_INCLUDE_DIRS}
        ${PCAP_INCLUDE_DIRS}
)

target_link_libraries(interconnect_library
        ${LIBVIRT_LIBRARIES}
        ${PCAP_LIBRARIES}
)
option(BUILD_TESTING "Build tests" OFF)
if (BUILD_TESTING)
    add_subdirectory(tests)
endif ()