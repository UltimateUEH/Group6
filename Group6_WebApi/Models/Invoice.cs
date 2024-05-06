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

    [Column("invoice_date", TypeName = "datetime")]
    public DateTime? InvoiceDate { get; set; }

    [Column("total_amount", TypeName = "decimal(10, 2)")]
    public decimal? TotalAmount { get; set; }

    [InverseProperty("Invoice")]
    public virtual ICollection<DetailInvoice> DetailInvoices { get; set; } = new List<DetailInvoice>();

    [ForeignKey("TenantId")]
    [InverseProperty("Invoices")]
    public virtual Tenant? Tenant { get; set; }
}
