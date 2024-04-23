using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BE_Movie_Rcm.Repos.Models;

[Table("tbl_statistic")]
public partial class TblStatistic
{
    [Key]
    [Column("Statistic_ID")]
    public int StatisticId { get; set; }

    [Column("User_ID")]
    public int? UserId { get; set; }

    [Column("Movie_ID")]
    public int? MovieId { get; set; }

    [Column("Access_duration")]
    public double? AccessDuration { get; set; }

    [Column("Time_access")]
    public TimeSpan? TimeAccess { get; set; }

    [Column("Date_access", TypeName = "datetime")]
    public DateTime? DateAccess { get; set; }

    [ForeignKey("MovieId")]
    [InverseProperty("TblStatistics")]
    public virtual TblMovie? Movie { get; set; }

    [ForeignKey("UserId")]
    [InverseProperty("TblStatistics")]
    public virtual TblUser? User { get; set; }
}
