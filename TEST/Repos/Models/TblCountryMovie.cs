using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BE_Movie_Rcm.Repos.Models;

[Table("tbl_country_movie")]
public partial class TblCountryMovie
{
    [Key]
    [Column("Country_MovieID")]
    public int CountryMovieId { get; set; }

    [Column("Movie_ID")]
    public int? MovieId { get; set; }

    [Column("Country_ID")]
    public int? CountryId { get; set; }

    [ForeignKey("CountryId")]
    [InverseProperty("TblCountryMovies")]
    public virtual TblCountry? Country { get; set; }

    [ForeignKey("MovieId")]
    [InverseProperty("TblCountryMovies")]
    public virtual TblMovie? Movie { get; set; }
}
