#ifndef EXECUTIONINFO_H
#define EXECUTIONINFO_H

/**
 * @brief Structure containing execution result information.
 *
 * This structure is used to return status and error information from function executions.
 */
struct ExecutionInfo
{
    /**
     * @brief Flag indicating whether an error occurred during execution.
     */
    bool errorOccurred;
    
    /**
     * @brief Error or status message describing the execution result.
     */
    char msg[128];
};

#endif // EXECUTIONINFO_H
