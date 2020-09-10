using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DiyaPM.UI.Models.ViewModels
{
    public class MenusInRolesViewModel
    {
        public IEnumerable<Role> Roles; 
        public IEnumerable<Menu> Menus;

    }
}