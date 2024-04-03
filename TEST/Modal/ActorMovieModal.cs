using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using BE_Movie_Rcm.Repos.Models;

namespace BE_Movie_Rcm.Modal
{
    public class ActorMovieModal
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
}
