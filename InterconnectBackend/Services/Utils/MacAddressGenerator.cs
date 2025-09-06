using System.Security.Cryptography;

namespace Services.Utils
{
    public static class MacAddressGenerator
    {
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
