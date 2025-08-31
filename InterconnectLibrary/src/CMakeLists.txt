set(SOURCE_FILES
        virt/VirtualizationFacade.cpp
        virt/managers/BaseManagerWithConnection.cpp
        virt/managers/VirtualMachineManager.cpp
        virt/managers/VirtualMachineConsoleManager.cpp
        virt/managers/ConnectionManager.cpp
        virt/managers/VirtualNetworkManager.cpp
        LibvirtWrapper.cpp
        InterconnectLibraryExtern.cpp
        utils/VersionUtils.cpp
        utils/StringUtils.cpp
        utils/ExecutionInfoObtainer.cpp
)

set(HEADER_FILES
        interfaces/ILibvirtWrapper.h
        LibvirtWrapper.h
        virt/VirtualizationFacade.h
        virt/managers/BaseManagerWithConnection.h
        virt/managers/VirtualMachineManager.h
        virt/managers/VirtualMachineConsoleManager.h
        virt/managers/ConnectionManager.h
        virt/managers/VirtualNetworkManager.h
        models/VirtualMachineInfo.h
        models/Version.h
        models/ConnectionInfo.h
        models/ExecutionInfo.h
        models/StreamData.h
        exceptions/VirtualMachineManagerException.h
        utils/VersionUtils.h
        utils/StringUtils.h
        utils/ExecutionInfoObtainer.h
)

set(EXTERN_FILES
        InterconnectLibraryExtern.cpp)

add_library(interconnect STATIC
        ${HEADER_FILES}
        ${SOURCE_FILES}
        ${EXTERN_FILES}
        virt/managers/BaseManagerWithConnection.cpp
        virt/managers/BaseManagerWithConnection.h
)

add_library(interconnect_shared SHARED
        ${HEADER_FILES}
        ${SOURCE_FILES}
        ${EXTERN_FILES}
)

target_link_libraries(interconnect_shared
        ${LIBVIRT_LIBRARIES}
)

target_include_directories(interconnect PUBLIC
        ${CMAKE_CURRENT_SOURCE_DIR}
)
