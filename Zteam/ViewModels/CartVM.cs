using Zteam.Models;

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
        public Game Game { get; set; } // Include the Game entity
        // Properties for individual cart item details
        public List<CartDtl> CartItems { get; set; }

        // Total price of all items in the cart
        public double TotalCartPrice { get; set; }
        public int CusId { get; set; }

    }
}
