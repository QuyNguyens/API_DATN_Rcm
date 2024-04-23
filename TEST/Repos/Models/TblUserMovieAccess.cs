using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BE_Movie_Rcm.Repos.Models;

[Table("tbl_user_movieAccess")]
public partial class TblUserMovieAccess
{
    [Key]
    [Column("Movie_AccessID")]
    public int MovieAccessId { get; set; }

    [Column("User_ID")]
    public int? UserId { get; set; }

    [Column("Movie_ID")]
    public int? MovieId { get; set; }

    [ForeignKey("MovieId")]
    [InverseProperty("TblUserMovieAccesses")]
    public virtual TblMovie? Movie { get; set; }

    [ForeignKey("UserId")]
    [InverseProperty("TblUserMovieAccesses")]
    public virtual TblUser? User { get; set; }
}
