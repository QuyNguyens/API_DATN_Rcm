using System;
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
    public int CountryId { get; set; }

    [Column("Name_Contry")]
    [StringLength(50)]
    public string? NameContry { get; set; }
}
