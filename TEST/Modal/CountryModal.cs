using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using BE_Movie_Rcm.Repos.Models;

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

        [InverseProperty("Country")]
        public virtual ICollection<TblCountryMovie> TblCountryMovies { get; set; } = new List<TblCountryMovie>();
    }
}
