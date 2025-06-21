﻿namespace SmartEdu.Demy.Platform.API.Iam.Infrastructure.Persistence;

using Microsoft.EntityFrameworkCore;
using Iam.Domain.Model.Aggregates;
using Iam.Domain.Model.ValueObjects;

public class IamContext : DbContext
{
    public IamContext(DbContextOptions<IamContext> options) : base(options) {}

    public DbSet<UserAccount> UserAccounts { get; set; }

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
    }
}