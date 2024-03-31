namespace Zteam.ViewModels
{
    public class CartVM
    {
        public int CartId { get; set; }
        public int GameId { get; set; }
        public double? CdtlQty { get; set; }
        public double? CdtlPrice { get; set; }
        public double? CdtlMoney { get; set; }
        public string GameName { get; set; }
    }
}
