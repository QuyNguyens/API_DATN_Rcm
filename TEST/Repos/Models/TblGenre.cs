using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BE_Movie_Rcm.Repos.Models;

[Table("tbl_genre")]
public partial class TblGenre
{
    [Key]
    [Column("Genre_ID")]
    public int GenreId { get; set; }

    [Column("Name_Genre")]
    [StringLength(100)]
    public string? NameGenre { get; set; }
}
