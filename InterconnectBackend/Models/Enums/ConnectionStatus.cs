namespace Models.Enums
{
    /// <summary>
    /// Connection status with the hypervisor.
    /// </summary>
    public enum ConnectionStatus
    {
        /// <summary>
        /// Connection is active.
        /// </summary>
        ALIVE = 1,
        
        /// <summary>
        /// Connection is inactive.
        /// </summary>
        DEAD = 0
    }
}
