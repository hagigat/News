using News.Models.Domin;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace News.Controllers
{
    public class ProfileController : Controller
    {
        NewsDB db = new NewsDB();
        // GET: Profile
        public ActionResult User_Profile()
        {
            if(Session["User"]==null)
            {
                return RedirectToAction("Login", "Register");
            }
            string s= Session["User"].ToString();
            ViewBag.user = s;
            int id = db.Tbl_User.Where(x => x.User_Name.Equals(s)).SingleOrDefault().User_id; ;
            ViewBag.id = id;
            return View();
        }

        [HttpPost]
        public ActionResult Add_Post(Tbl_Posts p,string Isbreak,HttpPostedFileBase file)
        {
            Tbl_Posts post = new Tbl_Posts();
            string s= Session["User"].ToString();
            int id= db.Tbl_User.Where(x => x.User_Name.Equals(s)).SingleOrDefault().User_id; ;
            post.Post_Description = p.Post_Description;
            if (Isbreak == null)
                post.Post_IsBreaking = false;
            else
                post.Post_IsBreaking = true;
            post.Post_Summery = p.Post_Summery;
            post.Post_Title = p.Post_Title;
            post.Post_Userid = id;
            if(file!=null)
            {
                string path = "~/Content/News/Photo/";
                var filename = Path.GetFileName(file.FileName);
                file.SaveAs(Server.MapPath(Path.Combine(path,filename)));
                post.Post_Image = "../Content/News/Photo/" + filename;
            }
            else
            {
                post.Post_Image = "../Content/News/Photo/no_image.jpg";
            }
            //post.Post_Image = p.Post_Image;
            post.Post_Date = DateTime.Now;
            post.Post_Catid = 1;

            db.Tbl_Posts.Add(post);
            if(Convert.ToBoolean(db.SaveChanges()>0))
            {
                ViewBag.id = id;
                ViewBag.user = s;
                return RedirectToAction("User_Profile");
            }
            else
            {
                TempData["message"] = "عملیات با شکست مواجه شد";
                TempData["info"] = "error";
                return RedirectToAction("Message","Register");
            }
            
        }
    }
}