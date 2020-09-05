using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DiyaPM.UI.Models
{
    public class MenuViewModel
    {
        public IEnumerable<Menu> Menus { get; set; }
        public IEnumerable<Menu> ParentMenus { get; set; }

    }
}