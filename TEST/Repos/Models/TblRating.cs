﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BE_Movie_Rcm.Repos.Models;

[Table("tbl_rating")]
public partial class TblRating
{
    [Key]
    [Column("RatingID")]
    public int RatingId { get; set; }

    [Column("User_ID")]
    public int? UserId { get; set; }

    [Column("Movie_ID")]
    public int? MovieId { get; set; }

    public int? Rating { get; set; }

    [ForeignKey("MovieId")]
    [InverseProperty("TblRatings")]
    public virtual TblMovie? Movie { get; set; }

    [ForeignKey("UserId")]
    [InverseProperty("TblRatings")]
    public virtual TblUser? User { get; set; }
}
