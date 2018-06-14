using News.Models.Domin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace News.Controllers
{
    public class RegisterController : Controller
    {
        NewsDB db = new NewsDB();
        // GET: Register
        [HttpGet]
        public ActionResult Login()
        {
            ViewBag.title = "ثبت نام/ورود";
            return View();
        }

        [HttpPost]
        public ActionResult Login(string username,string pass)
        {
            var q = db.Tbl_User.Where(x => x.User_Name.Equals(username) || x.User_Email.Equals(username)).SingleOrDefault();
            if(q!=null)
            {
                if(q.User_Password.Equals(pass))
                {
                    Session["User"] = q.User_Name;
                    return RedirectToAction("User_Profile", "Profile");
                }
                else
                {
                    TempData["message"] = "رمز عبور نادرست است";
                    TempData["info"] = "error";
                }

            }
            else
            {
                TempData["message"] = "نام کاربری یا نشانی الکترونیکی نادرست است";
                TempData["info"] = "error";
            }
            return RedirectToAction("Message");
        }

        [HttpGet]
        public ActionResult Register()
        {
            ViewBag.title = "ثبت نام/ورود";
            return View();
        }

        [HttpPost]
        public ActionResult Register(Tbl_User u)
        {
            Tbl_User user = new Tbl_User();
            user.User_Email = u.User_Email;
            user.User_Name = u.User_Name;
            user.User_Password = u.User_Password;
            user.User_Roll = 1;
            db.Tbl_User.Add(user);
            if (Convert.ToBoolean(db.SaveChanges() > 0))
            {
                TempData["message"] = "ثبت نام با موفقیت انجام شد";
                TempData["info"] = "done";
            }
            else
            {
                TempData["message"] = "ثبت نام با شکست مواجه شد.";
                TempData["info"] = "error";
            }
                
            return RedirectToAction("Message");
        }

        public ActionResult Message()
        {
            ViewBag.message = TempData["message"];
            ViewBag.state = TempData["info"];
            return View();
        }

        public ActionResult Exit()
        {
            Session["User"] = null;
            return RedirectToAction("Main", "MainPage");
        }
    }
}