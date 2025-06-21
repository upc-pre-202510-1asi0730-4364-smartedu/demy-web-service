using EntityFrameworkCore.CreatedUpdatedDate.Extensions;
using Microsoft.EntityFrameworkCore;
using SmartEdu.Demy.Platform.API.Enrollment.Domain.Model.Aggregates;
using SmartEdu.Demy.Platform.API.Enrollment.Domain.Model.ValueObjects;
using SmartEdu.Demy.Platform.API.Shared.Infrastructure.Persistence.EFC.Configuration.Extensions;

namespace SmartEdu.Demy.Platform.API.Shared.Infrastructure.Persistence.EFC.Configuration;

/// <summary>
///     Application database context
/// </summary>
public class AppDbContext(DbContextOptions options) : DbContext(options)
{
    protected override void OnConfiguring(DbContextOptionsBuilder builder)
    {
        builder.AddCreatedUpdatedInterceptor();
        base.OnConfiguring(builder);
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        // ===== Student =====
        builder.Entity<Student>(b =>
        {
            b.HasKey(s => s.Id);
            b.Property(s => s.Id)
             .IsRequired()
             .ValueGeneratedOnAdd();

            b.Property(s => s.BirthDate)
             .IsRequired();

            b.Property(s => s.Address)
             .IsRequired()
             .HasMaxLength(250);

            b.Property(s => s.Sex)
             .IsRequired();

            b.OwnsOne(s => s.Name, name =>
            {
                name.Property(n => n.FirstName)
                    .IsRequired()
                    .HasMaxLength(100);

                name.Property(n => n.LastName)
                    .IsRequired()
                    .HasMaxLength(100);
                name.WithOwner().HasForeignKey("id"); // igual que en TimeRange
            });

            b.OwnsOne(s => s.Dni, dni =>
            {
                dni.Property(d => d.Value)
                    .HasColumnName("dni")       // columna única para DNI
                   .IsRequired()
                   .HasMaxLength(8);
                dni.HasIndex(d => d.Value).IsUnique();
                dni.WithOwner().HasForeignKey("id"); // igual que en TimeRange
            });

            b.OwnsOne(s => s.PhoneNumber, phone =>
            {
                phone.Property(p => p.Value)
                    .HasColumnName("phone_number")   // columna única para teléfono
                     .IsRequired()
                     .HasMaxLength(9);
                phone.WithOwner().HasForeignKey("id"); // igual que en TimeRange
            });
        });

        // ===== AcademicPeriod =====
        builder.Entity<AcademicPeriod>(b =>
        {
            b.HasKey(p => p.Id);
            b.Property(p => p.Id)
             .IsRequired()
             .ValueGeneratedOnAdd();

            b.Property(p => p.PeriodName)
             .IsRequired()
             .HasMaxLength(100);

            b.Property(p => p.IsActive)
             .IsRequired();

            b.OwnsOne(p => p.PeriodDuration, duration =>
            {
                duration.Property(d => d.StartDate)
                        .IsRequired();

                duration.Property(d => d.EndDate)
                        .IsRequired();

                duration.WithOwner().HasForeignKey("id");  
            });
        });

        // ===== Enrollment =====
        builder.Entity<Enrollment.Domain.Model.Aggregates.Enrollment>(b =>
        {
            b.HasKey(e => e.Id);
            b.Property(e => e.Id)
             .IsRequired()
             .ValueGeneratedOnAdd();

            b.Property(e => e.Amount)
             .IsRequired()
             .HasPrecision(10, 2);

            b.Property(e => e.Currency)
             .IsRequired()
             .HasMaxLength(10);

            b.Property(e => e.EnrollmentStatus)
             .IsRequired()
             .HasMaxLength(50);

            b.Property(e => e.PaymentStatus)
             .IsRequired()
             .HasMaxLength(50);

            // Relaciones
            b.HasOne<Student>()
             .WithMany()
             .HasForeignKey(e => e.StudentId)
             .OnDelete(DeleteBehavior.Restrict);

            b.HasOne<AcademicPeriod>()
             .WithMany()
             .HasForeignKey(e => e.PeriodId)
             .OnDelete(DeleteBehavior.Restrict);
        });

        // Convención de nombres snake_case
        builder.UseSnakeCaseNamingConvention();
    }
}
