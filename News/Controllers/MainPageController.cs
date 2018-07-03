using News.Models.Domin;
using News.Models.Repositoty;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace News.Controllers
{
    public class MainPageController : Controller
    {
        // GET: MainPage
        public ActionResult Main()
        {
            if (Session["User"] != null)
            {
                NewsDB db = new NewsDB();
                string s = Session["User"].ToString();
                ViewBag.user = s;
                int id = db.Tbl_User.Where(x => x.User_Name.Equals(s)).SingleOrDefault().User_id; ;
                ViewBag.id = id;
            }
            return View();
        }

        [HttpGet]
        public ActionResult View_Post(int id)
        {
            Rep_posts post = new Rep_posts();
            Tbl_Posts posts = post.Get_Post(id);
            if (Session["User"] != null)
            {
                NewsDB db = new NewsDB();
                string s = Session["User"].ToString();
                ViewBag.user = s;
                int ids = db.Tbl_User.Where(x => x.User_Name.Equals(s)).SingleOrDefault().User_id; ;
                ViewBag.id = ids;
            }
            return View(posts);
        }
        public ActionResult call_with_us()
        {
            return View();
        }
        [HttpPost]
        public ActionResult call_with_us(Tbl_Call u)
        {
            NewsDB db = new NewsDB();
            Tbl_Call user = new Tbl_Call();
            user.name = u.name;
            user.email = u.email;
            user.request = u.request;
            db.Tbl_Call.Add(user);
            if (Convert.ToBoolean(db.SaveChanges() > 0))
            {
                TempData["message"] = "ثبت درخواست با موفقیت انجام شد";
                TempData["info"] = "done";
            }
            else
            {
                TempData["message"] = "ثبت درخواست با شکست مواجه شد.";
                TempData["info"] = "error";
            }

            return RedirectToAction("Message", "Register");
        }
        public ActionResult about_us()
        {
            return View();
        }
    }
}