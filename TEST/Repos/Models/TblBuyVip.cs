using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BE_Movie_Rcm.Repos.Models;

[Table("tbl_buy_vip")]
public partial class TblBuyVip
{
    [Key]
    [Column("buy_vip_id")]
    public int BuyVipId { get; set; }

    [Column("nameVip")]
    [StringLength(50)]
    public string? NameVip { get; set; }

    [Column("titleVip")]
    [StringLength(100)]
    public string? TitleVip { get; set; }

    [Column("priceVip")]
    public double? PriceVip { get; set; }

    [Column("subPriceVip")]
    public double? SubPriceVip { get; set; }

    [InverseProperty("IsTypeNavigation")]
    public virtual ICollection<TblUserSub> TblUserSubs { get; set; } = new List<TblUserSub>();
}
