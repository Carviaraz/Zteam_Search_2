using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Zteam.Data;
using Zteam.Models;
using Zteam.ViewModels;

public class CartController : Controller
{
    private readonly ApplicationDbContext _db;

    public CartController(ApplicationDbContext db)
    {
        _db = db;
    }

    private List<Game> _gamesInCart = new List<Game>(); // This should be stored in session or database for persistence

    // Index action to display the contents of the cart
    public IActionResult Index()    
    {
        var cartDetails = _db.CartDtls.ToList();
        var gamesInCart = _db.Game.Where(g => cartDetails.Select(c => c.GameId).Contains(g.GameId)).ToList();

        // Combine cart details with game information
        var cartViewModel = cartDetails.Select(cd => new CartVM
        {
            CartId = cd.CartId,
            GameId = cd.GameId,
            CdtlQty = cd.CdtlQty,
            CdtlPrice = cd.CdtlPrice,
            CdtlMoney = cd.CdtlMoney,
            GameName = gamesInCart.FirstOrDefault(g => g.GameId == cd.GameId)?.GameName ?? "Unknown Game"
        });

        return View(cartViewModel);

    }

    // Action to add a game to the cart
    [HttpPost]
    public async Task<IActionResult> AddToCart(int gameId)
    {
        // Get the ID of the logged-in customer from the session
        var customerId = HttpContext.Session.GetString("CusId");
        if (customerId == null)
        {
            // Handle scenario where user is not logged in
            // For example, redirect the user to the login page
            return RedirectToAction("Login", "Home");
        }

        var existingCartItem = _db.CartDtls.FirstOrDefault(c => c.GameId == gameId && c.CusId == int.Parse(customerId));
        if (existingCartItem != null)
        {
            existingCartItem.CdtlQty += 1; // Increment quantity if the game is already in the cart
        }
        else
        {
            var game = await _db.Game.FindAsync(gameId);
            if (game != null)
            {
                var cartDetail = new CartDtl
                {
                    GameId = game.GameId,
                    CdtlQty = 1, // Assuming you're adding one quantity of the game
                    CdtlPrice = game.Price, // Assuming game.Price is the price of the game
                    CdtlMoney = game.Price, // Assuming game.Price is the price of the game
                    CusId = int.Parse(customerId) // Set the customer ID for the cart item
                };

                _db.CartDtls.Add(cartDetail);
            }
        }

        await _db.SaveChangesAsync();

        return RedirectToAction("Index");
    }


    // Action to remove a game from the cart
    public IActionResult RemoveFromCart(int gameId)
    {
        var gameToRemove = _gamesInCart.FirstOrDefault(g => g.GameId == gameId);
        if (gameToRemove != null)
        {
            _gamesInCart.Remove(gameToRemove);
        }
        return RedirectToAction("Index");
    }


    //Ensure you implement proper disposal pattern for DbContext
    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            _db.Dispose();
        }
        base.Dispose(disposing);
    }
}
