namespace BudgetApp.Api.Core.Entities
{
    /// <summary>
    /// Represents a single income item.
    /// Corresponds to the 'income_items' table.
    /// </summary>
    public class IncomeItem : IAuditable
    {
        public int Income_Item_Id { get; set; }
        public int User_Id { get; set; }
        public int Income_Source_Id { get; set; }
        public decimal Amount { get; set; }
        public DateTime Transaction_Date { get; set; }
        public string? Description { get; set; }
        public bool Is_Recurring { get; set; } = false;
        public string? Recurring_Frequency { get; set; }
        public DateTime? Recurring_Start_Date { get; set; }
        public DateTime? Recurring_End_Date { get; set; }
        public DateTime Created_At { get; set; }
        public DateTime Updated_At { get; set; }

        // Navigation Properties
        public User User { get; set; } = null!;
        public IncomeSource IncomeSource { get; set; } = new IncomeSource();
        public Transaction Transaction { get; set; } = new Transaction();
    }
}
