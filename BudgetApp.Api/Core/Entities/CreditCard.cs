namespace BudgetApp.Api.Core.Entities
{
    /// <summary>
    /// Represents a credit card belonging to a user.
    /// Corresponds to the 'credit_cards' table.
    /// </summary>
    public class CreditCard : IAuditable
    {
        public int Credit_Card_Id { get; set; }
        public int User_Id { get; set; }
        public string Name { get; set; } = null!;
        public string? Bank_Name { get; set; }
        public string? Last_4_Digits { get; set; }
        public int Statement_Day { get; set; }
        public int Payment_Due_Date_Offset { get; set; }
        public decimal? Card_Limit { get; set; }
        public string? Expiration_Date { get; set; }
        public DateTime Created_At { get; set; }
        public DateTime Updated_At { get; set; }

        // Navigation Properties
        public User User { get; set; } = null!;
        public ICollection<CardRateHistory> CardRateHistories { get; set; } = [];
        public ICollection<InstallmentDefinition> InstallmentDefinitions { get; set; } = [];
        public ICollection<Transaction> Transactions { get; set; } = [];
    }
}
