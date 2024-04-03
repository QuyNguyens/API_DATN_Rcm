using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BE_Movie_Rcm.Repos.Models;

[Keyless]
[Table("tbl_user_subs")]
public partial class TblUserSub
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
