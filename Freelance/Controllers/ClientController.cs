using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Freelance.Models;
using Freelance.VIewModels;
using Microsoft.Owin.Security.Facebook;

namespace Freelance.Controllers
{
    public class ClientController : Controller
    {

        private ApplicationDbContext _context;

        public ClientController()
        {
            _context = new ApplicationDbContext();

        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }


      
        //============================//
        //Create  Post

        public ActionResult Create()
        {
            var ViewModel = new PostJobType
            {
                JopType = _context.JopTypes.ToList(),
               
            };
            return View(ViewModel);
        }
        [HttpPost]
        public ActionResult Create(Post post)
        {
            if (!ModelState.IsValid)
                return Json(new { result = 0, success = false, message = "Failed" });
            post.CreationDateTime = DateTime.Now;

            _context.Posts.Add(post);
            _context.SaveChanges();
            return Json(new { result = 1, success = true, message = "Successfully" });

        }
        //============================//
        //Show  Post
        public ActionResult Show(int id)
        {
            var posts = _context.Posts.
                Where(p => p.UserId == id).
                Include(x=>x.JopType).
                ToList();

            return View(posts);
        }

        //============================//
        //Delete  Post
        [HttpPost]
        public ActionResult Delete(int id)
        {
            var post = _context.Posts.Single(x => x.Id == id);
            if (post == null)
                return Json(new { result = 0, success = false, message = "Failed" });

            _context.Posts.Remove(post);
            _context.SaveChanges();
            return Json(new { result = 1, success = true, message = "Successfully" });

        }
        //============================//
        //Edit  Post
        public ActionResult EditPost(int id)
        {
            var postInDb = _context.Posts.SingleOrDefault(p => p.Id == id);
            var ViewModel = new PostJobType
            {
                Post = postInDb,
                JopType = _context.JopTypes.ToList(),

            };
            return View(ViewModel);
        }
        [HttpPost]
        public ActionResult EditPost(Post post)
        {
            var postInDb = _context.Posts.Single(p => p.Id == post.Id);
            if (postInDb == null)
                return Json(new { result = 0, success = false, message = "Failed" });
            postInDb.Description = post.Description;
            postInDb.JobBudget = post.JobBudget;
            postInDb.JopTypeId = postInDb.JopTypeId;
            _context.SaveChanges();

            return Json(new { result = 1, success = true, message = "Successfully" });
        }

        public ActionResult SubmittedUsers(int id)
        {
            var posts = _context.PostSubmitteds
                .Include(p => p.Post)
                .Include(u => u.User)
                .Where(x=>x.PostId==id && x.isAccepted == true)
                .ToList();
            if(!posts.Any())
                posts = _context.PostSubmitteds
                    .Include(p => p.Post)
                    .Include(u => u.User)
                    .Where(x => x.PostId == id)
                    .ToList();

            return View(posts);
        }

        [HttpPost]
        public ActionResult AcceptedUser(PostSubmitted postSubmitted)
        {
            var post = _context.PostSubmitteds
                .Single(x => x.PostId == postSubmitted.PostId && x.UserId== postSubmitted.UserId);
            if (post == null)
                return Json(new { result = 0, success = false, message = "Failed" });
            post.isAccepted = true;
            _context.SaveChanges();
            var postsNotAccpeted = _context.PostSubmitteds.Where(x =>x.PostId ==post.Id  &&x.isAccepted == false).ToList();
            foreach (var x in postsNotAccpeted)
            {
                _context.PostSubmitteds.Remove(x);
                _context.SaveChanges();
            }

            var deletefromwall = _context.Posts.SingleOrDefault(x => x.Id == post.PostId);
            deletefromwall.isSubmitted = true;
            _context.SaveChanges();
            return Json(new { result = 1, success = true, message = "Successfully" });

        }


        [HttpPost]
        public ActionResult RefusedUser(PostSubmitted postSubmitted)
        {
            var postInDb = _context.PostSubmitteds
                .Single(x => x.PostId == postSubmitted.PostId
                             && x.UserId == postSubmitted.UserId);
            if (postInDb == null)
                return Json(new { result = 0, success = false, message = "Failed" });

            _context.PostSubmitteds.Remove(postInDb);
            _context.SaveChanges();
            return Json(new { result = 1, success = true, message = "Successfully" });
        }

        public ActionResult ReceivedProposal(int id)
        {
            var getPostFromposts = _context.Posts.Where(x => x.UserId == id && x.isSubmitted==false).ToList();
            var sub = new List<PostSubmitted>();
            foreach (var x in getPostFromposts)
            {
               sub.AddRange(_context.PostSubmitteds.Where(q => q.PostId == x.Id).Include(t => t.User).Include(w => w.Post).ToList());
            }

            return View(sub);
        }

    }
}