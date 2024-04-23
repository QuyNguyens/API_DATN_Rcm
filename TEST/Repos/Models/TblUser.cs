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

    [StringLength(10)]
    public string? Phone { get; set; }

    [StringLength(100)]
    public string? Avatar { get; set; }

    public int? Gender { get; set; }

    [InverseProperty("User")]
    public virtual ICollection<TblAccessTime> TblAccessTimes { get; set; } = new List<TblAccessTime>();

    [InverseProperty("User")]
    public virtual ICollection<TblFavorite> TblFavorites { get; set; } = new List<TblFavorite>();

    [InverseProperty("User")]
    public virtual ICollection<TblRating> TblRatings { get; set; } = new List<TblRating>();

    [InverseProperty("User")]
    public virtual ICollection<TblStatistic> TblStatistics { get; set; } = new List<TblStatistic>();

    [InverseProperty("User")]
    public virtual ICollection<TblUserMovieAccess> TblUserMovieAccesses { get; set; } = new List<TblUserMovieAccess>();

    [InverseProperty("User")]
    public virtual ICollection<TblUserSub> TblUserSubs { get; set; } = new List<TblUserSub>();
}
