using Microsoft.EntityFrameworkCore;
using BudgetApp.Api.Core.Entities;

namespace BudgetApp.Api.Infrastructure.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    // DbSets
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

        foreach (var entityType in modelBuilder.Model.GetEntityTypes())
        {
            foreach (var property in entityType.GetProperties())
            {
                if (property.ClrType == typeof(DateTime) || property.ClrType == typeof(DateTime?))
                {
                    property.SetValueConverter(new Microsoft.EntityFrameworkCore.Storage.ValueConversion.ValueConverter<DateTime, DateTime>(
                        v => v.Kind == DateTimeKind.Unspecified ? DateTime.SpecifyKind(v, DateTimeKind.Utc) : v.ToUniversalTime(),
                        v => DateTime.SpecifyKind(v, DateTimeKind.Utc)));
                }
            }
        }

        ConfigureUserEntity(modelBuilder);
        ConfigureCreditCardEntity(modelBuilder);
        ConfigureExpenseEntities(modelBuilder);
        ConfigureIncomeEntities(modelBuilder);
        ConfigureTransactionEntity(modelBuilder);
        ConfigureTransactionTypeEntity(modelBuilder);
        ConfigureInstallmentEntity(modelBuilder);
        ConfigureCardRateHistoryEntity(modelBuilder);
    }

    private static void ConfigureUserEntity(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId);
            entity.HasIndex(e => e.Username).IsUnique();
            entity.HasIndex(e => e.Email).IsUnique();
            
            entity.Property(e => e.Username)
                .IsRequired()
                .HasMaxLength(50);
                
            entity.Property(e => e.Email)
                .IsRequired()
                .HasMaxLength(100);
                
            entity.Property(e => e.PasswordHash)
                .IsRequired()
                .HasMaxLength(255);
        });
    }

    private static void ConfigureCreditCardEntity(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CreditCard>(entity =>
        {
            entity.HasKey(e => e.CreditCardId);
            
            entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(100);
                
            entity.Property(e => e.BankName)
                .HasMaxLength(100);
                
            entity.Property(e => e.Last4Digits)
                .HasMaxLength(4);
                
            entity.Property(e => e.ExpirationDate)
                .HasMaxLength(5);
                
            entity.Property(e => e.CardLimit)
                .HasPrecision(18, 2);

            entity.HasOne(d => d.User)
                .WithMany(p => p.CreditCards)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        });
    }

    private static void ConfigureExpenseEntities(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ExpenseCategory>(entity =>
        {
            entity.HasKey(e => e.ExpenseCategoryId);
            
            entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(100);

            entity.HasOne(d => d.User)
                .WithMany(p => p.ExpenseCategories)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<ExpenseItem>(entity =>
        {
            entity.HasKey(e => e.ExpenseItemId);
            
            entity.Property(e => e.Amount)
                .HasPrecision(18, 2);
                
            entity.Property(e => e.Description)
                .HasMaxLength(500);

            entity.HasOne(d => d.User)
                .WithMany(p => p.ExpenseItems)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(d => d.ExpenseCategory)
                .WithMany(p => p.ExpenseItems)
                .HasForeignKey(d => d.ExpenseCategoryId)
                .OnDelete(DeleteBehavior.Restrict);
        });
    }

    private static void ConfigureIncomeEntities(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<IncomeSource>(entity =>
        {
            entity.HasKey(e => e.IncomeSourceId);
            
            entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(100);

            entity.HasOne(d => d.User)
                .WithMany(p => p.IncomeSources)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<IncomeItem>(entity =>
        {
            entity.HasKey(e => e.IncomeItemId);
            
            entity.Property(e => e.Amount)
                .HasPrecision(18, 2);
                
            entity.Property(e => e.Description)
                .HasMaxLength(500);
                
            entity.Property(e => e.RecurringFrequency)
                .HasMaxLength(50);

            entity.HasOne(d => d.User)
                .WithMany(p => p.IncomeItems)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(d => d.IncomeSource)
                .WithMany(p => p.IncomeItems)
                .HasForeignKey(d => d.IncomeSourceId)
                .OnDelete(DeleteBehavior.Restrict);
        });
    }

    private static void ConfigureTransactionEntity(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Transaction>(entity =>
        {
            entity.HasKey(e => e.TransactionId);

            entity.Property(e => e.Amount)
                .HasPrecision(18, 2);

            entity.Property(e => e.Description)
                .HasMaxLength(500);

            entity.Property(e => e.Type)
                .HasConversion<int>()
                .IsRequired();

            entity.HasOne(d => d.User)
                .WithMany(p => p.Transactions)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(d => d.IncomeItem)
                .WithOne(p => p.Transaction)
                .HasForeignKey<Transaction>(d => d.IncomeItemId)
                .IsRequired(false);

            entity.HasOne(d => d.ExpenseItem)
                .WithOne(p => p.Transaction)
                .HasForeignKey<Transaction>(d => d.ExpenseItemId)
                .IsRequired(false);

            entity.HasOne(d => d.CreditCard)
                .WithMany(p => p.Transactions)
                .HasForeignKey(d => d.CreditCardId)
                .IsRequired(false);

            entity.HasOne(d => d.InstallmentDefinition)
                .WithOne(p => p.Transaction)
                .HasForeignKey<Transaction>(d => d.InstallmentDefinitionId)
                .IsRequired(false);
            
            entity.HasOne(d => d.TransactionType)
                .WithMany(p => p.Transactions)
                .HasForeignKey(d => d.Type)
                .OnDelete(DeleteBehavior.Restrict); // Transaction silinse bile TransactionType silinmez
        });
    }
    private static void ConfigureTransactionTypeEntity(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TransactionType>(entity =>
        {
            entity.HasKey(e => e.TypeId);
            entity.HasIndex(e => e.Name).IsUnique();
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP");
        });
    }

    private static void ConfigureInstallmentEntity(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<InstallmentDefinition>(entity =>
        {
            entity.HasKey(e => e.InstallmentDefinitionId);

            entity.Property(e => e.Description)
                .IsRequired()
                .HasMaxLength(200);

            entity.Property(e => e.MonthlyAmount)
                .HasPrecision(18, 2);

            entity.HasOne(d => d.CreditCard)
                .WithMany(p => p.InstallmentDefinitions)
                .HasForeignKey(d => d.CreditCardId)
                .OnDelete(DeleteBehavior.Cascade);
        });
    }

    private static void ConfigureCardRateHistoryEntity(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CardRateHistory>(entity =>
        {
            entity.HasKey(e => e.RateHistoryId);
            
            entity.Property(e => e.InterestRate)
                .HasPrecision(5, 4);
                
            entity.Property(e => e.KkdfRate)
                .HasPrecision(5, 4);
                
            entity.Property(e => e.BsmvRate)
                .HasPrecision(5, 4);
                
            entity.Property(e => e.MinimumPaymentRate)
                .HasPrecision(5, 4);
                
            entity.Property(e => e.AbsoluteMinimumPayment)
                .HasPrecision(18, 2);

            entity.HasOne(d => d.CreditCard)
                .WithMany(p => p.CardRateHistories)
                .HasForeignKey(d => d.CreditCardId)
                .OnDelete(DeleteBehavior.Cascade);
        });
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        var entries = ChangeTracker
            .Entries()
            .Where(e => e.Entity is IAuditable && (
                    e.State == EntityState.Added ||
                    e.State == EntityState.Modified));

        foreach (var entityEntry in entries)
        {
            var auditable = (IAuditable)entityEntry.Entity;
            // UTC olarak ayarla
            auditable.UpdatedAt = DateTime.UtcNow;

            if (entityEntry.State == EntityState.Added)
            {
                auditable.CreatedAt = DateTime.UtcNow;
            }
        }

        // DateTime alanlarını UTC'ye çevir
        var dateTimeEntries = ChangeTracker.Entries()
            .SelectMany(e => e.Properties)
            .Where(p => p.CurrentValue != null && 
                       (p.Metadata.ClrType == typeof(DateTime) || 
                        p.Metadata.ClrType == typeof(DateTime?)));

        foreach (var property in dateTimeEntries)
        {
            if (property.CurrentValue is DateTime dateTime && dateTime.Kind == DateTimeKind.Unspecified)
            {
                property.CurrentValue = DateTime.SpecifyKind(dateTime, DateTimeKind.Utc);
            }
        }

        return await base.SaveChangesAsync(cancellationToken);
    }
}