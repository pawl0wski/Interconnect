using NativeLibrary.Structs;

namespace NativeLibrary
{
    /// <summary>
    /// Analyzer for native operation execution information.
    /// </summary>
    internal static class ExecutionInfoAnalyzer
    {
        /// <summary>
        /// Throws an exception if an error occurred during native operation execution.
        /// </summary>
        /// <param name="executionInfo">Information about the operation execution.</param>
        /// <exception cref="Exception">Thrown when an error occurred.</exception>
        public static void ThrowIfErrorOccurred(NativeExecutionInfo executionInfo)
        {
            if (executionInfo.ErrorOccurred)
            {
                throw new Exception(executionInfo.Msg);
            }
        }
    }
}