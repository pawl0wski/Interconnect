set(SOURCE_FILES
        VirtualMachineManager.cpp
        LibvirtWrapper.cpp
        InterconnectLibraryExtern.cpp
        utils/VersionUtils.cpp
        utils/StringUtils.cpp
)

set(HEADER_FILES
        interfaces/ILibvirtWrapper.h
        LibvirtWrapper.h
        VirtualMachineManager.h
        models/VirtualMachineInfo.h
        models/Version.h
        models/ConnectionInfo.h
        exceptions/VirtualMachineManagerException.h
        utils/VersionUtils.h
        utils/StringUtils.h
)

set(EXTERN_FILES
        InterconnectLibraryExtern.cpp)

add_library(interconnect STATIC
        ${HEADER_FILES}
        ${SOURCE_FILES}
        ${EXTERN_FILES}
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
