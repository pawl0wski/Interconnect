namespace Services.Utils
{
    /// <summary>
    /// Utility class for virtual network name and UUID conversions.
    /// </summary>
    public static class VirtualNetworkUtils
    {
        /// <summary>
        /// Generates a network name from a UUID.
        /// </summary>
        /// <param name="uuid">The UUID to convert to a network name.</param>
        /// <returns>Network name in the format "InterconnectNode-{uuid}".</returns>
        public static string GetNetworkNameFromUuid(Guid uuid) =>
            $"InterconnectNode-{uuid}";

        /// <summary>
        /// Extracts a UUID from a network name.
        /// </summary>
        /// <param name="networkName">The network name in the format "InterconnectNode-{uuid}".</param>
        /// <returns>The extracted UUID.</returns>
        public static Guid GetNetworkUuidFromName(string networkName) =>
            Guid.Parse(networkName.Replace("InterconnectNode-", ""));
    }
}
