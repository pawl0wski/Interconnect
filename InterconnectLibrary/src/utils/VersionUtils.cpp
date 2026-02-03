#include "VersionUtils.h"

/**
 * @brief Extracts version components from a packed version number.
 * 
 * Decomposes a packed unsigned long version number into its major, minor, and patch
 * components. The encoding format is: MMMMMMNNNNNNPPPP where M=major, N=minor, P=patch.
 * 
 * @param version The packed version number (format: major*1000000 + minor*1000 + patch).
 * @return Version A Version object containing separated major, minor, and patch components.
 */
Version VersionUtils::getVersion(const unsigned long version) {
    return Version(version / 1000000, version % 1000000 / 1000, version % 1000);
}
