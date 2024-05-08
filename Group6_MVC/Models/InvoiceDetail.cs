using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Group6_MVC.Models;

[PrimaryKey("InvoiceId", "ProductId")]
[Table("InvoiceDetail")]
public partial class InvoiceDetail
{
    [Key]
    [Column("invoice_id")]
    public int InvoiceId { get; set; }

    [Column("tenant_id")]
    public int? TenantId { get; set; }

    [Key]
    [Column("product_id")]
    public int ProductId { get; set; }

    [Column("product_name")]
    [StringLength(255)]
    public string? ProductName { get; set; }

    [Column("quantity")]
    public int? Quantity { get; set; }

    [Column("price", TypeName = "decimal(10, 2)")]
    public decimal? Price { get; set; }

    [ForeignKey("InvoiceId")]
    [InverseProperty("InvoiceDetails")]
    public virtual Invoice Invoice { get; set; } = null!;

    [ForeignKey("ProductId")]
    [InverseProperty("InvoiceDetails")]
    public virtual Product Product { get; set; } = null!;

    [ForeignKey("TenantId")]
    [InverseProperty("InvoiceDetails")]
    public virtual Tenant? Tenant { get; set; }
}
