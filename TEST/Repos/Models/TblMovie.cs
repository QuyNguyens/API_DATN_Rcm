﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BE_Movie_Rcm.Repos.Models;

[Table("tbl_movie")]
public partial class TblMovie
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
}