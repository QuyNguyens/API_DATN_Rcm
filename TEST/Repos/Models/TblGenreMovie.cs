using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BE_Movie_Rcm.Repos.Models;

[Keyless]
[Table("tbl_genre_movie")]
public partial class TblGenreMovie
{
    [Column("Movie_ID")]
    public int? MovieId { get; set; }

    [Column("Genre_ID")]
    public int? GenreId { get; set; }

    [ForeignKey("GenreId")]
    public virtual TblGenre? Genre { get; set; }

    [ForeignKey("MovieId")]
    public virtual TblMovie? Movie { get; set; }
}
