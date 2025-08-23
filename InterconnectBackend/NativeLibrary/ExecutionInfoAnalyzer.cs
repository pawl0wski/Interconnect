using NativeLibrary.Structs;

namespace NativeLibrary
{
    internal static class ExecutionInfoAnalyzer
    {
        public static void ThrowIfErrorOccurred(NativeExecutionInfo executionInfo)
        {
            if (executionInfo.ErrorOccurred)
            {
                throw new Exception(executionInfo.Msg);
            }
        }
    }
}