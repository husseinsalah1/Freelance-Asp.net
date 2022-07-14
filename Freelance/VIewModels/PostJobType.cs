using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Freelance.Models;

namespace Freelance.VIewModels
{
    public class PostJobType
    {
        public Post Post { get; set; }
        public IEnumerable<JopType> JopType { get; set; }
    }
}