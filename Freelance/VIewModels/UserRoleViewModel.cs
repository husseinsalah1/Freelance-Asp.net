using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Web;
using Freelance.Models;

namespace Freelance.VIewModels
{
    public class UserRoleViewModel
    {
        public User User { get; set; }
        public IEnumerable<Role> Roles { get; set; }
        public Role Role { get; set; }
    }
}