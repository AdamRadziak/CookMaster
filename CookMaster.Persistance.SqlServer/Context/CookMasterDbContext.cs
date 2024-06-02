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


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Photo>(entity =>
        {
            entity.Property(e => e.FileName).HasMaxLength(50);
            entity.Property(e => e.FilePath).HasMaxLength(120);

            entity.HasOne(d => d.IdRecipeNavigation).WithMany(p => p.Photos)
                .HasForeignKey(d => d.IdRecipe)
                .HasConstraintName("FK_Photos_Recipes")
                .OnDelete(DeleteBehavior.ClientSetNull);
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.Property(e => e.Category).HasMaxLength(50);
            entity.Property(e => e.Name).HasMaxLength(50);
            entity.Property(e => e.Unit).HasMaxLength(30);

            entity.HasOne(d => d.IdRecipeNavigation).WithMany(p => p.Products)
                .HasForeignKey(d => d.IdRecipe)
                .HasConstraintName("FK_Products_Recipes")
                .OnDelete(DeleteBehavior.ClientSetNull);
        });

        modelBuilder.Entity<Recipe>(entity =>
        {
            entity.Property(e => e.Category)
                .HasMaxLength(30)
                .IsFixedLength();
            entity.Property(e => e.Description).HasMaxLength(100);
            entity.Property(e => e.Name).HasMaxLength(50);

            entity.HasOne(d => d.IdMenuNavigation).WithMany(p => p.Recipes)
                .HasForeignKey(d => d.IdMenu)
                .HasConstraintName("FK_Recipes_UserMenu")
                .OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.IdUserNavigation).WithMany(p => p.Recipes)
                .HasForeignKey(d => d.IdUser)
                .HasConstraintName("FK_Recipes_Users")
                .OnDelete(DeleteBehavior.ClientSetNull);
        });

        modelBuilder.Entity<Step>(entity =>
        {
            entity.Property(e => e.StepNum).HasColumnName("Step_Num");

            entity.HasOne(d => d.IdRecipeNavigation).WithMany(p => p.Steps)
                .HasForeignKey(d => d.IdRecipe)
                .HasConstraintName("FK_Steps_Recipes")
                .OnDelete(DeleteBehavior.ClientSetNull);
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.Property(e => e.Email).HasMaxLength(50);
            entity.Property(e => e.Password).HasMaxLength(50);
        });

        modelBuilder.Entity<UserMenu>(entity =>
        {
            entity.ToTable("UserMenu");

            entity.Property(e => e.Category).HasMaxLength(50);
            entity.Property(e => e.Name).HasMaxLength(50);

            entity.HasOne(d => d.IdUserNavigation).WithMany(p => p.UserMenus)
                .HasForeignKey(d => d.IdUser)
                .HasConstraintName("FK_UserMenu_Users")
                .OnDelete(DeleteBehavior.ClientSetNull);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
