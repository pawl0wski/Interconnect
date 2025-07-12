#ifndef EXECUTIONINFOOBTAINER_H
#define EXECUTIONINFOOBTAINER_H
#include <functional>

#include "../models/ExecutionInfo.h"

class ExecutionInfoObtainer
{
public:
    static void runAndObtainExecutionInfo(ExecutionInfo* executionInfo, const std::function<void()>& func);
};

#endif // EXECUTIONINFOOBTAINER_H
