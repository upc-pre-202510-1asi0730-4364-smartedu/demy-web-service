using EntityFrameworkCore.CreatedUpdatedDate.Extensions;
using Microsoft.EntityFrameworkCore;
using SmartEdu.Demy.Platform.API.Scheduling.Domain.Model.Entities;
using SmartEdu.Demy.Platform.API.Attendance.Domain.Model.Aggregates;
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
        
        
        
        builder.UseSnakeCaseNamingConvention();
    }
}