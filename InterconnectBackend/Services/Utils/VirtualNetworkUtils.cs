namespace Services.Utils
{
    public static class VirtualNetworkUtils
    {
        public static string GetNetworkNameFromUuid(Guid uuid) =>
            $"InterconnectSwitch-{uuid}";

        public static Guid GetNetworkUuidFromName(string networkName) =>
            Guid.Parse(networkName.Replace("InterconnectSwitch-", ""));
    }
}
