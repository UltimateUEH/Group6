using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Group6_WebApi.Models;

public partial class Group06Context : DbContext
{
    public Group06Context()
    {
    }

    public Group06Context(DbContextOptions<Group06Context> options)
        : base(options)
    {
    }

    public virtual DbSet<Account> Accounts { get; set; }

    public virtual DbSet<Company> Companies { get; set; }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<Invoice> Invoices { get; set; }

    public virtual DbSet<InvoiceDetail> InvoiceDetails { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<Tenant> Tenants { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Name=ConnectionStrings:DefaultConnection");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Account>(entity =>
        {
            entity.HasKey(e => e.AccountId).HasName("PK__Account__46A222CD3F76C11B");

            entity.Property(e => e.AccountId).ValueGeneratedNever();

            entity.HasOne(d => d.Company).WithMany(p => p.Accounts).HasConstraintName("FK__Account__company__4F7CD00D");

            entity.HasOne(d => d.Tenant).WithMany(p => p.Accounts).HasConstraintName("FK__Account__tenant___4E88ABD4");
        });

        modelBuilder.Entity<Company>(entity =>
        {
            entity.HasKey(e => e.CompanyId).HasName("PK__Company__3E2672358DB7E6E0");

            entity.Property(e => e.CompanyId).ValueGeneratedNever();

            entity.HasOne(d => d.Tenant).WithMany(p => p.Companies).HasConstraintName("FK__Company__tenant___4BAC3F29");
        });

        modelBuilder.Entity<Customer>(entity =>
        {
            entity.HasKey(e => e.CustomerId).HasName("PK__Customer__CD65CB85FBCB887D");

            entity.Property(e => e.CustomerId).ValueGeneratedNever();

            entity.HasOne(d => d.Company).WithMany(p => p.Customers).HasConstraintName("FK__Customer__compan__5535A963");

            entity.HasOne(d => d.Tenant).WithMany(p => p.Customers).HasConstraintName("FK__Customer__tenant__5629CD9C");
        });

        modelBuilder.Entity<Invoice>(entity =>
        {
            entity.HasKey(e => e.InvoiceId).HasName("PK__Invoice__F58DFD490AFB7D4B");

            entity.Property(e => e.InvoiceId).ValueGeneratedNever();

            entity.HasOne(d => d.Customer).WithMany(p => p.Invoices).HasConstraintName("FK__Invoice__custome__5AEE82B9");

            entity.HasOne(d => d.Product).WithMany(p => p.Invoices).HasConstraintName("FK__Invoice__product__59FA5E80");

            entity.HasOne(d => d.Tenant).WithMany(p => p.Invoices).HasConstraintName("FK__Invoice__tenant___59063A47");
        });

        modelBuilder.Entity<InvoiceDetail>(entity =>
        {
            entity.HasKey(e => e.DetailId).HasName("PK__InvoiceD__38E9A224305D2688");

            entity.Property(e => e.DetailId).ValueGeneratedNever();

            entity.HasOne(d => d.Invoice).WithMany(p => p.InvoiceDetails).HasConstraintName("FK__InvoiceDe__invoi__60A75C0F");

            entity.HasOne(d => d.Product).WithMany(p => p.InvoiceDetails).HasConstraintName("FK__InvoiceDe__produ__619B8048");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.ProductId).HasName("PK__Product__47027DF58055AEFD");

            entity.Property(e => e.ProductId).ValueGeneratedNever();

            entity.HasOne(d => d.Tenant).WithMany(p => p.Products).HasConstraintName("FK__Product__tenant___52593CB8");
        });

        modelBuilder.Entity<Tenant>(entity =>
        {
            entity.HasKey(e => e.TenantId).HasName("PK__Tenant__D6F29F3E743A1727");

            entity.Property(e => e.TenantId).ValueGeneratedNever();
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
