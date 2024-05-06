using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Group6_WebApi.Models;

[Table("Tax")]
public partial class Tax
{
    [Key]
    [Column("tax_id")]
    public int TaxId { get; set; }

    [Column("name")]
    [StringLength(255)]
    public string? Name { get; set; }

    [Column("rate", TypeName = "decimal(5, 2)")]
    public decimal? Rate { get; set; }

    [InverseProperty("Tax")]
    public virtual ICollection<DetailInvoice> DetailInvoices { get; set; } = new List<DetailInvoice>();
}
