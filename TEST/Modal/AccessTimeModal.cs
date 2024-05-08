using System.ComponentModel.DataAnnotations.Schema;

namespace BE_Movie_Rcm.Modal
{
    public class AccessTimeModal
    {
        [Column("User_ID")]
        public int? UserId { get; set; }

        [Column("Country_ID")]
        public string? CountryId { get; set; }

        [Column("Access_time")]
        public int? AccessTime { get; set; }

        public string nameCountry { get; set; }

    }
}
