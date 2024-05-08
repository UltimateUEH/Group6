using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Group6_WebApi.Models;

[Table("Product")]
public partial class Product
{
    [Key]
    [Column("product_id")]
    public int ProductId { get; set; }

    [Column("tenant_id")]
    public int? TenantId { get; set; }

    [Column("product_name")]
    [StringLength(255)]
    public string? ProductName { get; set; }

    [Column("price", TypeName = "decimal(10, 2)")]
    public decimal? Price { get; set; }

    [Column("product_description", TypeName = "text")]
    public string? ProductDescription { get; set; }

    [InverseProperty("Product")]
    public virtual ICollection<InvoiceDetail> InvoiceDetails { get; set; } = new List<InvoiceDetail>();

    [ForeignKey("TenantId")]
    [InverseProperty("Products")]
    public virtual Tenant? Tenant { get; set; }
}
