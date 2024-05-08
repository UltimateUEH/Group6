using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Group6_WebApi.Models;
[Keyless]
[Table("InvoiceDetail")]
public partial class InvoiceDetail
{

    [Column("invoice_id")]
    public int? InvoiceId { get; set; }

    [Column("tenant_id")]
    public int? TenantId { get; set; }

    [Column("product_id")]
    public int? ProductId { get; set; }

    [Column("product_name")]
    [StringLength(255)]
    public string? ProductName { get; set; }

    [Column("quantity")]
    public int? Quantity { get; set; }

    [Column("price", TypeName = "decimal(10, 2)")]
    public decimal? Price { get; set; }

    [ForeignKey("InvoiceId")]
    public virtual Invoice? Invoice { get; set; }

    [ForeignKey("ProductId")]
    public virtual Product? Product { get; set; }

    [ForeignKey("TenantId")]
    public virtual Tenant? Tenant { get; set; }
}
