using System.ComponentModel.DataAnnotations;

namespace Zteam.Models
{
    public class BuyDtl
    {
        [Key]
        public string BuyId { get; set; } = null!;

        public string GameId { get; set; } = null!;

        public double? BdtlQty { get; set; }

        public double? BdtlPrice { get; set; }

    }
}
