using Microsoft.AspNetCore.Mvc;
using Zteam.Data;
using Zteam.Models;
using static Azure.Core.HttpHeader;

namespace Zteam.Controllers
{
    public class LibraryController : Controller
    {
        private readonly ApplicationDbContext _db;
        public LibraryController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult AllGame()
        {
            IEnumerable<Game> game = _db.Game;
            return View(game);
        }
        public IActionResult GameDetail(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Game game = _db.Game.FirstOrDefault(g => g.GameId == id);

            if (game == null)
            {
                return NotFound();
            }

            return View(game);
        }
        [HttpPost]
        public ActionResult Search(string query)
        {
            // Perform dynamic search based on the provided query
            var game = _db.Game.FirstOrDefault(g => g.GameName == query);

            if (game != null)
            {
                // If a game is found, redirect to its GameDetail page
                return RedirectToAction("GameDetail", new { id = game.GameId });
            }

            // If no game is found, display a message or handle it accordingly
            ViewBag.ErrorMessage = "Game not found.";
            return View();
        }
    }

}