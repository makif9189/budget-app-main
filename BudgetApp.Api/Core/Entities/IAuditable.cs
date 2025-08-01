namespace BudgetApp.Api.Core.Entities
{
    /// <summary>
    /// Interface to mark entities that should have creation and update timestamps.
    /// </summary>
    public interface IAuditable
    {
        /// <summary>
        /// The date and time when the entity was created.
        /// </summary>
        DateTime Created_At { get; set; }

        /// <summary>
        /// The date and time when the entity was last updated.
        /// </summary>
        DateTime Updated_At { get; set; }
    }
}
