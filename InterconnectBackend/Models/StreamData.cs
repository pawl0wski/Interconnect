namespace Models
{
    /// <summary>
    /// Represents data read from a stream.
    /// </summary>
    public class StreamData
    {
        /// <summary>
        /// Data read from the stream.
        /// </summary>
        public required byte[] Data { get; set; }
        
        /// <summary>
        /// Indicates whether the stream has been broken.
        /// </summary>
        public bool IsStreamBroken { get; set; }
    }
}
