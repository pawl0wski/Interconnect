#ifndef CONNECTIONINFO_H
#define CONNECTIONINFO_H
#include "Version.h"

struct ConnectionInfo {
    unsigned int cpuCount;
    unsigned int cpuFreq;
    unsigned long totalMemory;
    char connectionUrl[256];
    char driverType[256];
    Version libVersion;
    Version driverVersion;
};

#endif //CONNECTIONINFO_H
