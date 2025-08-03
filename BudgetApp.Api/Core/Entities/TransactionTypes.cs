using System.ComponentModel.DataAnnotations.Schema;

namespace BudgetApp.Api.Core.Entities;

[Table("transaction_types")]
public class TransactionType : IAuditable
{
    [Column("type_id")]
    public int TypeId { get; set; }

    [Column("name")]
    public string Name { get; set; } = null!;
    [Column("created_at")]
    public DateTime CreatedAt { get; set; }

    [Column("updated_at")]
    public DateTime UpdatedAt { get; set; }

    // Navigation Properties
    public ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();
}