add_library(interconnect_library STATIC virtual_machine_manager.cpp
        exceptions/ConnectionToVMBackendFailed.cpp
        exceptions/ConnectionToVMBackendFailed.h)
add_library(interconnect_library_shared SHARED virtual_machine_manager.cpp)

target_include_directories(interconnect_library PUBLIC ${CMAKE_CURRENT_SOURCE_DIR})
