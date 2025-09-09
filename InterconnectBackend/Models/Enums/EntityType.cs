namespace Models.Enums
{
    public enum EntityType
    {
        VirtualMachine = 1,
        /// <summary>
        /// Used only in the frontend to connect two entities
        /// </summary>
        Network = 2,
        VirtualSwitch = 3,
        Internet = 4,
    }
}
