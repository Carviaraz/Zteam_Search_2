using System.ComponentModel.DataAnnotations;

namespace Zteam.Models
{
    public class Buying
    {
        [Key]
        public string BuyId { get; set; } = null!;

        public string? DevoperId { get; set; }

        public DateOnly? BuyDate { get; set; }

        public string? StfId { get; set; }

        public string? BuyDocId { get; set; }

        public string? DeveloperName { get; set; }

        public double? BuyQty { get; set; }

    }
}
