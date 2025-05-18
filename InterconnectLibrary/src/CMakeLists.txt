add_library(interconnect_library STATIC VirtualMachineManager.cpp
        exceptions/ConnectionToVMBackendFailed.h
        exceptions/NoActiveVMBackendConnection.h
        exceptions/CreateVirtualMachineFailed.h
        interfaces/ILibvirtWrapper.h
        LibvirtWrapper.cpp
        LibvirtWrapper.h
        models/VirtualMachineInfo.h)

add_library(interconnect_library_shared SHARED VirtualMachineManager.cpp
        exceptions/ConnectionToVMBackendFailed.h
        interfaces/ILibvirtWrapper.h
        LibvirtWrapper.cpp
        LibvirtWrapper.h
        exceptions/NoActiveVMBackendConnection.h
        exceptions/CreateVirtualMachineFailed.h
        models/VirtualMachineInfo.h)

target_include_directories(interconnect_library PUBLIC ${CMAKE_CURRENT_SOURCE_DIR})
