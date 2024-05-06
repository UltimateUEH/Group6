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

    public virtual DbSet<DetailInvoice> DetailInvoices { get; set; }

    public virtual DbSet<Invoice> Invoices { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<Tax> Taxes { get; set; }

    public virtual DbSet<Tenant> Tenants { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Name=ConnectionStrings:DefaultConnection");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Account>(entity =>
        {
            entity.HasKey(e => e.AccountId).HasName("PK__Account__46A222CD442DB334");

            entity.Property(e => e.AccountId).ValueGeneratedNever();

            entity.HasOne(d => d.Company).WithMany(p => p.Accounts).HasConstraintName("FK__Account__company__4F7CD00D");

            entity.HasOne(d => d.Tenant).WithMany(p => p.Accounts).HasConstraintName("FK__Account__tenant___4E88ABD4");
        });

        modelBuilder.Entity<Company>(entity =>
        {
            entity.HasKey(e => e.CompanyId).HasName("PK__Company__3E26723594932133");

            entity.Property(e => e.CompanyId).ValueGeneratedNever();

            entity.HasOne(d => d.Tenant).WithMany(p => p.Companies).HasConstraintName("FK__Company__tenant___4BAC3F29");
        });

        modelBuilder.Entity<DetailInvoice>(entity =>
        {
            entity.HasKey(e => e.DetailId).HasName("PK__Detail_I__38E9A22406349550");

            entity.Property(e => e.DetailId).ValueGeneratedNever();

            entity.HasOne(d => d.Invoice).WithMany(p => p.DetailInvoices).HasConstraintName("FK__Detail_In__invoi__59FA5E80");

            entity.HasOne(d => d.Product).WithMany(p => p.DetailInvoices).HasConstraintName("FK__Detail_In__produ__5AEE82B9");

            entity.HasOne(d => d.Tax).WithMany(p => p.DetailInvoices).HasConstraintName("FK__Detail_In__tax_i__5BE2A6F2");
        });

        modelBuilder.Entity<Invoice>(entity =>
        {
            entity.HasKey(e => e.InvoiceId).HasName("PK__Invoice__F58DFD49129FC0A3");

            entity.Property(e => e.InvoiceId).ValueGeneratedNever();

            entity.HasOne(d => d.Tenant).WithMany(p => p.Invoices).HasConstraintName("FK__Invoice__tenant___5535A963");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.ProductId).HasName("PK__Product__47027DF5F3E7F9EC");

            entity.Property(e => e.ProductId).ValueGeneratedNever();

            entity.HasOne(d => d.Tenant).WithMany(p => p.Products).HasConstraintName("FK__Product__tenant___52593CB8");
        });

        modelBuilder.Entity<Tax>(entity =>
        {
            entity.HasKey(e => e.TaxId).HasName("PK__Tax__129B8670C0BA286F");

            entity.Property(e => e.TaxId).ValueGeneratedNever();
        });

        modelBuilder.Entity<Tenant>(entity =>
        {
            entity.HasKey(e => e.TenantId).HasName("PK__Tenant__D6F29F3E1CBCA3CD");

            entity.Property(e => e.TenantId).ValueGeneratedNever();
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
