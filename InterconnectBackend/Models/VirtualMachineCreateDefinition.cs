namespace Models
{
    /// <summary>
    /// Definition for creating a new virtual machine.
    /// </summary>
    public class VirtualMachineCreateDefinition
    {
        /// <summary>
        /// Virtual machine name.
        /// </summary>
        public required string Name { get; set; }
        
        /// <summary>
        /// Amount of RAM memory in MB.
        /// </summary>
        public required uint Memory { get; set; }
        
        /// <summary>
        /// Number of virtual CPUs.
        /// </summary>
        public required uint VirtualCpus { get; set; }
        
        /// <summary>
        /// Path to the bootable disk.
        /// </summary>
        public required string BootableDiskPath { get; set; }
    
        /// <summary>
        /// Generates a virtual machine name with a prefix.
        /// </summary>
        /// <param name="prefix">Prefix to add before the name.</param>
        /// <returns>Virtual machine name with prefix.</returns>
        public string GetVirtualMachineNameWithPrefix(string prefix)
        {
            return $"{prefix}_{Name}";
        }
    }
}
