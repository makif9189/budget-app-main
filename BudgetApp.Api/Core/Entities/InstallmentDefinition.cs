namespace BudgetApp.Api.Core.Entities
{
    /// <summary>
    /// Represents a definition for an installment-based purchase.
    /// Corresponds to the 'installment_definitions' table.
    /// </summary>
    public class InstallmentDefinition : IAuditable
    {
        public int installment_definition_id { get; set; }
        public int credit_card_id { get; set; }
        public string description { get; set; }
        public decimal monthly_amount { get; set; }
        public int total_installments { get; set; }
        public DateTime first_installment_date { get; set; }
        public DateTime created_at { get; set; }
        public DateTime updated_at { get; set; }

        // Navigation Property
        public CreditCard CreditCard { get; set; }
        public Transaction Transaction { get; set; }
    }
}
