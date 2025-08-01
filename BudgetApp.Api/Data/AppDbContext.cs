using Microsoft.EntityFrameworkCore;
using BudgetApp.Api.Core.Entities;
using BudgetApp.Api.Core.Enums;

namespace BudgetApp.Api.Data
{
    /// <summary>
    /// Represents the database context for the application, managing the connection
    /// and mapping entities to the database tables.
    /// </summary>
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        // Define DbSet for each entity to be mapped to a database table.
        public DbSet<User> Users { get; set; }
        public DbSet<CreditCard> CreditCards { get; set; }
        public DbSet<CardRateHistory> CardRateHistories { get; set; }
        public DbSet<ExpenseCategory> ExpenseCategories { get; set; }
        public DbSet<ExpenseItem> ExpenseItems { get; set; }
        public DbSet<IncomeSource> IncomeSources { get; set; }
        public DbSet<IncomeItem> IncomeItems { get; set; }
        public DbSet<InstallmentDefinition> InstallmentDefinitions { get; set; }
        public DbSet<Transaction> Transactions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Map the C# TransactionTypeEnum to the PostgreSQL 'transaction_type_enum'
            modelBuilder.HasPostgresEnum<TransactionTypeEnum>();

            // Configure entity relationships and constraints using Fluent API

            // User Entity Configuration
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.User_Id);
                entity.HasIndex(e => e.Username).IsUnique();
                entity.HasIndex(e => e.Email).IsUnique();
                entity.Property(e => e.Password_Hash).IsRequired();
            });

            // CreditCard Entity Configuration
            modelBuilder.Entity<CreditCard>(entity =>
            {
                entity.HasKey(e => e.Credit_Card_Id);
                entity.HasOne(d => d.User)
                    .WithMany(p => p.CreditCards)
                    .HasForeignKey(d => d.User_Id)
                    .OnDelete(DeleteBehavior.Cascade); // If user is deleted, their cards are deleted.
            });
            
            // Transaction Entity Configuration
            modelBuilder.Entity<Transaction>(entity =>
            {
                entity.HasKey(e => e.Transaction_Id);

                // A transaction is owned by one user
                entity.HasOne(d => d.User)
                    .WithMany(p => p.Transactions)
                    .HasForeignKey(d => d.User_Id)
                    .OnDelete(DeleteBehavior.Cascade);

                // A transaction can be linked to one income item
                entity.HasOne(d => d.IncomeItem)
                    .WithOne(p => p.Transaction)
                    .HasForeignKey<Transaction>(d => d.Income_Item_Id)
                    .IsRequired(false); // Optional relationship

                // A transaction can be linked to one expense item
                entity.HasOne(d => d.ExpenseItem)
                    .WithOne(p => p.Transaction)
                    .HasForeignKey<Transaction>(d => d.Expense_Item_Id)
                    .IsRequired(false);

                // A transaction can be linked to one installment definition
                 entity.HasOne(d => d.InstallmentDefinition)
                    .WithOne(p => p.Transaction)
                    .HasForeignKey<Transaction>(d => d.Installment_Definition_Id)
                    .IsRequired(false);
            });
            
            // ExpenseItem Configuration
            modelBuilder.Entity<ExpenseItem>(entity =>
            {
                entity.HasKey(e => e.Expense_Item_Id);

                entity.HasOne(d => d.ExpenseCategory)
                    .WithMany(p => p.ExpenseItems)
                    .HasForeignKey(d => d.Expense_Category_Id)
                    .OnDelete(DeleteBehavior.Restrict); // Prevent deleting a category if it has expenses.
            });
            
            // IncomeItem Configuration
            modelBuilder.Entity<IncomeItem>(entity =>
            {
                entity.HasKey(e => e.Income_Item_Id);

                entity.HasOne(d => d.IncomeSource)
                    .WithMany(p => p.IncomeItems)
                    .HasForeignKey(d => d.Income_Source_Id)
                    .OnDelete(DeleteBehavior.Restrict); // Prevent deleting a source if it has incomes.
            });
        }

        /// <summary>
        /// Overrides the default SaveChangesAsync to automatically update audit fields.
        /// </summary>
        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var entries = ChangeTracker
                .Entries()
                .Where(e => e.Entity is IAuditable && (
                        e.State == EntityState.Added ||
                        e.State == EntityState.Modified));

            foreach (var entityEntry in entries)
            {
                ((IAuditable)entityEntry.Entity).Updated_At = DateTime.UtcNow;

                if (entityEntry.State == EntityState.Added)
                {
                    ((IAuditable)entityEntry.Entity).Created_At = DateTime.UtcNow;
                }
            }

            return base.SaveChangesAsync(cancellationToken);
        }
    }
}
