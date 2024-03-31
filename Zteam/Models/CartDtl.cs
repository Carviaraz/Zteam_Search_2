using System.ComponentModel.DataAnnotations;

namespace Zteam.Models
{
    public class CartDtl
    {
        [Key]
        public int CartId { get; set; }

        public int GameId { get; set; }
        public Game Game { get; set; }

        public double? CdtlQty { get; set; }

        public double? CdtlPrice { get; set; }

        public double? CdtlMoney { get; set; }
        public int CusId { get; set; }
    }
}
