namespace Models
{
    /// <summary>
    /// Information about a virtual machine console stream.
    /// </summary>
    public class StreamInfo
    {
        /// <summary>
        /// Unique UUID identifier of the virtual machine.
        /// </summary>
        public Guid Uuid { get; set; }
        
        /// <summary>
        /// Pointer to the native console stream.
        /// </summary>
        public IntPtr Stream { get; set; }
    }
}
