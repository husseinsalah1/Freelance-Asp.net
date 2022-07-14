using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Freelance.Models
{
    public class User
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Username is required")]
        [Display(Name = "Username")]
        [StringLength(255)]
        [Index(IsUnique = true)]
        public string Username { get; set; }


        [Display(Name = "First name")]
        [StringLength(255)]
        public string FName { get; set; }


        [Display(Name = "Last name")]
        [StringLength(255)]
        public string LName { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [Display(Name = "Email")]
        [StringLength(255)]
        [EmailAddress]
        public string Email { get; set; }


       

        [Required(ErrorMessage = "Password is required")]
        [Display(Name = "Password")]
        [DataType(DataType.Password)]

        public string Password { get; set; }
        [NotMapped]
        [Display(Name = "Confirm Password")]
        [StringLength(255)]
        [DataType(DataType.Password)]

        public string ConfirmPassword { get; set; }


        public Role Role { get; set; }
        [Display(Name = "Role")]
        public int RoleId { get; set; }



        [FileExtensions(Extensions = "jpg,jpeg,png")]
        [DataType(DataType.ImageUrl)]
        [Display(Name = "Photo")]
        public string Image { get; set; }

        [Display(Name = "Phone")]
        [DataType(DataType.PhoneNumber)]

        public string PNumber { get; set; }

    }
}