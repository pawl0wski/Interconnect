namespace Models
{
    /// <summary>
    /// Model containing operating system information.
    /// </summary>
    public class InformationModel
    {
        /// <summary>
        /// Operating system description.
        /// </summary>
        public required string OsDescription { get; set; }
        
        /// <summary>
        /// Operating system architecture.
        /// </summary>
        public required string OsArch { get; set; }
    }
}
