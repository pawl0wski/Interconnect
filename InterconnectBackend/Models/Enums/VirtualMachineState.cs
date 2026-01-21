namespace Models.Enums
{
    /// <summary>
    /// State of the virtual machine.
    /// </summary>
    public enum VirtualMachineState
    {
        /// <summary>
        /// Unknown state.
        /// </summary>
        Unknown = 0,
        
        /// <summary>
        /// Virtual machine is running.
        /// </summary>
        Booted = 1,
    }
}
