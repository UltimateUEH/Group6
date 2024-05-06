using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Group6_WebApi.Models;

[Table("InvoiceDetail")]
public partial class InvoiceDetail
{
    [Key]
    [Column("detail_id")]
    public int DetailId { get; set; }

    [Column("invoice_id")]
    public int? InvoiceId { get; set; }

    [Column("product_id")]
    public int? ProductId { get; set; }

    [Column("quantity")]
    public int? Quantity { get; set; }

    [Column("price", TypeName = "decimal(10, 2)")]
    public decimal? Price { get; set; }

    [ForeignKey("InvoiceId")]
    [InverseProperty("InvoiceDetails")]
    public virtual Invoice? Invoice { get; set; }

    [ForeignKey("ProductId")]
    [InverseProperty("InvoiceDetails")]
    public virtual Product? Product { get; set; }
}
