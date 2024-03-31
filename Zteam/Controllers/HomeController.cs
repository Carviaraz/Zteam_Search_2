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
        [ValidateAntiForgeryToken]
        public IActionResult Login(string userName, string userPass)
        {
            var cus = from c in _db.Customer
                      where c.CusName.Equals(userName)
                      && c.CusPass.Equals(userPass)
                      select c;

            if (cus.ToList().Count() == 0)
            {
                TempData["ErrorMessage"] = "หาข้อมูลไม่พบ";
                return RedirectToAction("Index");
            }

            int CusId;
            string CusName;

            foreach (var item in cus)
            {
                CusId = item.CusId;
                CusName = item.CusName;

                HttpContext.Session.SetString("CusId", CusId.ToString());
                HttpContext.Session.SetString("CusName", CusName);

                var theRecord = _db.Customer.Find(CusId);
                theRecord.LastLogin = DateOnly.FromDateTime(DateTime.Now);

                _db.Entry(theRecord).State = EntityState.Modified;
            }

            _db.SaveChanges();

            return RedirectToAction("Index", "Home");

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
