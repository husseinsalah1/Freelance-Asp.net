using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Freelance.Models
{
    public class SavePost
    {
        public int Id { get; set; }
        public  User User { get; set; }
        public int UserId { get; set; }

        public  Post Post { get; set; }

        public int PostId { get; set; }
    }
}