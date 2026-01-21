namespace Models
{
    /// <summary>
    /// Base class representing an entity with a position on the board.
    /// </summary>
    public class BaseEntity
    {
        /// <summary>
        /// Entity identifier.
        /// </summary>
        public required int Id { get; set; }
        
        /// <summary>
        /// X coordinate of the entity position on the board.
        /// </summary>
        public required int X { get; set; }
        
        /// <summary>
        /// Y coordinate of the entity position on the board.
        /// </summary>
        public required int Y { get; set; }
    }
}
