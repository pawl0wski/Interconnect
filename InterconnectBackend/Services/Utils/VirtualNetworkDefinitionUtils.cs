using System.Xml.Linq;

namespace Services.Utils
{
    public static class VirtualNetworkDefinitionUtils
    {
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
