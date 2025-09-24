using Models.Enums;

namespace Services.Utils
{
    public static class EntitiesUtils
    {
        public static (int sourceEntityId, int destinationEntityId) ResolveEntityIdsOrder(int sourceEntityId, EntityType sourceEntityType, int destinationEntityId, EntityType destinationEntityType)
        {
            if (sourceEntityType != EntityType.VirtualMachine)
            {
                (sourceEntityId, destinationEntityId) = (destinationEntityId, sourceEntityId);
            }

            return (sourceEntityId, destinationEntityId);
        }

        public static bool AreTypes(EntityType sourceEntityType, EntityType destinationEntityType, EntityType firstEntityType, EntityType secondEntityType)
        {
            return (sourceEntityType == firstEntityType && destinationEntityType == secondEntityType) || (sourceEntityType == secondEntityType && destinationEntityType == firstEntityType);
        }
    }
}
