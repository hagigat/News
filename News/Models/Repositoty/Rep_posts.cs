using News.Models.Domin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace News.Models.Repositoty
{
    public class Rep_posts
    {
        NewsDB db = new NewsDB();
        public IEnumerable<Tbl_Posts> Get_Posts()
        {
            var q = db.Tbl_Posts.OrderByDescending(x => x.Post_Date).ToList();
            return q.AsEnumerable();
        }

        public IEnumerable<Tbl_Posts> Get_Breaking()
        {
            var q = db.Tbl_Posts.Where(x => x.Post_IsBreaking == true).ToList();
            return q.AsEnumerable();
        }

        public IEnumerable<Tbl_Posts> Get_MyPost(int id)
        {
            var q = db.Tbl_Posts.Where(x => x.Post_Userid == id).ToList();
            return q.AsEnumerable();
        }

        public Tbl_Posts Get_Post(int id)
        {
            Tbl_Posts q = db.Tbl_Posts.Where(x => x.Post_id == id).SingleOrDefault();
            return q;
        }

        public IEnumerable<Tbl_Posts> Get_Last_ten()
        {
            var q = db.Tbl_Posts.OrderByDescending(x => x.Post_Date).Take(10).ToList();
            return q.AsEnumerable();
        }
    }
}