using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Group6_MVC.Models;

[Table("Tenant")]
public partial class Tenant
{
    [Key]
    [Column("tenant_id")]
    public int TenantId { get; set; }

    [Column("tenant_name")]
    [StringLength(255)]
    public string? TenantName { get; set; }

    [InverseProperty("Tenant")]
    public virtual ICollection<Account> Accounts { get; set; } = new List<Account>();

    [InverseProperty("Tenant")]
    public virtual ICollection<Company> Companies { get; set; } = new List<Company>();

    [InverseProperty("Tenant")]
    public virtual ICollection<Customer> Customers { get; set; } = new List<Customer>();

    [InverseProperty("Tenant")]
    public virtual ICollection<InvoiceDetail> InvoiceDetails { get; set; } = new List<InvoiceDetail>();

    [InverseProperty("Tenant")]
    public virtual ICollection<Invoice> Invoices { get; set; } = new List<Invoice>();

    [InverseProperty("Tenant")]
    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
