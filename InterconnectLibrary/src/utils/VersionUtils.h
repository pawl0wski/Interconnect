#ifndef VERSIONUTILS_H
#define VERSIONUTILS_H
#include "../models/Version.h"

/**
 * @brief Utility class for version number operations.
 *
 * This class provides static helper methods for parsing and converting version numbers.
 */
class VersionUtils {
public:
    /**
     * @brief Converts a packed version number to a Version structure.
     *
     * This method decodes a libvirt-style packed version number (unsigned long)
     * into separate major, minor, and patch components.
     *
     * @param version Packed version number (e.g., from libvirt API).
     * @return Version Structure containing major, minor, and patch version numbers.
     */
    static Version getVersion(unsigned long version);
};

#endif //VERSIONUTILS_H
