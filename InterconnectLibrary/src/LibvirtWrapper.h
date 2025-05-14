#ifndef LIBVIRTWRAPPER_H
#define LIBVIRTWRAPPER_H
#include "interfaces/ILibvirtWrapper.h"


class LibvirtWrapper : public ILibvirtWrapper {
public:
    virConnectPtr connectOpen(const char *name) override;
};


#endif //LIBVIRTWRAPPER_H
