using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Group6_MVC.Models;

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
            entity.HasKey(e => e.AccountId).HasName("PK__Account__46A222CD6E4C817C");

            entity.Property(e => e.AccountId).ValueGeneratedNever();

            entity.HasOne(d => d.Company).WithMany(p => p.Accounts).HasConstraintName("FK__Account__company__52593CB8");

            entity.HasOne(d => d.Tenant).WithMany(p => p.Accounts).HasConstraintName("FK__Account__tenant___534D60F1");
        });

        modelBuilder.Entity<Company>(entity =>
        {
            entity.HasKey(e => e.CompanyId).HasName("PK__Company__3E2672352E176FF1");

            entity.Property(e => e.CompanyId).ValueGeneratedNever();

            entity.HasOne(d => d.Tenant).WithMany(p => p.Companies).HasConstraintName("FK__Company__tenant___4BAC3F29");
        });

        modelBuilder.Entity<Customer>(entity =>
        {
            entity.HasKey(e => e.CustomerId).HasName("PK__Customer__CD65CB8551EFA01C");

            entity.Property(e => e.CustomerId).ValueGeneratedNever();

            entity.HasOne(d => d.Company).WithMany(p => p.Customers).HasConstraintName("FK__Customer__compan__4E88ABD4");

            entity.HasOne(d => d.Tenant).WithMany(p => p.Customers).HasConstraintName("FK__Customer__tenant__4F7CD00D");
        });

        modelBuilder.Entity<Invoice>(entity =>
        {
            entity.HasKey(e => e.InvoiceId).HasName("PK__Invoice__F58DFD498BC93769");

            entity.Property(e => e.InvoiceId).ValueGeneratedNever();

            entity.HasOne(d => d.Customer).WithMany(p => p.Invoices).HasConstraintName("FK__Invoice__custome__59063A47");

            entity.HasOne(d => d.Tenant).WithMany(p => p.Invoices).HasConstraintName("FK__Invoice__tenant___59FA5E80");
        });

        modelBuilder.Entity<InvoiceDetail>(entity =>
        {
            entity.HasKey(e => new { e.InvoiceId, e.ProductId }).HasName("PK__InvoiceD__B1FDDA9612106786");

            entity.HasOne(d => d.Invoice).WithMany(p => p.InvoiceDetails)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__InvoiceDe__invoi__5CD6CB2B");

            entity.HasOne(d => d.Product).WithMany(p => p.InvoiceDetails)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__InvoiceDe__produ__5DCAEF64");

            entity.HasOne(d => d.Tenant).WithMany(p => p.InvoiceDetails).HasConstraintName("FK__InvoiceDe__tenan__5EBF139D");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.ProductId).HasName("PK__Product__47027DF5915A60F7");

            entity.Property(e => e.ProductId).ValueGeneratedNever();

            entity.HasOne(d => d.Tenant).WithMany(p => p.Products).HasConstraintName("FK__Product__tenant___5629CD9C");
        });

        modelBuilder.Entity<Tenant>(entity =>
        {
            entity.HasKey(e => e.TenantId).HasName("PK__Tenant__D6F29F3E261E8E5C");

            entity.Property(e => e.TenantId).ValueGeneratedNever();
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
