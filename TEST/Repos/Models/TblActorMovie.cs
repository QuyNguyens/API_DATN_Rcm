using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BE_Movie_Rcm.Repos.Models;

[Table("tbl_actor_movie")]
public partial class TblActorMovie
{
    [Key]
    [Column("Actor_MovieID")]
    public int ActorMovieId { get; set; }

    [Column("Movie_ID")]
    public int? MovieId { get; set; }

    [Column("Actor_ID")]
    public int? ActorId { get; set; }

    [Column("role")]
    public string? Role { get; set; }

    [ForeignKey("ActorId")]
    [InverseProperty("TblActorMovies")]
    public virtual TblActor? Actor { get; set; }

    [ForeignKey("MovieId")]
    [InverseProperty("TblActorMovies")]
    public virtual TblMovie? Movie { get; set; }
}
