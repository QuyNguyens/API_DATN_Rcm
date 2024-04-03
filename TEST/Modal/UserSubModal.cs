using System.ComponentModel.DataAnnotations.Schema;
using BE_Movie_Rcm.Repos.Models;

namespace BE_Movie_Rcm.Modal
{
    public class UserSubModal
    {
        [Column("User_ID")]
        public int? UserId { get; set; }

        [Column("Start_Day", TypeName = "datetime")]
        public DateTime? StartDay { get; set; }

        [Column("End_Day", TypeName = "datetime")]
        public DateTime? EndDay { get; set; }

        public int? Status { get; set; }

        [ForeignKey("UserId")]
        public virtual TblUser? User { get; set; }
    }
}
