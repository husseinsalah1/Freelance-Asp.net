using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Freelance.Models
{
    public class Role
    {
        public int Id { get; set; }
        [Display(Name = "Role")]
        public string Name { get; set; }
    }
}