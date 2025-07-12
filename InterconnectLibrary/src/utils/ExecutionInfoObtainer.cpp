#include "ExecutionInfoObtainer.h"
#include <string.h>

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
