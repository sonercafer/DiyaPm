using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DiyaPM.UI.Models.ViewModels
{
    public class UsersInRolesViewModel
    {
        public IEnumerable<Role> Roles;
        public IEnumerable<UsersInRole> UsersInRoles;
        public IEnumerable<User> Users;

    }
}