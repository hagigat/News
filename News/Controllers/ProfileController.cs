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
            if (Session["User"] == null)
            {
                return RedirectToAction("Login", "Register");
            }
            string s = Session["User"].ToString();
            ViewBag.user = s;
            int id = db.Tbl_User.Where(x => x.User_Name.Equals(s)).SingleOrDefault().User_id; ;
            ViewBag.id = id;
            return View();
        }

        [HttpPost]
        public ActionResult Add_Post(Tbl_Posts p, string Isbreak, HttpPostedFileBase file)
        {
            Tbl_Posts post = new Tbl_Posts();
            string s = Session["User"].ToString();
            int id = db.Tbl_User.Where(x => x.User_Name.Equals(s)).SingleOrDefault().User_id; ;
            post.Post_Description = p.Post_Description;
            if (Isbreak == null)
                post.Post_IsBreaking = false;
            else
                post.Post_IsBreaking = true;
            post.Post_Summery = p.Post_Summery;
            post.Post_Title = p.Post_Title;
            post.Post_Userid = id;
            if (file != null)
            {
                string path = "~/Content/News/Photo/";
                var filename = Path.GetFileName(file.FileName);
                file.SaveAs(Server.MapPath(Path.Combine(path, filename)));
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
            if (Convert.ToBoolean(db.SaveChanges() > 0))
            {
                ViewBag.id = id;
                ViewBag.user = s;
                return RedirectToAction("User_Profile");
            }
            else
            {
                TempData["message"] = "عملیات با شکست مواجه شد";
                TempData["info"] = "error";
                return RedirectToAction("Message", "Register");
            }

        }
        public ActionResult GetId(string id)
        {
            return Content("<a class=\"btn btn-danger\"  href=\"../Profile/Delete?delid=" + id + "\">حذف</a>");
        }

        [HttpGet]
        public ActionResult Delete(string delid)
        {
            try
            {
                int idd = Int32.Parse(delid);
                var q = db.Tbl_Posts.Single(x => x.Post_id == idd);
                db.Tbl_Posts.Remove(q);
                db.SaveChanges();
                ViewBag.type = "success";
            }
            catch (Exception)
            {
                ViewBag.type = "failed";
            }
            ViewBag.title = "حذف";
            return View();
        }

        public ActionResult GetEdit(string id)
        {
            int idd = Int32.Parse(id);
            string check= "";
            var q = db.Tbl_Posts.Single(x => x.Post_id == idd);
            if(q.Post_IsBreaking==true)
            {
                check = "checked";
            }
            string body= "<div class=\"form-sec\"><form action=\"Edit_Post\" method=\"post\" class=\"new_post_add\" enctype=\"multipart/form-data\">"+
                "<input style=\"display: none\" type=\"text\" value=\"" + q.Post_id+"\" name=\"id\"/><br /><br />"+
                "<label for=\"txt_title\">عنوان</label><br/>"+
                            "<input type =\"text\" value=\""+q.Post_Title+"\" name=\"title\" /><br/>"+
                            "<label for=\"txt_title\">خلاصه</label><br />"+
                            "<input type =\"text\" value=\""+q.Post_Summery+"\" name=\"summery\" /><br />"+
                            "<label for=\"txt_title\">توضیحات</label><br />"+
                            "<textarea class=\"form-control\" rows=\"9\" id=\"comment\" name=\"description\">"+q.Post_Description+"</textarea><br/><br><br>" +
                            "<label for=\"txt_title\">خبر فوری</label>"+
                            "<input type =\"checkbox\" name=\"Isbreak\" value=\"1\" style=\"width:auto !important; height :auto !important\" "+check+" /><br/><br/>"+
                            "<input class=\"btn_send\" type=\"submit\" value=\"ارسال\" /> <br/> <br/>"+
                        "</form>" +
                    "</div>";
            return Content(body);
        }

        [HttpPost]
        public ActionResult Edit_Post(string id,string title,string summery,string description,string Isbreak)
        {
            try
            {
                int idd = Int32.Parse(id);
                var q = db.Tbl_Posts.Single(x => x.Post_id == idd);
                q.Post_Summery = summery;
                q.Post_Title = title;
                q.Post_Description = description;
                if (Isbreak == "1")
                    q.Post_IsBreaking = true;
                else
                    q.Post_IsBreaking = false;

                db.SaveChanges();
            }
            catch (Exception)
            {

                ViewBag.title = "پیغام";
                ViewBag.type = "error";
            }
            return RedirectToAction("User_Profile");
        }
    }
}