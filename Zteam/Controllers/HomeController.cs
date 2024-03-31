using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Security.Claims;
using Zteam.Data;
using Zteam.Models;
using Zteam.ViewModels;

namespace Zteam.Controllers
{
    public class HomeController : Controller
    {
        public readonly ApplicationDbContext _db;

        public HomeController(ApplicationDbContext db) { _db = db; }


        //private readonly ILogger<HomeController> _logger;
        //public HomeController(ILogger<HomeController> logger)
        //{
        //    _logger = logger;
        //}





        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


        public async Task<IActionResult> Login()
        {
            return View();
        }

        [HttpPost]
        
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _db.Customer.FirstOrDefaultAsync(c => c.CusName == model.Username && c.CusPass == model.Password);

                if (user != null)
                {
                    // Authentication successful, create authentication cookie or JWT token
                    var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.CusName)
                    // Add more claims as needed
                };

                    var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    var principal = new ClaimsPrincipal(identity);

                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

                    // Redirect to a protected resource or home page
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid username or password.");
                }
            }

            // If ModelState is not valid or authentication fails, redisplay the login form with validation errors
            return View(model);

        }
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(Customer model)
        {
            if (ModelState.IsValid)
            {
                // Check if username or email already exists
                if (_db.Customer.Any(c => c.CusName == model.CusName))
                {
                    ModelState.AddModelError("CusName", "Username already exists.");
                }

                if (_db.Customer.Any(c => c.CusEmail == model.CusEmail))
                {
                    ModelState.AddModelError("CusEmail", "Email address already exists.");
                }

                // If no duplicate username or email found, proceed with registration
                if (!ModelState.IsValid)
                {
                    // Redisplay the form with validation errors
                    return View(model);
                }

                var customer = new Customer
                {
                    CusName = model.CusName,
                    CusPass = model.CusPass,
                    CusEmail = model.CusEmail,
                    StartDate = DateOnly.FromDateTime(DateTime.UtcNow)
                };

                _db.Customer.Add(customer);
                await _db.SaveChangesAsync(); // Use async version
                TempData["SuccessMessage"] = "Registration successful. You can now login.";
                return RedirectToAction("Login", "Home");
            }

            // If ModelState is not valid, redisplay the form with validation errors
            return View(model);
        }
    }
}
