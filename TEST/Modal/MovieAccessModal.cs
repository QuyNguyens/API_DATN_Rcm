using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BE_Movie_Rcm.Repos.Models;

namespace BE_Movie_Rcm.Modal
{
    public class MovieAccessModal
    {

        [Column("User_ID")]
        public int? UserId { get; set; }

        [Column("Movie_ID")]
        public int? MovieId { get; set; }
    }
}
