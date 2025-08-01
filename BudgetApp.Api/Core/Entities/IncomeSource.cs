namespace BudgetApp.Api.Core.Entities
{
    /// <summary>
    /// Represents a source of income.
    /// Corresponds to the 'income_sources' table.
    /// </summary>
    public class IncomeSource : IAuditable
    {
        public int income_source_id { get; set; }
        public int user_id { get; set; }
        public string name { get; set; }
        public DateTime created_at { get; set; }
        public DateTime updated_at { get; set; }

        // Navigation Properties
        public User User { get; set; }
        public ICollection<IncomeItem> IncomeItems { get; set; } = new List<IncomeItem>();
    }
}
