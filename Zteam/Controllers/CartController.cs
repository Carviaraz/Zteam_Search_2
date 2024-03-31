using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Zteam.Data;
using Zteam.Models;

public class CartController : Controller
{
    private readonly ApplicationDbContext _context;

    public CartController(ApplicationDbContext context)
    {
        _context = context;
    }

    // Index action to display the contents of the cart
    public IActionResult Index()
    {
        return View(_context.Game.Where(g => _gamesInCart.Contains(g)));
    }

    // Action to add a game to the cart
    public async Task<IActionResult> AddToCart(int gameId)
    {
        var game = await _context.Game.FindAsync(gameId);
        if (game != null)
        {
            _gamesInCart.Add(game);
        }
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

    private List<Game> _gamesInCart = new List<Game>(); // This should be stored in session or database for persistence

    // Ensure you implement proper disposal pattern for DbContext
    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            _context.Dispose();
        }
        base.Dispose(disposing);
    }
}
