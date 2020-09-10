using DiyaPM.UI.Models;
using DiyaPM.UI.Models.Dto;
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
            var user = _diyaPMContext.Users.FirstOrDefault(x=> x.UserName == User.Identity.Name.ToString());

            MenuViewModel menuViewModel = new MenuViewModel();
            menuViewModel.Menus = (from m in _diyaPMContext.Menus
                                   join mir in _diyaPMContext.MenusInRoles on m.id equals mir.menu_id 
                                   join uir in _diyaPMContext.UsersInRoles on mir.role_id equals uir.role_id
                                   where uir.user_id == user.id && m.ParentID == 0
                                   orderby m.OrderBy
                                   select new MenuDTO { 
                                        id = m.id,
                                        MenuName = m.MenuName,
                                        ParentID = m.ParentID,
                                        IconName = m.IconName,
                                        Action = m.Action,
                                        Controller = m.Controller,
                                        Code = m.Code
                                   }).ToList();

            menuViewModel.SubMenus = (from m in _diyaPMContext.Menus
                                      join mir in _diyaPMContext.MenusInRoles on m.id equals mir.menu_id
                                      join uir in _diyaPMContext.UsersInRoles on mir.role_id equals uir.role_id
                                      where uir.user_id == user.id && m.ParentID != 0
                                      orderby m.OrderBy
                                      select new MenuDTO
                                      {
                                          id = m.id,
                                          MenuName = m.MenuName,
                                          ParentID = m.ParentID,
                                          IconName = m.IconName,
                                          Action = m.Action,
                                          Controller = m.Controller,
                                          Code = m.Code
                                      }).ToList(); 

            return PartialView("_Menu", menuViewModel);
        }

    }
}