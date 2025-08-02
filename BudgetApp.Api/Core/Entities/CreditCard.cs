using System.ComponentModel.DataAnnotations.Schema;

namespace BudgetApp.Api.Core.Entities
{
    /// <summary>
    /// Represents a credit card belonging to a user.
    /// Corresponds to the 'credit_cards' table.
    /// </summary>
    [Table("credit_cards")]
    public class CreditCard : IAuditable
    {
        [Column("credit_card_id")]
        public int Credit_Card_Id { get; set; }
        [Column("user_id")]
        public int User_Id { get; set; }
        [Column("name")]
        public string Name { get; set; } = null!;
        [Column("bank_name")]
        public string? Bank_Name { get; set; }
        [Column("last_4_digits")]
        public string? Last_4_Digits { get; set; }
        [Column("statement_day")]
        public int Statement_Day { get; set; }
        [Column("payment_due_date_offset")]
        public int Payment_Due_Date_Offset { get; set; }
        [Column("card_limit")]
        public decimal? Card_Limit { get; set; }
        [Column("expiration_date")]
        public string? Expiration_Date { get; set; }
        [Column("created_at")]
        public DateTime Created_At { get; set; }
        [Column("updated_at")]
        public DateTime Updated_At { get; set; }

        // Navigation Properties
        public User User { get; set; } = null!;
        public ICollection<CardRateHistory> CardRateHistories { get; set; } = [];
        public ICollection<InstallmentDefinition> InstallmentDefinitions { get; set; } = [];
        public ICollection<Transaction> Transactions { get; set; } = [];
    }
}
