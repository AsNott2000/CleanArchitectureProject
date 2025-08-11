using System;
using CleanArchitectureProject.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitectureProject.Infrastructure.Presistence;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }


    public DbSet<CarsModel> Cars => Set<CarsModel>();
    public DbSet<UsersModel> Users => Set<UsersModel>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CarsModel>(b =>
        {
            b.ToTable("car");
            b.HasKey(x => x.Id);
            b.Property(x => x.Id).HasColumnName("id");
            b.Property(x => x.CarName).HasColumnName("carName").IsRequired();

            // 1 car -> many users
            b.HasMany(c => c.Users)
            .WithOne(u => u.Car)
            .HasForeignKey(u => u.CarId)
            .HasConstraintName("fk_car")
            .OnDelete(DeleteBehavior.Restrict); // PostgreSQL DDL: NO ACTION
        });
        

        modelBuilder.Entity<UsersModel>(b =>
        {
            b.ToTable("users");
            b.HasKey(x => x.Id);
            b.Property(x => x.Id).HasColumnName("id");
            b.Property(x => x.Name).HasColumnName("name").IsRequired();
            b.Property(x => x.Surname).HasColumnName("surname").IsRequired();
            b.Property(x => x.CarId).HasColumnName("carId").IsRequired();
            b.Property(x => x.Password).HasColumnName("password").IsRequired();
            b.Property(x => x.UserName).HasColumnName("userName").IsRequired();
        });
    }
}
