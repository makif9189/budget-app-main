namespace BudgetApp.Api.Core.Entities
{
    /// <summary>
    /// Represents a definition for an installment-based purchase.
    /// Corresponds to the 'installment_definitions' table.
    /// </summary>
    public class InstallmentDefinition : IAuditable
    {
        public int Installment_Definition_Id { get; set; }
        public int Credit_Card_Id { get; set; }
        public string Description { get; set; } = null!;
        public decimal Monthly_Amount { get; set; }
        public int Total_Installments { get; set; }
        public DateTime First_Installment_Date { get; set; }
        public DateTime Created_At { get; set; }
        public DateTime Updated_At { get; set; }

        // Navigation Property
        public CreditCard CreditCard { get; set; } = new CreditCard();
        public Transaction Transaction { get; set; } = new Transaction();
    }
}
