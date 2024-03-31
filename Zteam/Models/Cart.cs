using System.ComponentModel.DataAnnotations;

namespace Zteam.Models
{
    public class Cart
    {
        [Key]
        public string CartId { get; set; }
        public string? CusId { get; set; }
        public DateOnly? CartDate { get; set; }
        public double? CartMoney { get; set; }
        public double? CartQty { get; set; }
        public double? CartCf { get; set; }
        public string? CartPay { get; set; }
    }
}
