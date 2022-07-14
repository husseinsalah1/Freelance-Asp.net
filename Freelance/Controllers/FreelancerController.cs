using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using Freelance.Models;
using Microsoft.Ajax.Utilities;
using Action = Antlr.Runtime.Misc.Action;


namespace Project.Controllers
{
    public class FreelancerController : Controller
    {
        private ApplicationDbContext _context;

        public FreelancerController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }
        // GET: Freelancer
        public ActionResult Index()
        {
            return View();
        }

        public IEnumerable<Post> getPosts()
        {
            var posts = _context.Posts.Include(m => m.User).Include(m => m.JopType).Where(m => m.isApproved ==true && m.isSubmitted==false).ToList();


            return posts;
        }

        [HttpGet]
        public ActionResult ViewJop()
        {
            var Post = getPosts();

            return View(Post);
            ;
        }

        [HttpPost]
        public ActionResult SavePost(SavePost savePost)
        {

            var postInPostSubmitted = _context.SavePosts.SingleOrDefault(x => x.PostId == savePost.PostId && x.UserId == savePost.UserId);
            if (postInPostSubmitted == null)
            {
                var postInDb = _context.Posts.SingleOrDefault(x => x.Id == savePost.PostId);
                _context.SavePosts.Add(savePost);
                _context.SaveChanges();
                return Json(new { result = 1, success = true, message = "Successfully" });
            }

            return Json(new { result = 0, success = false, message = "This post is already saved" });

        }

        [HttpPost]
        public ActionResult SubmitPost(PostSubmitted PostSubmitted)
        {
            var postInPostSubmitted = _context.PostSubmitteds.SingleOrDefault(x => x.PostId == PostSubmitted.PostId && x.UserId == PostSubmitted.UserId);
            if(postInPostSubmitted == null)
            {
                var postInDb = _context.Posts.SingleOrDefault(x => x.Id == PostSubmitted.PostId);
                postInDb.NumberOfPorpsalSubmitted++;
                _context.PostSubmitteds.Add(PostSubmitted);
                _context.SaveChanges();
                return Json(new { result = 1, success = true, message = "Successfully" });
            }

            return Json(new { result = 0, success = false, message = "This post is already submitted" });
        }

        public ActionResult SavedPage(int id)
        {
            var SavedPosts = _context.SavePosts.Include(x=>x.Post)
                .Include(x=>x.Post.JopType)
                .Include(x=>x.Post.User)
                .Where(x => x.UserId == id && x.Post.isSubmitted==false).ToList();

            return View(SavedPosts);
        }
        public ActionResult Submitted(int id)
        {
            var submitteds = _context.PostSubmitteds
                .Include(x => x.Post)
                .Include(x => x.Post.JopType)
                .Include(x => x.Post.User)
                .Where(x => x.UserId == id).ToList();

            return View(submitteds);
        }


        public ActionResult Rate(int id)
        {
            var post = _context.Posts.SingleOrDefault(x => x.Id == id);
            if (post == null)
                return Json(new { result = 0 });

            return View(post);
        }
        [HttpPost]
        public ActionResult Rate(Post post)
        {
            var postInDb = _context.Posts.Single(x => x.Id == post.Id);
            if (postInDb == null)
                return Json(new { result = 0 });
            postInDb.NumberOfrates++;
            postInDb.rate += post.rate;
            _context.SaveChanges();
            return RedirectToAction("ViewJop");
        }
        [HttpPost]
        public ActionResult CancelPostSubmitted(PostSubmitted postSubmitted)
        {
            var submitteds = _context.PostSubmitteds
                .Include(x=>x.Post)
                .Single(x => x.PostId == postSubmitted.PostId && x.UserId == postSubmitted.UserId);
            if(submitteds == null)
                return Json(new { result = 0, success = false, message = "Failed" });

            submitteds.Post.NumberOfPorpsalSubmitted--;
            submitteds.Post.isSubmitted = false;
            _context.PostSubmitteds.Remove(submitteds);
            _context.SaveChanges();

            return Json(new { result = 1, success = true, message = "Successfully" });

        }

        public ActionResult CancelPostSaved(SavePost savePost)
        {
            var saved = _context.SavePosts
                .Single(x => x.PostId == savePost.PostId && x.UserId == savePost.UserId);
            if (savePost == null)
                return Json(new { result = 0, success = false, message = "Failed" });

            _context.SavePosts.Remove(saved);
            _context.SaveChanges();

            return Json(new { result = 1, success = true, message = "Successfully" });

        }
    }
        
}
