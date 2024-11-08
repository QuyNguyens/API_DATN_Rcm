﻿using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BE_Movie_Rcm.Modal
{
    public class MovieCreateModal
    {
        [Key]
        [Column("Movie_ID")]
        public int MovieId { get; set; }

        [StringLength(100)]
        public string Title { get; set; } = null!;

        public int IsType { get; set; }

        public string? Descriptions { get; set; }

        [Column("URLS", TypeName = "text")]
        public string? Urls { get; set; }

        [Column(TypeName = "text")]
        public string? Poster { get; set; }

        [Column("Original_language")]
        [StringLength(20)]
        [Unicode(false)]
        public string? OriginalLanguage { get; set; }
    }
}
