using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Group6_WebApi.Models;

[Table("Account")]
public partial class Account
{
    [Key]
    [Column("account_id")]
    public int AccountId { get; set; }

    [Column("tenant_id")]
    public int? TenantId { get; set; }

    [Column("company_id")]
    public int? CompanyId { get; set; }

    [Column("username")]
    [StringLength(255)]
    public string? Username { get; set; }

    [Column("password")]
    [StringLength(255)]
    public string? Password { get; set; }

    [Column("email")]
    [StringLength(255)]
    public string? Email { get; set; }

    [ForeignKey("CompanyId")]
    [InverseProperty("Accounts")]
    public virtual Company? Company { get; set; }

    [ForeignKey("TenantId")]
    [InverseProperty("Accounts")]
    public virtual Tenant? Tenant { get; set; }
}
