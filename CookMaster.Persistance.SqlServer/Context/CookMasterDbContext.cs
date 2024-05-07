using System;
using System.Collections.Generic;
using CookMaster.Persistance.SqlServer.Model;
using Microsoft.EntityFrameworkCore;

namespace CookMaster.Persistance.SqlServer.Context;

public partial class CookMasterDbContext : DbContext
{
    public CookMasterDbContext()
    {
    }

    public CookMasterDbContext(DbContextOptions<CookMasterDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Photo> Photos { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<Recipe> Recipes { get; set; }

    public virtual DbSet<Step> Steps { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserMenu> UserMenus { get; set; }

    //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    //{
    //    optionsBuilder.UseSqlServer("Server=localhost,1433;Database=CookMasterDB;User Id=sa;Password=Your@Password;TrustServerCertificate=True");
    //}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Photo>(entity =>
        {
            entity.Property(e => e.FileName).HasMaxLength(50);
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.Property(e => e.Amount).HasMaxLength(50);
            entity.Property(e => e.Category).HasMaxLength(50);
            entity.Property(e => e.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<Recipe>(entity =>
        {
            entity.Property(e => e.Description).HasMaxLength(50);
            entity.Property(e => e.Name).HasMaxLength(50);
            entity.Property(e => e.PrepareTime)
                .HasMaxLength(10)
                .IsFixedLength();

            entity.HasOne(d => d.IdFavouriteNavigation).WithMany(p => p.Recipes)
                .HasForeignKey(d => d.IdFavourite)
                .HasConstraintName("FK_Recipes_Users");

            entity.HasOne(d => d.IdPhotoNavigation).WithMany(p => p.Recipes)
                .HasForeignKey(d => d.IdPhoto)
                .HasConstraintName("FK_Recipes_Photos");

            entity.HasOne(d => d.IdProductRecipeNavigation).WithMany(p => p.Recipes)
                .HasForeignKey(d => d.IdProductRecipe)
                .HasConstraintName("FK_Recipes_Products");

            entity.HasOne(d => d.IdStepsRecipeNavigation).WithMany(p => p.Recipes)
                .HasForeignKey(d => d.IdStepsRecipe)
                .HasConstraintName("FK_Recipes_Steps");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.Property(e => e.Email).HasMaxLength(50);
            entity.Property(e => e.Password).HasMaxLength(50);

            entity.HasOne(d => d.IdMenuNavigation).WithMany(p => p.Users)
                .HasForeignKey(d => d.IdMenu)
                .HasConstraintName("FK_Users_UserMenu");
        });

        modelBuilder.Entity<UserMenu>(entity =>
        {
            entity.ToTable("UserMenu");

            entity.Property(e => e.Name).HasMaxLength(50);
            entity.Property(e => e.RecipeCategory).HasMaxLength(50);

            entity.HasOne(d => d.IdMenuRecipeNavigation).WithMany(p => p.UserMenus)
                .HasForeignKey(d => d.IdMenuRecipe)
                .HasConstraintName("FK_UserMenu_Recipes");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
