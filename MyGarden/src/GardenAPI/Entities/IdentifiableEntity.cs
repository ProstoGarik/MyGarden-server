namespace GardenAPI.Entities
{
    /// <summary>
    ///     Модель с уникальным идентификатором.
    /// </summary>
    public class IdentifiableEntity : Entity
    {
        /// <summary>
        ///     Идентификатор.
        /// </summary>
        public int? Id { get; set; }
    }
}
