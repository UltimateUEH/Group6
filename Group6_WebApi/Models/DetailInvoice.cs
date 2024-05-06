using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Group6_WebApi.Models;

[Table("Detail_Invoice")]
public partial class DetailInvoice
{
    [Key]
    [Column("detail_id")]
    public int DetailId { get; set; }

    [Column("invoice_id")]
    public int? InvoiceId { get; set; }

    [Column("product_id")]
    public int? ProductId { get; set; }

    [Column("name")]
    [StringLength(255)]
    public string? Name { get; set; }

    [Column("quantity")]
    public int? Quantity { get; set; }

    [Column("price", TypeName = "decimal(10, 2)")]
    public decimal? Price { get; set; }

    [Column("tax_id")]
    public int? TaxId { get; set; }

    [Column("total_amount", TypeName = "decimal(10, 2)")]
    public decimal? TotalAmount { get; set; }

    [Column("discount")]
    public int? Discount { get; set; }

    [Column("note", TypeName = "text")]
    public string? Note { get; set; }

    [ForeignKey("InvoiceId")]
    [InverseProperty("DetailInvoices")]
    public virtual Invoice? Invoice { get; set; }

    [ForeignKey("ProductId")]
    [InverseProperty("DetailInvoices")]
    public virtual Product? Product { get; set; }

    [ForeignKey("TaxId")]
    [InverseProperty("DetailInvoices")]
    public virtual Tax? Tax { get; set; }
}
