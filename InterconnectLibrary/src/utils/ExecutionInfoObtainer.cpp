#include "ExecutionInfoObtainer.h"
#include <string.h>

/**
 * @brief Executes a function and captures execution status information.
 * 
 * Runs the provided function within a try-catch block, capturing any exceptions
 * and storing error information in the ExecutionInfo structure.
 * 
 * @param executionInfo Pointer to ExecutionInfo structure that will be populated with:
 *                       - errorOccurred: boolean flag indicating if an exception was caught
 *                       - msg: error message if an exception occurred, empty otherwise
 * @param func A function or callable object to execute.
 */
void ExecutionInfoObtainer::runAndObtainExecutionInfo(ExecutionInfo* executionInfo,
                                                      const std::function<void()>& func)
{
    try
    {
        func();
    }
    catch (std::exception& e)
    {
        executionInfo->errorOccurred = true;
        strncpy(executionInfo->msg, e.what(), sizeof(executionInfo->msg) - 1);
        return;
    }

    executionInfo->errorOccurred = false;
    strncpy(executionInfo->msg, "\0", sizeof(executionInfo->msg) - 1);
}
