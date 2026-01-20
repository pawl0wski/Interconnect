#ifndef EXECUTIONINFOOBTAINER_H
#define EXECUTIONINFOOBTAINER_H
#include <functional>

#include "../models/ExecutionInfo.h"

/**
 * @brief Utility class for executing functions and capturing execution information.
 *
 * This class provides a wrapper for running functions while automatically capturing
 * any exceptions and converting them to ExecutionInfo structures.
 */
class ExecutionInfoObtainer
{
public:
    /**
     * @brief Executes a function and captures execution result information.
     *
     * This method runs the provided function and populates an ExecutionInfo structure
     * with the result. If the function throws an exception, the error flag is set
     * and the exception message is captured.
     *
     * @param executionInfo Pointer to ExecutionInfo structure to be filled with results.
     * @param func Function to execute.
     */
    static void runAndObtainExecutionInfo(ExecutionInfo* executionInfo, const std::function<void()>& func);
};

#endif // EXECUTIONINFOOBTAINER_H
