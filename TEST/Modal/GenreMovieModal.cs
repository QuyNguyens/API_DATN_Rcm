using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using BE_Movie_Rcm.Repos.Models;

namespace BE_Movie_Rcm.Modal
{
    public class GenreMovieModal
    {
        [Key]
        [Column("Genre_MovieID")]
        public int GenreMovieId { get; set; }

        [Column("Movie_ID")]
        public int? MovieId { get; set; }

        [Column("Genre_ID")]
        public int? GenreId { get; set; }

        [ForeignKey("GenreId")]
        [InverseProperty("TblGenreMovies")]
        public virtual TblGenre? Genre { get; set; }

        [ForeignKey("MovieId")]
        [InverseProperty("TblGenreMovies")]
        public virtual TblMovie? Movie { get; set; }
    }
}
