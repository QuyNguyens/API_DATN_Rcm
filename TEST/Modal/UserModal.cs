using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BE_Movie_Rcm.Modal
{
    public class UserModal
    {
        [Key]
        [Column("User_ID")]
        public int UserId { get; set; }

        [StringLength(100)]
        public string Email { get; set; } = null!;

        [StringLength(100)]
        public string? Password { get; set; }

    }
}
