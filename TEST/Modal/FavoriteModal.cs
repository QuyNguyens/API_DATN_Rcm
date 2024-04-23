using BE_Movie_Rcm.Repos.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BE_Movie_Rcm.Modal
{
    public class FavoriteModal
    {
        [Column("User_ID")]
        public int? UserId { get; set; }

        [Column("Movie_ID")]
        public int? MovieId { get; set; }
    }
}
