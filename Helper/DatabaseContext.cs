﻿using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Project.API.Models;

namespace Project.API.Helper;

public partial class DatabaseContext : DbContext
{
    public DatabaseContext()
    {
    }

    public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
    {
    }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<TblCustomer> TblCustomers { get; set; }

    public virtual DbSet<TblDesignation> TblDesignations { get; set; }

    public virtual DbSet<TblEmployee> TblEmployees { get; set; }

    public virtual DbSet<TblMenu> TblMenus { get; set; }

    public virtual DbSet<TblPermission> TblPermissions { get; set; }

    public virtual DbSet<TblProduct> TblProducts { get; set; }

    public virtual DbSet<TblProductImage> TblProductImages { get; set; }

    public virtual DbSet<TblRefreshtoken> TblRefreshtokens { get; set; }

    public virtual DbSet<TblRole> TblRoles { get; set; }

    public virtual DbSet<TblUser> TblUsers { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Category>(entity =>
        {
            entity.ToTable("Category");
        });

        modelBuilder.Entity<TblCustomer>(entity =>
        {
            entity.ToTable("tbl_customer");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.CreditLimit).HasDefaultValueSql("((0))");
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Phone)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<TblDesignation>(entity =>
        {
            entity.HasKey(e => e.Code);

            entity.ToTable("tbl_designation");

            entity.Property(e => e.Code)
                .HasMaxLength(10)
                .HasColumnName("code");
            entity.Property(e => e.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<TblEmployee>(entity =>
        {
            entity.HasKey(e => e.Code);

            entity.ToTable("tbl_employee");

            entity.Property(e => e.Code).HasColumnName("code");
            entity.Property(e => e.Designation)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("designation");
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("email");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("name");
            entity.Property(e => e.Phone)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<TblMenu>(entity =>
        {
            entity.ToTable("tbl_menu");

            entity.Property(e => e.Id)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.LinkName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<TblPermission>(entity =>
        {
            entity.HasKey(e => new { e.RoleId, e.MenuId });

            entity.ToTable("tbl_permission");

            entity.Property(e => e.RoleId)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.MenuId)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<TblProduct>(entity =>
        {
            entity.HasKey(e => e.Code);

            entity.ToTable("tbl_product");

            entity.Property(e => e.Amount).HasColumnType("decimal(18, 3)");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<TblProductImage>(entity =>
        {
            entity.HasKey(e => e.ImageId).HasName("PK__tbl_prod__DC9AC95538B503EA");

            entity.ToTable("tbl_product_images");

            entity.Property(e => e.ImageId)
                .ValueGeneratedNever()
                .HasColumnName("image_id");
            entity.Property(e => e.ProductCode).HasColumnName("product_code");
            entity.Property(e => e.ProductImage)
                .HasColumnType("image")
                .HasColumnName("product_image");

            entity.HasOne(d => d.ProductCodeNavigation).WithMany(p => p.TblProductImages)
                .HasForeignKey(d => d.ProductCode)
                .HasConstraintName("FK__tbl_produ__produ__5CD6CB2B");
        });

        modelBuilder.Entity<TblRefreshtoken>(entity =>
        {
            entity.HasKey(e => e.UserId);

            entity.ToTable("tbl_refreshtoken");

            entity.Property(e => e.UserId)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.TokenId)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<TblRole>(entity =>
        {
            entity.HasKey(e => e.Roleid);

            entity.ToTable("tbl_role");

            entity.Property(e => e.Roleid)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("roleid");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<TblUser>(entity =>
        {
            entity.HasKey(e => e.Userid);

            entity.ToTable("tbl_user");

            entity.Property(e => e.Userid)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("userid");
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.IsActive).HasDefaultValueSql("((1))");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Password)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("password");
            entity.Property(e => e.Role)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
