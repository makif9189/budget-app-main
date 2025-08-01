namespace BudgetApp.Api.Core.Entities
{
    /// <summary>
    /// Represents a source of income.
    /// Corresponds to the 'income_sources' table.
    /// </summary>
    public class IncomeSource : IAuditable
    {
        public int Income_Source_Id { get; set; }
        public int User_Id { get; set; }
        public string Name { get; set; } = null!;
        public DateTime Created_At { get; set; }
        public DateTime Updated_At { get; set; }

        // Navigation Properties
        public User User { get; set; } = null!;
        public ICollection<IncomeItem> IncomeItems { get; set; } = [];
    }
}
