using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BE_Movie_Rcm.Modal
{
    public class CountryModal
    {
        [Key]
        [Column("Country_ID")]
        public int CountryId { get; set; }

        [Column("Name_Contry")]
        [StringLength(50)]
        public string? NameContry { get; set; }
    }
}
