using Microsoft.AspNetCore.Mvc;
using Zteam.Data;

namespace Zteam.Controllers
{
    public class StaffController : Controller
    {
        public readonly ApplicationDbContext _db;

        public StaffController(ApplicationDbContext db) { _db = db; }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(string userName, string userPass)
        {
            //Query หาว่ามี Login Password ที่ระบุหรือไม่
            var stf = from s in _db.Staff
                      where s.StfName.Equals(userName)
                          && s.StfPass.Equals(userPass)
                      select s;

            //ถ้าข้อมูลเท่ากับ 0 ให้บอกว่าหาข้อมูลไม่พบ
            if (stf.Count() == 0)
            {
                TempData["ErrorMessage"] = "ระบุผู้ใช้หรือรหัสผ่านไม่ถูกต้อง";
                return View();
            }
            //ถ้าหาข้อมูลพบ ให้เก็บค่าเข้า Session 
            string StfId;
            string StfName;
            string DutyId;
            foreach (var item in stf)
            {
                //อ่านค่าจาก Object เข้าตัวแปร
                StfId = item.StfId;
                StfName = item.StfName;
                DutyId = item.DutyId;
                //เอาค่าจากตัวแปรเข้า Session
                HttpContext.Session.SetString("StfId", StfId);
                HttpContext.Session.SetString("StfName", StfName);
                HttpContext.Session.SetString("DutyId", DutyId);
            }
            //ทำการย้ายไปหน้าที่ต้องการ
            return RedirectToAction("Index");
        }
    }
}
