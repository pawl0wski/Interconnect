add_library(interconnect_library STATIC library.cpp)
add_library(interconnect_library_shared SHARED library.cpp)

target_include_directories(interconnect_library PUBLIC ${CMAKE_CURRENT_SOURCE_DIR})
