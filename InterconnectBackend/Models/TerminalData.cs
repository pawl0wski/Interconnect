namespace Models
{
    /// <summary>
    /// Virtual machine terminal data.
    /// </summary>
    public class TerminalData
    {
        /// <summary>
        /// UUID of the virtual machine.
        /// </summary>
        public required string Uuid { get; set; }
        
        /// <summary>
        /// Terminal data in text format.
        /// </summary>
        public required string Data { get; set; }
    }
}
