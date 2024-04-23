using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using BE_Movie_Rcm.Repos.Models;

namespace BE_Movie_Rcm.Modal
{
    public class UserResponse
    {
        [Key]
        [Column("User_ID")]
        public int UserId { get; set; }

        [Column("User_name")]
        [StringLength(100)]
        public string? UserName { get; set; }

        [StringLength(100)]
        public string Email { get; set; } = null!;

        [StringLength(100)]
        public string? Password { get; set; }

        public int? Role { get; set; }

        [StringLength(10)]
        public string? Phone { get; set; }

        [StringLength(100)]
        public string? Avatar { get; set; }

        public int? Gender { get; set; }

        public List<UserSubModal>? SubModals { get; set; }

        public List<BuyVipModal>? BuyVips { get; set; }
    }
}
