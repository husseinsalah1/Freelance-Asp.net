using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Freelance.Models
{
    public class Post
    {
        public int Id { get; set; }
        [Display(Name = "Budget")]
        [Range(100, 100000)]
        [Required]
        public int JobBudget { get; set; }
        [Display(Name = "Date")]
        public DateTime CreationDateTime { get; set; }
        [Required]
        public string Description { get; set; }

        public bool isApproved { get; set; }
        [Display(Name = "Type")]

        public JopType JopType { get; set; }
        [Display(Name = "Jop Type")]
        [Required]
        public int JopTypeId { get; set; }


        public User User { get; set; }
        public int UserId { get; set; }

        public int NumberOfPorpsalSubmitted { get; set; }
        public bool isSubmitted { get; set; }

        public int rate { get; set; }
        public int NumberOfrates { get; set; }
    }
}