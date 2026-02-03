using System.Xml.Linq;

namespace Services.Utils
{
    /// <summary>
    /// Utility class for extracting information from virtual network definitions.
    /// </summary>
    public static class VirtualNetworkDefinitionUtils
    {
        /// <summary>
        /// Extracts the network name from a network interface definition XML.
        /// </summary>
        /// <param name="definition">The network interface definition XML string.</param>
        /// <returns>The network name if found; otherwise null.</returns>
        public static string? GetNetworkNameFromDefinition(string definition)
        {
            XElement? interfaceElement = XElement.Parse(definition);

            XElement? sourceElement = interfaceElement.Element("source");

            if (sourceElement != null)
            {
                return sourceElement.Attribute("network")?.Value;
            }

            return null;
        }
    }
}
