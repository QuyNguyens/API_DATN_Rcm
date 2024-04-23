using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BE_Movie_Rcm.Repos.Models;

[Table("tbl_actor")]
public partial class TblActor
{
    [Key]
    [Column("Actor_ID")]
    public int ActorId { get; set; }

    [Column("Name_Actor")]
    [StringLength(100)]
    public string? NameActor { get; set; }

    [Column("Age_Actor")]
    public int? AgeActor { get; set; }

    public int? Gender { get; set; }

    [InverseProperty("Actor")]
    public virtual ICollection<TblActorMovie> TblActorMovies { get; set; } = new List<TblActorMovie>();
}
