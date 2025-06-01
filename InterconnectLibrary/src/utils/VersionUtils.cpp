#include "VersionUtils.h"

Version VersionUtils::getVersion(const unsigned long version) {
    return Version(version / 1000000, version % 1000000 / 1000, version % 1000);
}
