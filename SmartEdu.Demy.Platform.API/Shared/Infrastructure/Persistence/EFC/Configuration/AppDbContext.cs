using EntityFrameworkCore.CreatedUpdatedDate.Extensions;
using Microsoft.EntityFrameworkCore;
using SmartEdu.Demy.Platform.API.Scheduling.Domain.Model.Entities;
using SmartEdu.Demy.Platform.API.Attendance.Domain.Model.Aggregates;
using SmartEdu.Demy.Platform.API.Billing.Domain.Model.Aggregates;
using SmartEdu.Demy.Platform.API.Billing.Domain.Model.Entities;
using SmartEdu.Demy.Platform.API.Shared.Infrastructure.Persistence.EFC.Configuration.Extensions;
using SmartEdu.Demy.Platform.API.Iam.Domain.Model.Aggregates;

namespace SmartEdu.Demy.Platform.API.Shared.Infrastructure.Persistence.EFC.Configuration;

/// <summary>
///     Application database context
/// </summary>
public class AppDbContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<UserAccount> UserAccounts { get; set; }
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
        
        // Attendance Context
        
        builder.Entity<ClassSession>().HasKey(c => c.Id );
        builder.Entity<ClassSession>().Property(c => c.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<ClassSession>().Property(c => c.CourseId).IsRequired();
        builder.Entity<ClassSession>().Property(c => c.Date).IsRequired();

        builder.Entity<ClassSession>().OwnsMany(c => c.Attendance, a =>
        {
            a.WithOwner().HasForeignKey("ClassSessionId");;
            a.Property(ar => ar.StudentId).HasColumnName("StudentId").IsRequired().ValueGeneratedNever();
            a.Property(ar => ar.Status).HasColumnName("Status").IsRequired();
            
            a.HasKey("ClassSessionId","StudentId");
        });
        
        // Scheduling Context
        builder.Entity<Course>().HasKey(c => c.Id);
        builder.Entity<Course>().Property(c => c.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<Course>().Property(c => c.Name).IsRequired().HasMaxLength(100);
        builder.Entity<Course>().Property(c => c.Code).IsRequired().HasMaxLength(20);
        builder.Entity<Course>().Property(c => c.Description).HasMaxLength(500);
        
        
        builder.Entity<UserAccount>(entity =>
        {
            entity.ToTable("user_accounts");
            entity.HasKey(e => e.UserId);
            entity.Property(e => e.Role).HasConversion<string>();
            entity.Property(e => e.Status).HasConversion<string>();
        });
        builder.UseSnakeCaseNamingConvention();
    }
}