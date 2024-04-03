using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using BE_Movie_Rcm.Repos.Models;

namespace BE_Movie_Rcm.Modal
{
    public class GenreMovieModal
    {
        [Column("Movie_ID")]
        public int? MovieId { get; set; }

        [Column("Genre_ID")]
        public int? GenreId { get; set; }

        [ForeignKey("GenreId")]
        public virtual TblGenre? Genre { get; set; }

        [ForeignKey("MovieId")]
        public virtual TblMovie? Movie { get; set; }
    }
}
