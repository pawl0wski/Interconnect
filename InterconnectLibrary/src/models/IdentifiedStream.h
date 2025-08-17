#ifndef IDENTIFIEDSTREAM_H
#define IDENTIFIEDSTREAM_H
#include <libvirt/libvirt-host.h>

struct IdentifiedStream
{
    int id;
    virStreamPtr stream;
};

#endif //IDENTIFIEDSTREAM_H
