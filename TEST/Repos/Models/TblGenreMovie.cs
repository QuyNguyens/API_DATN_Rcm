using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BE_Movie_Rcm.Repos.Models;

[Table("tbl_genre_movie")]
public partial class TblGenreMovie
{
    [Key]
    [Column("Genre_MovieID")]
    public int GenreMovieId { get; set; }

    [Column("Movie_ID")]
    public int? MovieId { get; set; }

    [Column("Genre_ID")]
    public int? GenreId { get; set; }

    [ForeignKey("GenreId")]
    [InverseProperty("TblGenreMovies")]
    public virtual TblGenre? Genre { get; set; }

    [ForeignKey("MovieId")]
    [InverseProperty("TblGenreMovies")]
    public virtual TblMovie? Movie { get; set; }
}
