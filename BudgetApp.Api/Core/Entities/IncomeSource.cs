using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BudgetApp.Api.Core.Entities;

[Table("income_sources")]
public class IncomeSource : IAuditable
{
    [Key]
    [Column("income_source_id")]
    public int IncomeSourceId { get; set; }
    
    [Column("user_id")]
    public int UserId { get; set; }
    
    [Column("name")]
    public string Name { get; set; } = null!;
    
    [Column("created_at")]
    public DateTime CreatedAt { get; set; }
    
    [Column("updated_at")]
    public DateTime UpdatedAt { get; set; }

    // Navigation Properties
    public User User { get; set; } = null!;
    public ICollection<IncomeItem> IncomeItems { get; set; } = new List<IncomeItem>();
}