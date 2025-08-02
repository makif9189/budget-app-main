using System.ComponentModel.DataAnnotations.Schema;


namespace BudgetApp.Api.Core.Entities
{
    /// <summary>
    /// Represents a single income item.
    /// Corresponds to the 'income_items' table.
    /// </summary>
    [Table("income_items")]
    public class IncomeItem : IAuditable
    {
        [Column("income_item_id")]
        public int Income_Item_Id { get; set; }
        [Column("user_id")]
        public int User_Id { get; set; }
        [Column("income_source_id")]
        public int Income_Source_Id { get; set; }
        [Column("amount")]
        public decimal Amount { get; set; }
        [Column("transaction_date")]
        public DateTime Transaction_Date { get; set; }
        [Column("description")]
        public string? Description { get; set; }
        [Column("is_recurring")]
        public bool Is_Recurring { get; set; } = false;
        [Column("recurring_frequency")]
        public string? Recurring_Frequency { get; set; }
        [Column("recurring_start_date")]
        public DateTime? Recurring_Start_Date { get; set; }
        [Column("recurring_end_date")]
        public DateTime? Recurring_End_Date { get; set; }
        [Column("created_at")]
        public DateTime Created_At { get; set; }
        [Column("updated_at")]
        public DateTime Updated_At { get; set; }

        // Navigation Properties
        public User User { get; set; } = null!;
        public IncomeSource IncomeSource { get; set; } = new IncomeSource();
        public Transaction Transaction { get; set; } = new Transaction();
    }
}
