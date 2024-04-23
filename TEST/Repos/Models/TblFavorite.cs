using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BE_Movie_Rcm.Repos.Models;

[Table("tbl_favorite")]
public partial class TblFavorite
{
    [Key]
    [Column("FavoriteID")]
    public int FavoriteId { get; set; }

    [Column("User_ID")]
    public int? UserId { get; set; }

    [Column("Movie_ID")]
    public int? MovieId { get; set; }

    [ForeignKey("MovieId")]
    [InverseProperty("TblFavorites")]
    public virtual TblMovie? Movie { get; set; }

    [ForeignKey("UserId")]
    [InverseProperty("TblFavorites")]
    public virtual TblUser? User { get; set; }
}
