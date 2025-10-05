set(SOURCE_FILES
        VirtualizationLibraryExtern.cpp
        packetsniffer/PacketSniffer.cpp
        utils/ExecutionInfoObtainer.cpp
        utils/StringUtils.cpp
        utils/VersionUtils.cpp
        virt/VirtualizationFacade.cpp
        virt/managers/BaseManagerWithConnection.cpp
        virt/managers/ConnectionManager.cpp
        virt/managers/VirtualMachineConsoleManager.cpp
        virt/managers/VirtualMachineManager.cpp
        virt/managers/VirtualNetworkManager.cpp
        wrappers/LibpcapWrapper.cpp
        wrappers/LibvirtWrapper.cpp
)

set(HEADER_FILES
        exceptions/PacketSnifferException.h
        exceptions/VirtualizationException.h
        interfaces/ILibpcapWrapper.h
        interfaces/ILibvirtWrapper.h
        models/ConnectionInfo.h
        models/ExecutionInfo.h
        models/Packet.h
        models/StreamData.h
        models/Version.h
        models/VirtualMachineInfo.h
        packetsniffer/PacketSniffer.h
        utils/ExecutionInfoObtainer.h
        utils/StringUtils.h
        utils/VersionUtils.h
        virt/VirtualizationFacade.h
        virt/managers/BaseManagerWithConnection.h
        virt/managers/ConnectionManager.h
        virt/managers/VirtualMachineConsoleManager.h
        virt/managers/VirtualMachineManager.h
        virt/managers/VirtualNetworkManager.h
        wrappers/LibpcapWrapper.h
        wrappers/LibvirtWrapper.h
)

set(EXTERN_FILES
        VirtualizationLibraryExtern.cpp
        PacketSnifferLibraryExtern.cpp)

add_library(interconnect STATIC
        ${HEADER_FILES}
        ${SOURCE_FILES}
        ${EXTERN_FILES}
        models/ListenCallbackArgs.h
)

add_library(interconnect_shared SHARED
        ${HEADER_FILES}
        ${SOURCE_FILES}
        ${EXTERN_FILES}
)

target_link_libraries(interconnect_shared
        ${LIBVIRT_LIBRARIES}
        ${PCAP_LIBRARIES}
)

target_include_directories(interconnect PUBLIC
        ${CMAKE_CURRENT_SOURCE_DIR}
)
