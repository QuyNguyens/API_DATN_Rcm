using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BE_Movie_Rcm.Repos.Models;

[Table("tbl_user")]
public partial class TblUser
{
    [Key]
    [Column("User_ID")]
    public int UserId { get; set; }

    [Column("User_name")]
    [StringLength(100)]
    public string? UserName { get; set; }

    [StringLength(100)]
    public string Email { get; set; } = null!;

    [StringLength(100)]
    public string? Password { get; set; }

    [Column("User_age")]
    public int? UserAge { get; set; }

    public int? Role { get; set; }

    [Column("Upgrade_status")]
    public int? UpgradeStatus { get; set; }

    [StringLength(10)]
    public string? Phone { get; set; }

    public double? Avatar { get; set; }

    public int? Gender { get; set; }
}
