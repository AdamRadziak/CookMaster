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

    public virtual DbSet<RecipesDetail> RecipesDetails { get; set; }

    public virtual DbSet<Step> Steps { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserMenu> UserMenus { get; set; }

    public virtual DbSet<UserMenuRecipe> UserMenuRecipes { get; set; }

    //    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    //#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
    //        => optionsBuilder.UseSqlServer("Server=localhost,1433;Database=CookMasterDB;User Id=sa;Password=Your@Password;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Photo>(entity =>
        {
            entity.Property(e => e.FileName).HasMaxLength(50);
            entity.Property(e => e.FilePath).HasMaxLength(120);
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.Property(e => e.Amount).HasMaxLength(50);
            entity.Property(e => e.Category).HasMaxLength(50);
            entity.Property(e => e.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<Recipe>(entity =>
        {
            entity.Property(e => e.Description).HasMaxLength(100);
            entity.Property(e => e.Name).HasMaxLength(50);
            entity.Property(e => e.PrepareTime)
                .HasMaxLength(10)
                .IsFixedLength();
        });

        modelBuilder.Entity<RecipesDetail>(entity =>
        {
            entity.HasNoKey();

            entity.HasOne(d => d.IdPhotoNavigation).WithMany()
                .HasForeignKey(d => d.IdPhoto)
                .HasConstraintName("FK_RecipesDetails_Photos");

            entity.HasOne(d => d.IdProductNavigation).WithMany()
                .HasForeignKey(d => d.IdProduct)
                .HasConstraintName("FK_RecipesDetails_Products");

            entity.HasOne(d => d.IdRecipeNavigation).WithMany()
                .HasForeignKey(d => d.IdRecipe)
                .HasConstraintName("FK_RecipesDetails_Recipes");

            entity.HasOne(d => d.IdStepNavigation).WithMany()
                .HasForeignKey(d => d.IdStep)
                .HasConstraintName("FK_RecipesDetails_Steps");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.Property(e => e.Email).HasMaxLength(50);
            entity.Property(e => e.Password).HasMaxLength(50);
        });

        modelBuilder.Entity<UserMenu>(entity =>
        {
            entity.ToTable("UserMenu");

            entity.Property(e => e.Name).HasMaxLength(50);
            entity.Property(e => e.RecipeCategory).HasMaxLength(50);
        });

        modelBuilder.Entity<UserMenuRecipe>(entity =>
        {
            entity.HasNoKey();

            entity.HasOne(d => d.IdRecipeNavigation).WithMany()
                .HasForeignKey(d => d.IdRecipe)
                .HasConstraintName("FK_UserMenuRecipes_Recipes");

            entity.HasOne(d => d.IdUserNavigation).WithMany()
                .HasForeignKey(d => d.IdUser)
                .HasConstraintName("FK_UserMenuRecipes_Users");

            entity.HasOne(d => d.IdUserMenuNavigation).WithMany()
                .HasForeignKey(d => d.IdUserMenu)
                .HasConstraintName("FK_UserMenuRecipes_UserMenu");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
