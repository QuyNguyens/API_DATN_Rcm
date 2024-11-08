﻿using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using BE_Movie_Rcm.Modal;
using BE_Movie_Rcm.Repos.Models;

namespace TEST.Modal
{
    public class MovieModal
    {
        [Key]
        [Column("Movie_ID")]
        public int MovieId { get; set; }

        [StringLength(100)]
        public string? Title { get; set; }

        public string? Descriptions { get; set; }

        [Column("URLS", TypeName = "text")]
        public string? Urls { get; set; }

        [Column(TypeName = "text")]
        public string? Poster { get; set; }

        [Column("Original_language")]
        [StringLength(20)]
        [Unicode(false)]
        public string? OriginalLanguage { get; set; }

        [StringLength(10)]
        [Unicode(false)]
        public string? Status { get; set; }

        [Column("Vote_average")]
        public double? VoteAverage { get; set; }

        [Column("Vote_count")]
        public int? VoteCount { get; set; }

        [Column("isType")]
        public int? IsType { get; set; }

        public List<string> Genres { get; set; }
        public List<ActorModal> Actors { get; set; }
        public List<CountryModal> Countrys { get; set; }

    }
}
