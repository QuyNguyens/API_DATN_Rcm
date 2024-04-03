using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BE_Movie_Rcm.Modal
{
    public class ActorModal
    {
        [Key]
        [Column("Actor_ID")]
        public int ActorId { get; set; }

        [Column("Name_Actor")]
        [StringLength(100)]
        public string? NameActor { get; set; }

        [Column("Age_Actor")]
        public int? AgeActor { get; set; }

        public int? Gender { get; set; }
    }
}
