using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Group6_WebApi.Models;

[Table("Customer")]
public partial class Customer
{
    [Key]
    [Column("customer_id")]
    public int CustomerId { get; set; }

    [Column("customer_name")]
    [StringLength(255)]
    public string? CustomerName { get; set; }

    [Column("invoice_id")]
    public int? InvoiceId { get; set; }

    [Column("company_id")]
    public int? CompanyId { get; set; }

    [Column("tenant_id")]
    public int? TenantId { get; set; }

    [ForeignKey("CompanyId")]
    [InverseProperty("Customers")]
    public virtual Company? Company { get; set; }

    [InverseProperty("Customer")]
    public virtual ICollection<Invoice> Invoices { get; set; } = new List<Invoice>();

    [ForeignKey("TenantId")]
    [InverseProperty("Customers")]
    public virtual Tenant? Tenant { get; set; }
}
