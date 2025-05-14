#ifndef ILIBVIRTWRAPPER_H
#define ILIBVIRTWRAPPER_H
#include <libvirt/libvirt.h>


class ILibvirtWrapper {
public:
    virtual ~ILibvirtWrapper() = default;

    virtual virConnectPtr connectOpen(const char *name) = 0;
};


#endif // ILIBVIRTWRAPPER_H
