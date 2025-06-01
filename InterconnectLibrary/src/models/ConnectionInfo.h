#ifndef CONNECTIONINFO_H
#define CONNECTIONINFO_H
#include "Version.h"

struct ConnectionInfo {
    unsigned int cpuCount;
    unsigned int cpuFreq;
    unsigned long totalMemory;
    const char* connectionUrl;
    const char* driverType;
    Version libVersion;
    Version driverVersion;
};

#endif //CONNECTIONINFO_H
