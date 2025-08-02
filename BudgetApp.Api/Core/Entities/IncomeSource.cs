using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BudgetApp.Api.Core.Entities
{
    /// <summary>
    /// Represents a source of income.
    /// Corresponds to the 'income_sources' table.
    /// </summary>
    [Table("income_sources")]
    public class IncomeSource : IAuditable
    {
        [Key]
        [Column("income_source_id")]
        public int Income_Source_Id { get; set; }
        [Column("user_id")]
        public int User_Id { get; set; }
        [Column("name")]
        public string Name { get; set; } = null!;
        [Column("created_at")]
        public DateTime Created_At { get; set; }
        [Column("updated_at")]
        public DateTime Updated_At { get; set; }

        // Navigation Properties
        public User User { get; set; } = null!;
        public ICollection<IncomeItem> IncomeItems { get; set; } = [];
    }
}
