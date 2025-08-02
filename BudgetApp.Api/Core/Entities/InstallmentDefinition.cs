using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BudgetApp.Api.Core.Entities
{
    /// <summary>
    /// Represents a definition for an installment-based purchase.
    /// Corresponds to the 'installment_definitions' table.
    /// </summary>
    [Table("installment_definitions")]
    public class InstallmentDefinition : IAuditable
    {
        [Key]
        [Column("installment_definition_id")]
        public int Installment_Definition_Id { get; set; }
        [Column("credit_card_id")]
        public int Credit_Card_Id { get; set; }
        [Column("description")]
        public string Description { get; set; } = null!;
        [Column("monthly_amount")]
        public decimal Monthly_Amount { get; set; }
        [Column("total_installments")]
        public int Total_Installments { get; set; }
        [Column("first_installment_date")]
        public DateTime First_Installment_Date { get; set; }
        [Column("created_at")]
        public DateTime Created_At { get; set; }
        [Column("updated_at")]
        public DateTime Updated_At { get; set; }

        // Navigation Property
        public CreditCard CreditCard { get; set; } = new CreditCard();
        public Transaction Transaction { get; set; } = new Transaction();
    }
}
