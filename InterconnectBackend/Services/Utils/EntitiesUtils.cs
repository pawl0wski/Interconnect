using Models.Enums;

namespace Services.Utils
{
    /// <summary>
    /// Utility class for entity-related operations and transformations.
    /// </summary>
    public static class EntitiesUtils
    {
        /// <summary>
        /// Reorders entity IDs so virtual machine entity comes first.
        /// </summary>
        /// <param name="sourceEntityId">Source entity identifier.</param>
        /// <param name="sourceEntityType">Source entity type.</param>
        /// <param name="destinationEntityId">Destination entity identifier.</param>
        /// <param name="destinationEntityType">Destination entity type.</param>
        /// <returns>Tuple with virtual machine ID first, or original order if not found.</returns>
        public static (int sourceEntityId, int destinationEntityId) GetVirtualMachineEntityIdFirst(int sourceEntityId, EntityType sourceEntityType, int destinationEntityId, EntityType destinationEntityType)
        {
            if (sourceEntityType != EntityType.VirtualMachine)
            {
                (sourceEntityId, destinationEntityId) = (destinationEntityId, sourceEntityId);
            }

            return (sourceEntityId, destinationEntityId);
        }

        /// <summary>
        /// Reorders entity IDs so Internet entity comes first.
        /// </summary>
        /// <param name="sourceEntityId">Source entity identifier.</param>
        /// <param name="sourceEntityType">Source entity type.</param>
        /// <param name="destinationEntityId">Destination entity identifier.</param>
        /// <param name="destinationEntityType">Destination entity type.</param>
        /// <returns>Tuple with Internet ID first, or original order if not found.</returns>
        public static (int sourceEntityId, int destinationEntityId) GetInternetEntityIdFirst(int sourceEntityId, EntityType sourceEntityType, int destinationEntityId, EntityType destinationEntityType)
        {
            if (sourceEntityType != EntityType.Internet)
            {
                (sourceEntityId, destinationEntityId) = (destinationEntityId, sourceEntityId);
            }

            return (sourceEntityId, destinationEntityId);
        }

        /// <summary>
        /// Checks if entity types match in either order.
        /// </summary>
        /// <param name="sourceEntityType">Source entity type to check.</param>
        /// <param name="destinationEntityType">Destination entity type to check.</param>
        /// <param name="firstEntityType">First type to compare against.</param>
        /// <param name="secondEntityType">Second type to compare against.</param>
        /// <returns>True if entity types match in either order; otherwise false.</returns>
        public static bool AreTypes(EntityType sourceEntityType, EntityType destinationEntityType, EntityType firstEntityType, EntityType secondEntityType)
        {
            return (sourceEntityType == firstEntityType && destinationEntityType == secondEntityType) || (sourceEntityType == secondEntityType && destinationEntityType == firstEntityType);
        }
    }
}
