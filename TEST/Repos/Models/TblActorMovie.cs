using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BE_Movie_Rcm.Repos.Models;

[Keyless]
[Table("tbl_actor_movie")]
public partial class TblActorMovie
{
    [Column("Movie_ID")]
    public int? MovieId { get; set; }

    [Column("Actor_ID")]
    public int? ActorId { get; set; }

    [Column("role")]
    [StringLength(100)]
    public string? Role { get; set; }

    [ForeignKey("ActorId")]
    public virtual TblActor? Actor { get; set; }

    [ForeignKey("MovieId")]
    public virtual TblMovie? Movie { get; set; }
}
