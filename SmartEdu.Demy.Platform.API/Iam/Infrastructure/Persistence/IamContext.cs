namespace SmartEdu.Demy.Platform.API.Iam.Infrastructure.Persistence;

using Microsoft.EntityFrameworkCore;
using Iam.Domain.Model.Aggregates;
using Iam.Domain.Model.ValueObjects;

/// <summary>
/// The database context for the Identity and Access Management (IAM) module.
/// Provides access to <see cref="UserAccount"/> and <see cref="Academy"/> entities.
/// </summary>
public class IamContext : DbContext
{
    /// <summary>
    /// Initializes a new instance of the <see cref="IamContext"/> class.
    /// </summary>
    /// <param name="options">The options to be used by the DbContext.</param>
    public IamContext(DbContextOptions<IamContext> options) : base(options) {}
    /// <summary>
    /// Gets or sets the DbSet for user accounts.
    /// </summary>
    public DbSet<UserAccount> UserAccounts { get; set; }
    /// <summary>
    /// Gets or sets the DbSet for academies.
    /// </summary>
    public DbSet<Academy> Academies { get; set; }
    /// <summary>
    /// Configures the schema needed for the IAM context.
    /// </summary>
    /// <param name="modelBuilder">The builder being used to construct the model for this context.</param>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<UserAccount>(entity =>
        {
            entity.HasKey(e => e.UserId);
            entity.Property(e => e.FullName).IsRequired();
            entity.Property(e => e.Email).IsRequired();
            entity.Property(e => e.PasswordHash).IsRequired();
            entity.Property(e => e.Role)
                .HasConversion<string>()
                .IsRequired();
            entity.Property(e => e.Status)
                .HasConversion<string>()
                .IsRequired();
        });
        modelBuilder.Entity<Academy>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.AcademyName)
                .IsRequired();
            entity.Property(e => e.Ruc)
                .IsRequired();
            
            entity.Property(a => a.UserId)
                .IsRequired();
        });
    }
}