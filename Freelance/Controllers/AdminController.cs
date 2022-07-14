using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Freelance.Models;
using System.Data.Entity;
using Freelance.VIewModels;

namespace Freelance.Controllers
{
    public class AdminController : Controller
    {
        private ApplicationDbContext _context;

        public AdminController()
        {
            _context = new ApplicationDbContext();

        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }
        // GET: Admin
        public ActionResult ShowUsers()
        {

            var User = getUsers();
            return View(User);

        }

        public ActionResult index()
        {
            return View();
        }

        public IEnumerable<User> getUsers()
        {
            var users = _context.Users.Where(x=>x.RoleId != 1).Include(x=>x.Role).ToList();
            return users;
        }

          [HttpPost]
        public ActionResult Delete(int id)
        {
            var user = _context.Users.Single(x => x.Id == id);
            if (user == null)
                return Json(new { result = 0, success = false, message = "Failed" });

            var savePost = _context.SavePosts.Where(x => x.UserId == user.Id).ToList();
            var subPost = _context.PostSubmitteds.Where(x => x.UserId == user.Id).ToList();
            if (savePost.Any())
            {
                foreach(var post in savePost)
                {
                    _context.SavePosts.Remove(post);
                    _context.SaveChanges();
                }
            }

            if (savePost.Any())
            {
                foreach (var post in subPost)
                {
                    _context.PostSubmitteds.Remove(post);
                    _context.SaveChanges();
                }
            }


            _context.Users.Remove(user);
            _context.SaveChanges();
            return Json(new { result = 1, success = true, message = "Successfully" });

        }


        public ActionResult Create()
        {
            var ViewModel = new UserRoleViewModel
            {
                User = new User(),
                Roles = _context.Roles.ToList(),

            };
            return View(ViewModel);

        }

        [HttpPost]
        public ActionResult Create(User user, HttpPostedFileBase file)
        {
            var users = _context.Users.Where(x => x.Username == user.Username || x.Email == user.Email).ToList();
            if (!users.Any())
            {
                if (file != null)
                {
                    string pic = System.IO.Path.GetFileName(file.FileName);
                    string path = System.IO.Path.Combine(Server.MapPath("~/Images/Profiles/"), pic);

                    file.SaveAs(path);
                    user.Image = pic;
                }

                _context.Users.Add(user);
                _context.SaveChanges();
                return Json(new { result = 1, success = true, message = "Successfully" });

            }
            return Json(new { result = 0, success = false, message = "Username/Email is already Exist" });
        }

        //================POSTS======================//


        public ActionResult Posts()
        {

            var Post = getPosts();
            return View(Post);

        }

        public IEnumerable<Post> getPosts()
        {
            var Posts = _context.Posts
                .Include(x=>x.JopType)
                .Include(x=>x.User)
                .ToList();
            return Posts;
        }


        public ActionResult AddPost()
        {
            var ViewModel = new PostJobType
            {
                JopType = _context.JopTypes.ToList(),

            };
            return View(ViewModel);
        }
        [HttpPost]
        public ActionResult AddPost(Post post)
        {
            if (!ModelState.IsValid)
                return Json(new { result = 0, success = false, message = "Failed" });
            post.CreationDateTime = DateTime.Now;
            post.isApproved = true;

            _context.Posts.Add(post);
            _context.SaveChanges();
            return Json(new { result = 1, success = true, message = "Successfully" });
        }
        [HttpPost]
        public ActionResult RemovePost(int id)
        {
            var RemovePost = _context.Posts.Single(c => c.Id == id);
            if (RemovePost == null)
                return Json(new { result = 1, success = true, message = "Failed" });
            _context.Posts.Remove(RemovePost);
            _context.SaveChanges();

            return Json(new { result = 1, success = true, message = "Successfully" });


        }

        [HttpGet]
        public ActionResult UpdatePost(int id)
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
        public ActionResult UpdatePost(Post post)
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

        public ActionResult Requests()
        {
            var posts = _context.Posts.Include(x => x.User).Include(x => x.JopType)
                .Where(x=> x.isApproved == false).ToList();
            return View(posts);
        }
        [HttpPost]
        public ActionResult AcceptPost(int id)
        {
            var post = _context.Posts.Single(x => x.Id == id);
            if (post == null)
                return Json(new { result = 0, success = false, message = "Failed" });
            post.isApproved = true;
            _context.SaveChanges();
            return Json(new { result = 1, success = true, message = "Successfully" });
        }


        public ActionResult AcceptedUser(int id)
        {
            var post = _context.PostSubmitteds
                .Single(x => x.PostId == id && x.UserId== id);
            if (post == null)
                return Json(new { result = 0, success = false, message = "Failed" });
            post.isAccepted = true;
            _context.SaveChanges();
            var postsNotAccpeted = _context.PostSubmitteds.Where(x =>x.PostId ==post.Id  &&x.isAccepted == false).ToList();
            foreach (var x in postsNotAccpeted)
            {
                _context.PostSubmitteds.Remove(x);
            }

            var deletefromwall = _context.Posts.SingleOrDefault(x => x.Id == post.PostId);
            deletefromwall.isSubmitted = true;
            _context.SaveChanges();


            return Json(new { result = 1, success = true, message = "Successfully" });

        }

        public ActionResult ReceivedProposal(int id)
        {
            var getPostFromposts = _context.Posts.Where(x => x.UserId == id && x.isSubmitted == false).ToList();
            var sub = new List<PostSubmitted>();
            foreach (var x in getPostFromposts)
            {
                sub.AddRange(_context.PostSubmitteds.Where(q => q.PostId == x.Id).Include(t => t.User).Include(w => w.Post).ToList());
            }

            return View(sub);
        }
        [HttpPost]
        public ActionResult RefusedUser(PostSubmitted postSubmitted)
        {
            var postInDb = _context.PostSubmitteds
                    .Single(x => x.PostId == postSubmitted.PostId 
                                && x.UserId == postSubmitted.UserId);
            if(postInDb == null)
                return Json(new { result = 0, success = false, message = "Failed" });

            _context.PostSubmitteds.Remove(postInDb);
            _context.SaveChanges();
            return Json(new { result = 1, success = true, message = "Successfully" });
        }
    }
}