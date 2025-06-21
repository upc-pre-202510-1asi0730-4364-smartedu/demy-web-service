using System;
using EntityFrameworkCore.CreatedUpdatedDate.Extensions;
using Microsoft.EntityFrameworkCore;
using SmartEdu.Demy.Platform.API.Scheduling.Domain.Model.Entities;
using SmartEdu.Demy.Platform.API.Scheduling.Domain.Model.Aggregates;
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

        // Aquí irá lo demás
        
        
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
        
        
        builder.UseSnakeCaseNamingConvention();
    }
}