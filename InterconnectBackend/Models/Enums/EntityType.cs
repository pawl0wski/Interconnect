namespace Models.Enums
{
    /// <summary>
    /// Type of entity in a virtual network.
    /// </summary>
    public enum EntityType
    {
        /// <summary>
        /// Virtual machine entity.
        /// </summary>
        VirtualMachine = 1,
        
        /// <summary>
        /// Network - used only in frontend for connecting two entities.
        /// </summary>
        Network = 2,
        
        /// <summary>
        /// Virtual network node (switch/router).
        /// </summary>
        VirtualNetworkNode = 3,
        
        /// <summary>
        /// Internet entity.
        /// </summary>
        Internet = 4,
    }
}
