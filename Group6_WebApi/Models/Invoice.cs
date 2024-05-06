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

    [Column("product_id")]
    public int? ProductId { get; set; }

    [Column("customer_id")]
    public int? CustomerId { get; set; }

    [Column("invoice_date", TypeName = "datetime")]
    public DateTime? InvoiceDate { get; set; }

    [Column("status")]
    [StringLength(50)]
    public string? Status { get; set; }

    [Column("total_amount", TypeName = "decimal(10, 2)")]
    public decimal? TotalAmount { get; set; }

    [Column("product_name")]
    [StringLength(255)]
    public string? ProductName { get; set; }

    [Column("quantity")]
    public int? Quantity { get; set; }

    [Column("price", TypeName = "decimal(10, 2)")]
    public decimal? Price { get; set; }

    [Column("customer_name")]
    [StringLength(255)]
    public string? CustomerName { get; set; }

    [Column("discount")]
    public int? Discount { get; set; }

    [Column("tax_rate")]
    public int? TaxRate { get; set; }

    [Column("detail_id")]
    public int? DetailId { get; set; }

    [Column("note", TypeName = "text")]
    public string? Note { get; set; }

    [ForeignKey("CustomerId")]
    [InverseProperty("Invoices")]
    public virtual Customer? Customer { get; set; }

    [ForeignKey("ProductId")]
    [InverseProperty("Invoices")]
    public virtual Product? Product { get; set; }

    [ForeignKey("TenantId")]
    [InverseProperty("Invoices")]
    public virtual Tenant? Tenant { get; set; }
}
