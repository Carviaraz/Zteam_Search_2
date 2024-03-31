namespace Zteam.ViewModels
{
    public class CartVM
    {
        public string CartId { get; set; } = null!;

        public string GameId { get; set; } = null!;
        public string GameName { get; set; }

        public double? CdtlQty { get; set; }

        public double? CdtlPrice { get; set; }

        public double? CdtlMoney { get; set; }
    }
}
