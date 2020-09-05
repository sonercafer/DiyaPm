using DiyaPM.UI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DiyaPM.UI.Controllers
{
    public class HomeController : Controller
    {
        DiyaPMContext DiyaPMContext = new DiyaPMContext();
        public ActionResult Index()
        {

            return View();
        } 

        public ActionResult Menu()
        {
            MenuViewModel menuViewModel = new MenuViewModel();
            menuViewModel.Menus = DiyaPMContext.Menus.Where(x => x.ParentID == 0).ToList();
            menuViewModel.ParentMenus = DiyaPMContext.Menus.Where(x => x.ParentID != 0).ToList();
            
            return PartialView("_Menu", menuViewModel);
        }
    }
}