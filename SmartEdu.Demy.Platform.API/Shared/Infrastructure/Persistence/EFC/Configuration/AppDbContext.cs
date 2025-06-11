using EntityFrameworkCore.CreatedUpdatedDate.Extensions;
using Microsoft.EntityFrameworkCore;
using SmartEdu.Demy.Platform.API.Billing.Domain.Model.Aggregates;
using SmartEdu.Demy.Platform.API.Billing.Domain.Model.Entities;
using SmartEdu.Demy.Platform.API.Shared.Infrastructure.Persistence.EFC.Configuration.Extensions;

namespace SmartEdu.Demy.Platform.API.Shared.Infrastructure.Persistence.EFC.Configuration;

/// <summary>
///     Application database context
/// </summary>
public class AppDbContext(DbContextOptions options) : DbContext(options)
{
    protected override void OnConfiguring(DbContextOptionsBuilder builder)
    {
        // Add the created and updated interceptor
        builder.AddCreatedUpdatedInterceptor();
        base.OnConfiguring(builder);
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        
        // Billing Context
        
        builder.Entity<Invoice>().HasKey(i => i.Id);
        builder.Entity<Invoice>().Property(i => i.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<Invoice>().Property(i => i.StudentId).IsRequired();
        builder.Entity<Invoice>().Property(i => i.Amount).IsRequired();
        builder.Entity<Invoice>().Property(i => i.Currency).IsRequired().HasMaxLength(3);
        builder.Entity<Invoice>().Property(i => i.DueDate).IsRequired();
        builder.Entity<Invoice>().Property(i => i.Status).IsRequired();
        builder.Entity<Invoice>().HasMany(i => i.Payments)
            .WithOne(p => p.Invoice)
            .HasForeignKey(p => p.InvoiceId)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.Entity<Payment>().HasKey(p => p.Id);
        builder.Entity<Payment>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<Payment>().Property(p => p.Amount).IsRequired();
        builder.Entity<Payment>().Property(p => p.Currency).IsRequired().HasMaxLength(3);
        builder.Entity<Payment>().Property(p => p.Method).IsRequired();
        builder.Entity<Payment>().Property(p => p.PaidAt).IsRequired();
        
        builder.UseSnakeCaseNamingConvention();
    }
}