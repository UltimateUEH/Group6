using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Group6_WebApi.Models;

[Table("Company")]
public partial class Company
{
    [Key]
    [Column("company_id")]
    public int CompanyId { get; set; }

    [Column("tenant_id")]
    public int? TenantId { get; set; }

    [Column("name")]
    [StringLength(255)]
    public string? Name { get; set; }

    [Column("address")]
    [StringLength(255)]
    public string? Address { get; set; }

    [Column("contact_info")]
    [StringLength(255)]
    public string? ContactInfo { get; set; }

    [InverseProperty("Company")]
    public virtual ICollection<Account> Accounts { get; set; } = new List<Account>();

    [ForeignKey("TenantId")]
    [InverseProperty("Companies")]
    public virtual Tenant? Tenant { get; set; }
}
