namespace Services.Utils
{
    public static class VirtualNetworkUtils
    {
        public static string GetNetworkNameFromUuid(Guid uuid) =>
            $"InterconnectNode-{uuid}";

        public static Guid GetNetworkUuidFromName(string networkName) =>
            Guid.Parse(networkName.Replace("InterconnectNode-", ""));
    }
}
