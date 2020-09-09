using DiyaPM.UI.Models;
using DiyaPM.UI.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DiyaPM.UI.Controllers
{
    public class HomeController : Controller
    {
        DiyaPMContext _diyaPMContext = new DiyaPMContext();
        [Authorize] 
        public ActionResult Index()
        { 
            return View();
        }

        public PartialViewResult Menu()
        {
            MenuViewModel menuViewModel = new MenuViewModel();
            menuViewModel.Menus = _diyaPMContext.Menus.Where(x => x.ParentID == 0).ToList();
            menuViewModel.SubMenus = _diyaPMContext.Menus.Where(x => x.ParentID != 0).ToList(); 

            return PartialView("_Menu", menuViewModel);
        }

    }
}