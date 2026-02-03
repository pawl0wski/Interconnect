using System.Xml;

namespace Services.Utils
{
    /// <summary>
    /// Base class for building XML definitions from model definitions.
    /// </summary>
    /// <typeparam name="TDefinition">The type of definition to build from.</typeparam>
    public abstract class BaseDefinitionBuilder<TDefinition>
    {
        /// <summary>
        /// Delegate for XML building blocks.
        /// </summary>
        /// <param name="writer">XML writer for building the definition.</param>
        protected delegate void BuildingBlock(XmlWriter writer);
        
        /// <summary>
        /// Sets the builder state from a create definition object.
        /// </summary>
        /// <param name="definition">The definition to set from.</param>
        /// <returns>The builder instance for method chaining.</returns>
        public abstract BaseDefinitionBuilder<TDefinition> SetFromCreateDefinition(TDefinition definition);
        
        /// <summary>
        /// Builds the XML definition string.
        /// </summary>
        /// <returns>The XML definition as a string.</returns>
        public abstract string Build();
        
        /// <summary>
        /// Validates that all required parameters are provided.
        /// </summary>
        /// <param name="args">Parameters to validate for non-null values.</param>
        /// <exception cref="Exception">Thrown when any parameter is null.</exception>
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
        /// <summary>
        /// Creates an XML writer with appropriate settings for building definitions.
        /// </summary>
        /// <returns>A tuple containing the XML writer and underlying string writer.</returns>
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
