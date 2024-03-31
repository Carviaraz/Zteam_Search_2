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
        var customerId = HttpContext.Session.GetString("CusId");
        if (customerId == null)
        {
            // Handle scenario where user is not logged in
            // For example, redirect the user to the login page
            return RedirectToAction("Login", "Home");
        }

        var cartItems = _db.CartDtls
            .Include(c => c.Game) // Eager loading: Include the Game entity
            .Where(c => c.CusId == int.Parse(customerId))
            .ToList();

        var cartViewModel = new CartVM
        {
            CartItems = cartItems, // Assign cart items to CartItems property
            CusId = int.Parse(customerId),
            TotalCartPrice = cartItems.Sum(item => (item.CdtlQty ?? 0) * (item.CdtlPrice ?? 0))
        };

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
    public async Task<IActionResult> RemoveFromCart(int cartId)
    {
        var cartItemToRemove = _db.CartDtls.FirstOrDefault(c => c.CartId == cartId);
        if (cartItemToRemove != null)
        {
            _db.CartDtls.Remove(cartItemToRemove);
            await _db.SaveChangesAsync();
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









    [HttpPost]
    public IActionResult BuyItems(int cusId)
    {
        var cartItems = _db.CartDtls.Where(cd => cd.CusId == cusId).ToList();

        foreach (var cartItem in cartItems)
        {
            // Process payment logic here (e.g., charging the user)
            // Once payment is successful, generate a sales report
            GenerateSalesReport(cartItem, cusId);
        }

        // Optionally, clear the cart after successful purchase
        _db.CartDtls.RemoveRange(cartItems);
        _db.SaveChanges();

        return RedirectToAction("Index", "Home"); // Redirect to a suitable page after purchase
    }

    private void GenerateSalesReport(CartDtl cartItem, int cusId)
    {
        // Explicitly load the related Game entity
        _db.Entry(cartItem).Reference(cd => cd.Game).Load();

        // Check if the Game property is not null before accessing its properties
        if (cartItem.Game != null)
        {
            // Generate sales report for the purchased item
            SalesReport salesReport = new SalesReport
            {
                cusId = cusId, // Assuming you have a CustomerId property in the SalesReport model
                PurchaseTime = DateTime.Now,
                GameSold = cartItem.Game.GameName,
                QuantitySold = (int)(cartItem.CdtlQty ?? 0), // Handle null quantity
                TotalRevenue = (decimal)(cartItem.CdtlMoney ?? 0), // Handle null money
                PlatformShare = (decimal)(cartItem.CdtlMoney ?? 0) * 0.1m,
            };
            _db.SalesReports.Add(salesReport);
        }
        else
        {
            // Handle case where Game property is null (optional)
            // Log or handle the situation accordingly
        }
    }
    public IActionResult SalesReport()
    {
        var customerId = HttpContext.Session.GetString("CusId");
        if (customerId == null)
        {
            // Handle scenario where user is not logged in
            // For example, redirect the user to the login page
            return RedirectToAction("Login", "Home");
        }

        var salesReports = _db.SalesReports
            .Where(sr => sr.cusId == int.Parse(customerId))
            .ToList();

        return View(salesReports);
    }


}
