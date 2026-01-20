#ifndef NETWORKDEFINITION_H
#define NETWORKDEFINITION_H

/**
 * @brief Structure containing network definition in XML format.
 *
 * This structure holds the XML configuration for defining virtual networks.
 */
struct NetworkDefinition
{
    /**
     * @brief XML content defining the network configuration.
     */
    char content[4096];
};

#endif
