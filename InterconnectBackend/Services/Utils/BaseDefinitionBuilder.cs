using System.Xml;

namespace Services.Utils
{
    public abstract class BaseDefinitionBuilder<TDefinition>
    {
        protected delegate void BuildingBlock(XmlWriter writer);
        public abstract BaseDefinitionBuilder<TDefinition> SetFromCreateDefinition(TDefinition definition);
        public abstract string Build();
        protected void CheckIsEverythingIsProvided(params object?[] args)
        {
            foreach (var arg in args)
            {
                if (arg is null)
                {
                    throw new Exception("There are not all the things needed to build the XML definition for the virtual machine");
                }
            }
        }
        protected (XmlWriter, StringWriter) CreateXmlWriter()
        {
            var stringWriter = new StringWriter();
            var writer = XmlWriter.Create(stringWriter, new XmlWriterSettings
            {
                OmitXmlDeclaration = true,
                Indent = true
            });

            return (writer, stringWriter);
        }
    }
}
