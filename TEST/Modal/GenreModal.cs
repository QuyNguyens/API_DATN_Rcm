using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BE_Movie_Rcm.Modal
{
    public class GenreModal
    {
        [Key]
        [Column("Genre_ID")]
        public int GenreId { get; set; }

        [Column("Name_Genre")]
        [StringLength(100)]
        public string? NameGenre { get; set; }
    }
}
