using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BE_Movie_Rcm.Repos.Models;

[Keyless]
[Table("tbl_country_movie")]
public partial class TblCountryMovie
{
    [Column("Movie_ID")]
    public int? MovieId { get; set; }

    [Column("Country_ID")]
    public int? CountryId { get; set; }

    [ForeignKey("CountryId")]
    public virtual TblCountry? Country { get; set; }

    [ForeignKey("MovieId")]
    public virtual TblMovie? Movie { get; set; }
}
