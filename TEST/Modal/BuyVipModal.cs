using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BE_Movie_Rcm.Modal
{
    public class BuyVipModal
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

    }
}
