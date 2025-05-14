add_library(interconnect_library STATIC VirtualMachineManager.cpp
        exceptions/ConnectionToVMBackendFailed.cpp
        exceptions/ConnectionToVMBackendFailed.h
        interfaces/ILibvirtWrapper.h
        LibvirtWrapper.cpp
        LibvirtWrapper.h)

add_library(interconnect_library_shared SHARED VirtualMachineManager.cpp
        exceptions/ConnectionToVMBackendFailed.cpp
        exceptions/ConnectionToVMBackendFailed.h
        interfaces/ILibvirtWrapper.h
        LibvirtWrapper.cpp
        LibvirtWrapper.h)

target_include_directories(interconnect_library PUBLIC ${CMAKE_CURRENT_SOURCE_DIR})
