using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Group6_MVC.Models;

[Table("Company")]
public partial class Company
{
    [Key]
    [Column("company_id")]
    public int CompanyId { get; set; }

    [Column("tenant_id")]
    public int? TenantId { get; set; }

    [Column("company_name")]
    [StringLength(255)]
    public string? CompanyName { get; set; }

    [Column("company_email")]
    [StringLength(255)]
    public string? CompanyEmail { get; set; }

    [Column("company_address")]
    [StringLength(255)]
    public string? CompanyAddress { get; set; }

    [InverseProperty("Company")]
    public virtual ICollection<Account> Accounts { get; set; } = new List<Account>();

    [InverseProperty("Company")]
    public virtual ICollection<Customer> Customers { get; set; } = new List<Customer>();

    [ForeignKey("TenantId")]
    [InverseProperty("Companies")]
    public virtual Tenant? Tenant { get; set; }
}
