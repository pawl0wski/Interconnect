add_library(citadel_library STATIC library.cpp)
add_library(citadel_library_shared SHARED library.cpp)

target_include_directories(citadel_library PUBLIC ${CMAKE_CURRENT_SOURCE_DIR})
