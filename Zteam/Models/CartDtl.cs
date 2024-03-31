using System.ComponentModel.DataAnnotations;

namespace Zteam.Models
{
    public class CartDtl
    {
        [Key]
        public string CartId { get; set; } = null!;

        public string GameId { get; set; } = null!;

        public double? CdtlQty { get; set; }

        public double? CdtlPrice { get; set; }

        public double? CdtlMoney { get; set; }
    }
}
