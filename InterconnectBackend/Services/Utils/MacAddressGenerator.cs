using System.Security.Cryptography;

namespace Services.Utils
{
    /// <summary>
    /// Utility class for generating random MAC addresses.
    /// </summary>
    public static class MacAddressGenerator
    {
        /// <summary>
        /// Generates a random MAC address with unicast and locally administered flags set.
        /// </summary>
        /// <returns>A MAC address in colon-separated hexadecimal format (e.g., "02:11:22:33:44:55").</returns>
        public static string Generate()
        {
            byte[] macBytes = new byte[6];
            RandomNumberGenerator.Fill(macBytes);

            macBytes[0] = (byte)(macBytes[0] & 0xFE);
            macBytes[0] = (byte)(macBytes[0] | 0x02);

            return string.Join(":", Array.ConvertAll(macBytes, b => b.ToString("X2")));
        }
    }
}
