using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Group6_WebApi.Models;

[Table("Invoice")]
public partial class Invoice
{
    [Key]
    [Column("invoice_id")]
    public int InvoiceId { get; set; }

    [Column("tenant_id")]
    public int? TenantId { get; set; }

    [Column("customer_id")]
    public int? CustomerId { get; set; }

    [Column("invoice_date", TypeName = "datetime")]
    public DateTime? InvoiceDate { get; set; }

    [Column("invoice_status")]
    [StringLength(50)]
    public string? InvoiceStatus { get; set; }

    [Column("total_amount", TypeName = "decimal(10, 2)")]
    public decimal? TotalAmount { get; set; }

    [Column("customer_name")]
    [StringLength(255)]
    public string? CustomerName { get; set; }

    [Column("discount")]
    public int? Discount { get; set; }

    [Column("tax_rate")]
    public int? TaxRate { get; set; }

    [Column("note", TypeName = "text")]
    public string? Note { get; set; }

    [ForeignKey("CustomerId")]
    [InverseProperty("Invoices")]
    public virtual Customer? Customer { get; set; }

    [InverseProperty("Invoice")]
    public virtual ICollection<InvoiceDetail> InvoiceDetails { get; set; } = new List<InvoiceDetail>();

    [ForeignKey("TenantId")]
    [InverseProperty("Invoices")]
    public virtual Tenant? Tenant { get; set; }
}
