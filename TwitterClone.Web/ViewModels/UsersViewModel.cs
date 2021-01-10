using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TwitterClone.Web.Models;

namespace TwitterClone.Web.ViewModels
{
    public class UsersViewModel
    {
        public IEnumerable<User> Users { get; set; }
    }
}
