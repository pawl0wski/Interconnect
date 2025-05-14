#include "LibvirtWrapper.h"

virConnectPtr LibvirtWrapper::connectOpen(const char *name) {
    return virConnectOpen(name);
}
