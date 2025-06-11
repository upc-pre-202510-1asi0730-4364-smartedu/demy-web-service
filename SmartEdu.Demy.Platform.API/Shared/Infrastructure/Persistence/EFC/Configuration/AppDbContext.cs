using EntityFrameworkCore.CreatedUpdatedDate.Extensions;
using Microsoft.EntityFrameworkCore;
using SmartEdu.Demy.Platform.API.Shared.Infrastructure.Persistence.EFC.Configuration.Extensions;

using SmartEdu.Demy.Platform.API.Enrollment.Domain.Model.Entities;

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
        
        
// Enrollment Context

        builder.Entity<Student>().HasKey(s => s.Id);

        builder.Entity<Student>().Property(s => s.Id)
            .IsRequired()
            .ValueGeneratedOnAdd();

        builder.Entity<Student>().Property(s => s.FirstName)
            .IsRequired()
            .HasMaxLength(100);

        builder.Entity<Student>().Property(s => s.LastName)
            .IsRequired()
            .HasMaxLength(100);

        builder.Entity<Student>().Property(s => s.Dni)
            .IsRequired()
            .HasMaxLength(20);

        builder.Entity<Student>().Property(s => s.Sex)
            .IsRequired()
            .HasConversion<string>()  // Si ESex es enum, lo guardamos como string
            .HasMaxLength(10);

        builder.Entity<Student>().Property(s => s.BirthDate)
            .IsRequired(false);

        builder.Entity<Student>().Property(s => s.Address)
            .IsRequired()
            .HasMaxLength(255);

        builder.Entity<Student>().Property(s => s.PhoneNumber)
            .HasMaxLength(9);
        
        // Aquí irá lo demás
        
        builder.UseSnakeCaseNamingConvention();
    }
}