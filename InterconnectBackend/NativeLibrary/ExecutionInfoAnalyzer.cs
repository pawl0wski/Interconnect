using Library.Models;

namespace Library
{
    public static class ExecutionInfoAnalyzer
    {
        public static void ThrowIfErrorOccurred(ExecutionInfo executionInfo)
        {
            if (executionInfo.ErrorOccurred)
            {
                throw new Exception(executionInfo.Msg);
            }
        }
    }
}