using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BE_Movie_Rcm.Repos.Models;

[Table("tbl_access_time")]
public partial class TblAccessTime
{
    [Key]
    [Column("Access_time_Id")]
    public int AccessTimeId { get; set; }

    [Column("User_ID")]
    public int? UserId { get; set; }

    [Column("Country_ID")]
    public int? CountryId { get; set; }

    [Column("Access_time")]
    public int? AccessTime { get; set; }

    [ForeignKey("CountryId")]
    [InverseProperty("TblAccessTimes")]
    public virtual TblCountry? Country { get; set; }

    [ForeignKey("UserId")]
    [InverseProperty("TblAccessTimes")]
    public virtual TblUser? User { get; set; }
}
