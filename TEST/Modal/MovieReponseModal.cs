using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BE_Movie_Rcm.Modal
{
    public class MovieReponseModal
    {
        [Key]
        [Column("Movie_ID")]
        public int MovieId { get; set; }

        [StringLength(100)]
        public string? Title { get; set; }

        [Column("URLS", TypeName = "text")]
        public string? Urls { get; set; }

        [Column(TypeName = "text")]
        public string? Poster { get; set; }

        [Column("Vote_average")]
        public double? VoteAverage { get; set; }

        [Column("Vote_count")]
        public int? VoteCount { get; set; }
    }
}
