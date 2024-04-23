using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BE_Movie_Rcm.Repos.Models;

[Table("tbl_user_subs")]
public partial class TblUserSub
{
    [Key]
    [Column("User_SubID")]
    public int UserSubId { get; set; }

    [Column("User_ID")]
    public int? UserId { get; set; }

    [Column("Start_Day", TypeName = "datetime")]
    public DateTime? StartDay { get; set; }

    [Column("End_Day", TypeName = "datetime")]
    public DateTime? EndDay { get; set; }

    public int? Status { get; set; }

    [Column("isType")]
    public int? IsType { get; set; }

    [ForeignKey("IsType")]
    [InverseProperty("TblUserSubs")]
    public virtual TblBuyVip? IsTypeNavigation { get; set; }

    [ForeignKey("UserId")]
    [InverseProperty("TblUserSubs")]
    public virtual TblUser? User { get; set; }
}
