using EntityFrameworkCore.CreatedUpdatedDate.Extensions;
using Microsoft.EntityFrameworkCore;
using SmartEdu.Demy.Platform.API.Scheduling.Domain.Model.Entities;
using SmartEdu.Demy.Platform.API.Attendance.Domain.Model.Aggregates;
using SmartEdu.Demy.Platform.API.Billing.Domain.Model.Aggregates;
using SmartEdu.Demy.Platform.API.Billing.Domain.Model.Entities;
using SmartEdu.Demy.Platform.API.Iam.Domain.Model.Aggregates;
using SmartEdu.Demy.Platform.API.Scheduling.Domain.Model.Aggregates;
using SmartEdu.Demy.Platform.API.Shared.Infrastructure.Persistence.EFC.Configuration.Extensions;
using SmartEdu.Demy.Platform.API.Enrollment.Domain.Model.Aggregates;
using SmartEdu.Demy.Platform.API.Enrollment.Domain.Model.ValueObjects;
using SmartEdu.Demy.Platform.API.Shared.Infrastructure.Persistence.EFC.Configuration.Extensions;

namespace SmartEdu.Demy.Platform.API.Shared.Infrastructure.Persistence.EFC.Configuration;

/// <summary>
///     Application database context
/// </summary>
public class AppDbContext(DbContextOptions options) : DbContext(options)
{
    // Esto no debería ir aquí
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
        builder.Entity<Invoice>().Property(i => i.Id).ValueGeneratedOnAdd();

        builder.Entity<Invoice>()
            .OwnsOne(i => i.Dni, dni =>
            {
                dni.WithOwner().HasForeignKey("Id");

                dni.Property(d => d.Value)
                    .HasColumnName("dni")
                    .IsRequired()
                    .HasMaxLength(8);
            });

        builder.Entity<Invoice>()
            .OwnsOne(i => i.MonetaryAmount, ma =>
            {
                ma.WithOwner().HasForeignKey("Id");

                ma.Property(m => m.Amount)
                    .HasColumnName("amount")
                    .IsRequired();

                ma.OwnsOne(m => m.Currency, currency =>
                {
                    currency.WithOwner().HasForeignKey("Id");

                    currency.Property(c => c.Code)
                        .HasColumnName("currency")
                        .IsRequired()
                        .HasMaxLength(3);
                });
            });

        builder.Entity<Invoice>().Property(i => i.Name).IsRequired();
        builder.Entity<Invoice>().Property(i => i.DueDate).IsRequired();
        builder.Entity<Invoice>().Property(i => i.Status).IsRequired();

        builder.Entity<Payment>().HasKey(p => p.Id);
        builder.Entity<Payment>().Property(p => p.Id).ValueGeneratedOnAdd();
        builder.Entity<Payment>().Property(p => p.InvoiceId);

        builder.Entity<Payment>()
            .OwnsOne(p => p.MonetaryAmount, ma =>
            {
                ma.WithOwner().HasForeignKey("Id");

                ma.Property(m => m.Amount)
                    .HasColumnName("amount")
                    .IsRequired();

                ma.OwnsOne(m => m.Currency, currency =>
                {
                    currency.WithOwner().HasForeignKey("Id");

                    currency.Property(c => c.Code)
                        .HasColumnName("currency")
                        .IsRequired()
                        .HasMaxLength(3);
                });
            });

        builder.Entity<Payment>().Property(p => p.Method).IsRequired();
        builder.Entity<Payment>().Property(p => p.PaidAt).IsRequired();

        builder.Entity<FinancialTransaction>().HasKey(ft => ft.Id);
        builder.Entity<FinancialTransaction>().Property(ft => ft.Id).ValueGeneratedOnAdd();

        builder.Entity<FinancialTransaction>().Property(ft => ft.Type)
            .HasConversion<string>()
            .IsRequired();

        builder.Entity<FinancialTransaction>().Property(ft => ft.Category)
            .HasConversion<string>()
            .IsRequired();

        builder.Entity<FinancialTransaction>().Property(ft => ft.Concept)
            .IsRequired();

        builder.Entity<FinancialTransaction>().Property(ft => ft.Date)
            .IsRequired();

        builder.Entity<FinancialTransaction>()
            .HasOne(ft => ft.Payment)
            .WithMany()
            .HasForeignKey("PaymentId")
            .IsRequired();

        // Enrollment Context

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
                .IsRequired().HasConversion<string>();

            b.OwnsOne(s => s.Name, name =>
            {
                name.WithOwner().HasForeignKey("Id");

                name.Property(n => n.FirstName)
                    .IsRequired()
                    .HasMaxLength(100);

                name.Property(n => n.LastName)
                    .IsRequired()
                    .HasMaxLength(100);
            });

            b.OwnsOne(s => s.Dni, dni =>
            {
                dni.WithOwner().HasForeignKey("Id");

                dni.Property(d => d.Value)
                    .HasColumnName("dni")
                   .IsRequired()
                   .HasMaxLength(8);
                dni.HasIndex(d => d.Value).IsUnique();
            });

            b.OwnsOne(s => s.PhoneNumber, phone =>
            {
                phone.WithOwner().HasForeignKey("Id");

                phone.Property(p => p.Value)
                    .HasColumnName("phone_number")
                    .IsRequired()
                    .HasMaxLength(9);
            });
        });

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
             .HasMaxLength(10)
             .HasConversion<string>();

            b.Property(e => e.PaymentStatus)
             .IsRequired()
             .HasMaxLength(10)
             .HasConversion<string>();

            b.HasOne<Student>()
             .WithMany()
             .HasForeignKey(e => e.StudentId)
             .OnDelete(DeleteBehavior.Restrict);

            b.HasOne<AcademicPeriod>()
             .WithMany()
             .HasForeignKey(e => e.PeriodId)
             .OnDelete(DeleteBehavior.Restrict);
        });

        // Attendance Context

        builder.Entity<ClassSession>().HasKey(c => c.Id );
        builder.Entity<ClassSession>().Property(c => c.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<ClassSession>().Property(c => c.CourseId).IsRequired();
        builder.Entity<ClassSession>()
            .Property(c => c.Date)
            .IsRequired()
            .HasColumnType("date") // le decimos que es un DATE, no DATETIME
            .HasConversion(
                d => d.ToDateTime(TimeOnly.MinValue),   // al guardar
                dt => DateOnly.FromDateTime(dt)         // al leer
            );

        builder.Entity<ClassSession>().OwnsMany(c => c.Attendance, a =>
        {
            a.WithOwner().HasForeignKey("ClassSessionId");

            a.Property(ar => ar.Dni)
                .HasColumnName("Dni")
                .IsRequired();

            a.Property(ar => ar.Status)
                .HasColumnName("Status")
                .IsRequired()
                .HasConversion<string>();

            a.HasKey("ClassSessionId", "Dni");
        });

        // Scheduling Context

        builder.Entity<Course>().HasKey(c => c.Id);
        builder.Entity<Course>().Property(c => c.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<Course>().Property(c => c.Name).IsRequired().HasMaxLength(100);
        builder.Entity<Course>().Property(c => c.Code).IsRequired().HasMaxLength(20);
        builder.Entity<Course>().Property(c => c.Description).HasMaxLength(500);

        builder.Entity<Classroom>().HasKey(c => c.Id);
        builder.Entity<Classroom>().Property(c => c.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<Classroom>().Property(c => c.Code).IsRequired().HasMaxLength(20);
        builder.Entity<Classroom>().Property(c => c.Capacity).IsRequired();
        builder.Entity<Classroom>().Property(c => c.Campus).IsRequired().HasMaxLength(100);

        // Scheduling Context
        builder.Entity<WeeklySchedule>().HasKey(ws => ws.Id);
        builder.Entity<WeeklySchedule>().Property(ws => ws.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<WeeklySchedule>().Property(ws => ws.Name).IsRequired().HasMaxLength(100);

        builder.Entity<Schedule>().HasKey(s => s.Id);
        builder.Entity<Schedule>().Property(s => s.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<Schedule>().Property(s => s.TeacherId).IsRequired();
        builder.Entity<Schedule>().Property(s => s.DayOfWeek).HasConversion<string>();

        // Configure foreign keys and navigation properties for Schedule
        builder.Entity<Schedule>()
            .HasOne(s => s.Course)
            .WithMany()
            .HasForeignKey(s => s.CourseId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Entity<Schedule>()
            .HasOne(s => s.Classroom)
            .WithMany()
            .HasForeignKey(s => s.ClassroomId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Entity<Schedule>()
            .HasOne(s => s.Teacher)
            .WithMany()
            .HasForeignKey(s => s.TeacherId)
            .OnDelete(DeleteBehavior.Restrict);

        // Configure TimeRange as a value object with explicit column type mapping
        builder.Entity<Schedule>().OwnsOne(s => s.TimeRange, timeRange =>
        {
            timeRange.Property(t => t.StartTime)
                .IsRequired()
                .HasConversion(
                    timeOnly => timeOnly.ToTimeSpan(),
                    timeSpan => TimeOnly.FromTimeSpan(timeSpan));

            timeRange.Property(t => t.EndTime)
                .IsRequired()
                .HasConversion(
                    timeOnly => timeOnly.ToTimeSpan(),
                    timeSpan => TimeOnly.FromTimeSpan(timeSpan));

            timeRange.WithOwner().HasForeignKey("id"); // Use the same primary key as Schedule
        });

        builder.Entity<UserAccount>(entity =>
        {
            entity.ToTable("user_accounts");
            entity.HasKey(e => e.UserId);
            entity.Property(e => e.Role).HasConversion<string>();
            entity.Property(e => e.Status).HasConversion<string>();
        });

        // Convención de nombres snake_case
        builder.UseSnakeCaseNamingConvention();
    }
}