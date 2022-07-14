using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Freelance.Models;
using Freelance.VIewModels;
using System.Data.Entity;
using System.Security.Cryptography;
using System.Text;

namespace Freelance.Controllers
{
    public class UserController : Controller
    {
        private ApplicationDbContext _context;

        public UserController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

       

        //GET:User/Register
        public ActionResult Register()
        {
            var ViewModel = new UserRoleViewModel
            {
                Roles = _context.Roles.Where(x => x.Name != "Admin").ToList()
            };
            return View(ViewModel);
        }

       
        //POST
        [HttpPost]
        public ActionResult Register(User user, HttpPostedFileBase file)
        {
            
            var users = _context.Users.Where(x => x.Username == user.Username || x.Email == user.Email).ToList();

            if(!users.Any())
            {
                if (file != null)
                {
                    string pic = System.IO.Path.GetFileName(file.FileName);
                    string path = System.IO.Path.Combine(Server.MapPath("~/Images/Profiles/"), pic);

                    file.SaveAs(path);
                    user.Image = pic;
                }
                if(user.Password != user.ConfirmPassword)
                    return Json(new { result = -1, success = false, message = "Confirmation Password doesn't match" });

                _context.Users.Add(user);
                _context.SaveChanges();
                return Json(new { result = 1, success = true, message = "Successfully" });

            }
            return Json(new { result = 0, success = false, message = "Username/Email is already Exist" });
        }

        //GET:User/Login
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(User user)
        {
           
                var email = _context.Users.Include(c => c.Role).SingleOrDefault(u => u.Email == user.Email || u.Username == user.Username);

                if (email == null || email.Password != user.Password)
                    return Json(new { result = 0, success = false, message = "Incorrect username or password, Try again" });

                Session["Username"] = email.Username;
                Session["Id"] = email.Id;
                Session["Role"] = email.Role.Name;

                if (email.Role.Name == "Freelancer")
                    return Json(new { result = 1, url = "/Freelancer/ViewJop", message = "Successfully" });
                if (email.Role.Name == "Client")
                    return Json(new { result = 1, url = "/Client/Show/" + Session["Id"], message = "Successfully" });
                if (email.Role.Name == "Admin")
                  return Json(new { result = 1, url = "/Admin/ShowUsers", message = "Successfully" });
                
            return View();
        }

        public ActionResult LogOut()
        {
            Session.Abandon();
            return RedirectToAction("Login", "User");
        }



        //Profile

        public ActionResult Edit(int id)
        {
            var profile = _context.Users.Include(x=>x.Role).SingleOrDefault(z => z.Id == id);
            if (profile == null)
                return HttpNotFound();
            return View(profile);
        }


        [HttpPost]
        public ActionResult Edit(User user)
        {
            var users = _context.Users.Where(x => ( x.Username == user.Username && x.Id != user.Id )|| (x.Email == user.Email && x.Id != user.Id)).ToList();
            if(users.Any())
                return Json(new { result = -1, success = false, message = "Username/Email is already Exist" });

            if (!ModelState.IsValid)
            {
                var Data = _context.Users.Single(x => x.Id == user.Id);

                if (Data == null)
                    return HttpNotFound();
                Data.Username = user.Username;
                Data.FName = user.FName;
                Data.LName = user.LName;
                Data.Username = user.Username;
                Data.Email = user.Email;
                Data.PNumber = user.PNumber;
                
                _context.SaveChanges();
                return Json(new { result = 1, success = true, message = "Successfully" });
            }

            return Json(new { result = 0, success = false, message = "Failed" });

        }

        //============================//
        //ChangePassword
        public ActionResult ChangePassword(int id)
        {
            var UserInDb = _context.Users.SingleOrDefault(x => x.Id == id);
            if (UserInDb == null)
                return Content("Not Found");

            return View(UserInDb);
        }
        [HttpPost]
        public ActionResult ChangePassword(User user)
        {
            var UserInDb = _context.Users.SingleOrDefault(x => x.Id == user.Id);
            if (UserInDb == null)
                return Json(new { result = 0, success = false, message = "Failed" });
            if (user.Password != user.ConfirmPassword)
                return Json(new { result = -1, success = false, message = "Confirmation Password doesn't match" });
            UserInDb.Password = user.Password;
            _context.SaveChanges();

            return Json(new { result = 1, success = true, message = "Successfully" });
        }



    }
}