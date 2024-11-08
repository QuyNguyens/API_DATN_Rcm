﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BE_Movie_Rcm.Repos.Models;

[Table("tbl_country")]
public partial class TblCountry
{
    [Key]
    [Column("Country_ID")]
    [StringLength(15)]
    public string CountryId { get; set; } = null!;

    [Column("Name_Contry")]
    [StringLength(50)]
    public string? NameContry { get; set; }

    [InverseProperty("Country")]
    public virtual ICollection<TblAccessTime> TblAccessTimes { get; set; } = new List<TblAccessTime>();

    [InverseProperty("Country")]
    public virtual ICollection<TblCountryMovie> TblCountryMovies { get; set; } = new List<TblCountryMovie>();
}
