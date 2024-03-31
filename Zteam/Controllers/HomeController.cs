using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using Zteam.Data;
using Zteam.Models;

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


        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(string userName, string userPass)
        {
            var cus = from c in _db.Customer
                      where c.CusLogin.Equals(userName)
                      && c.CusPass.Equals(userPass)
                      select c;

            if (cus.ToList().Count() == 0)
            {
                TempData["ErrorMessage"] = "หาข้อมูลไม่พบ";
                return RedirectToAction("Index");
            }

            string CusId;
            string CusName;

            foreach (var item in cus)
            {
                CusId = item.CusId;
                CusName = item.CusName;

                HttpContext.Session.SetString("CusId", CusId);
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
        public IActionResult SignUp()
        {
            return View();
        }

    }
}
