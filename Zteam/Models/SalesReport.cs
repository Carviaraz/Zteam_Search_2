namespace Zteam.Models
{
    public class SalesReport
    {
        public int Id { get; set; }  // Assuming you have an ID property
        public DateTime PurchaseTime { get; set; }
        public string GameSold { get; set; }
        public int QuantitySold { get; set; }
        public decimal TotalRevenue { get; set; }
        public decimal PlatformShare { get; set; }
        public int cusId { get; internal set; }
    }
}
