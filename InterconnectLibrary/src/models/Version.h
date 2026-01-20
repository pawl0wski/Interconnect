#ifndef VERSION_H
#define VERSION_H

/**
 * @brief Structure representing a semantic version number.
 *
 * This structure follows the semantic versioning convention (MAJOR.MINOR.PATCH).
 */
struct Version {
    /**
     * @brief Major version number.
     */
    unsigned int major;
    
    /**
     * @brief Minor version number.
     */
    unsigned int minor;
    
    /**
     * @brief Patch version number.
     */
    unsigned int patch;
};

#endif //VERSION_H
