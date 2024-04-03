﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BE_Movie_Rcm.Repos.Models;

[Keyless]
[Table("tbl_user_movieAccess")]
public partial class TblUserMovieAccess
{
    [Column("User_ID")]
    public int? UserId { get; set; }

    [Column("Movie_ID")]
    public int? MovieId { get; set; }

    [ForeignKey("MovieId")]
    public virtual TblMovie? Movie { get; set; }

    [ForeignKey("UserId")]
    public virtual TblUser? User { get; set; }
}