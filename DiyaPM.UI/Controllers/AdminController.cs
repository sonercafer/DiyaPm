using DiyaPM.UI.Class;
using DiyaPM.UI.Models;
using DiyaPM.UI.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace DiyaPM.UI.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        // GET: User
        DiyaPMContext diyaPMContext = new DiyaPMContext();
        DatabaseLogger databaseLogger = new DatabaseLogger();
        public ActionResult Index()
        {
            return View();
        }

        #region User Process
        public ActionResult AddUser()
        {
            var userList = diyaPMContext.Users.ToList();
            return View(userList);
        }

        [HttpPost]
        public ActionResult AddUser(string _username, string _password)
        {
            try
            {
                if (_username == "" || _password == "")
                {
                    return Json(new ResultJson { Success = false, Message = "Lütfen tüm alanları doldurunuz!" });
                }

                User user = new User();
                user.UserName = _username;
                user.Password = Crypto.HashPassword(_password);

                var username = diyaPMContext.Users.FirstOrDefault(x => x.UserName == _username);
                if (username != null)
                {
                    return Json(new ResultJson { Success = false, Message = "Böyle bir kullanıcı zaten mevcut!" });
                }

                diyaPMContext.Users.Add(user);
                diyaPMContext.SaveChanges();
                return Json(new ResultJson { Success = true, Message = "Kullanıcı ekleme işlemi tamamlandı.." });
            }
            catch (Exception ex)
            {
                databaseLogger.Log(ex.ToString(), User.Identity.Name);
                return Json(new ResultJson { Success = false, Message = ex.ToString() });
            }

        }

        public ActionResult EditUser(int id)
        {
            var user = diyaPMContext.Users.FirstOrDefault(x => x.id == id);
            return View(user);
        }

        [HttpPost]
        public ActionResult EditUser(string _user, string _password, int id)
        {
            try
            {
                if (_user == "" || _password == "")
                {
                    return Json(new ResultJson { Success = false, Message = "Lütfen tüm alanları doldurunuz!" });
                }
                if (diyaPMContext.Users.Where(x => x.id != id).Any(x => x.UserName == _user))
                {
                    return Json(new ResultJson { Success = false, Message = "Kullanıcı Mevcut.." });
                }

                var user = diyaPMContext.Users.FirstOrDefault(x => x.id == id);
                user.UserName = _user;
                user.Password = Crypto.HashPassword(_password);
                diyaPMContext.SaveChanges();

                return Json(new ResultJson { Success = true, Message = "Kullanıcı düzenleme işlemi tamamlandı.." });
            }
            catch (Exception ex)
            {
                databaseLogger.Log(ex.ToString(), User.Identity.Name);
                return Json(new ResultJson { Success = false, Message = ex.ToString() });
            }
        }

        public ActionResult DeleteUser(int id)
        {
            var user = diyaPMContext.Users.FirstOrDefault(x => x.id == id);
            diyaPMContext.Users.Remove(user);
            diyaPMContext.SaveChanges();

            return RedirectToAction("AddUser");
        }

        #endregion

        #region Role Process
        public ActionResult AddRole()
        {
            var roleList = diyaPMContext.Roles.ToList();
            return View(roleList);
        }

        [HttpPost]
        public ActionResult AddRole(string _role)
        {
            try
            {
                if (_role == "")
                {
                    return Json(new ResultJson { Success = false, Message = "Lütfen rol giriniz!" });
                }
                if (diyaPMContext.Roles.Any(x => x.Name == _role))
                {
                    return Json(new ResultJson { Success = false, Message = "Böyle bir rol zaten tanımlı!" });
                }
                Role role = new Role();
                role.Name = _role;
                diyaPMContext.Roles.Add(role);
                diyaPMContext.SaveChanges();
                return Json(new ResultJson { Success = true, Message = "Rol ekleme işlemi tamamlandı.." });
            }
            catch (Exception ex)
            {
                databaseLogger.Log(ex.ToString(), User.Identity.Name);
                return Json(new ResultJson { Success = false, Message = ex.ToString() });
            }

        }
        public ActionResult EditRole(int id)
        {
            var role = diyaPMContext.Roles.FirstOrDefault(x => x.id == id);
            return View(role);
        }

        [HttpPost]
        public ActionResult EditRole(string _role, int id)
        {
            try
            {
                if (_role == "")
                {
                    return Json(new ResultJson { Success = false, Message = "Lütfen rol giriniz!" });
                }
                if (diyaPMContext.Roles.Where(x => x.id != id).Any(x => x.Name == _role))
                {
                    return Json(new ResultJson { Success = false, Message = "Rol Mevcut.." });
                }

                var role = diyaPMContext.Roles.FirstOrDefault(x => x.id == id);
                role.Name = _role;
                diyaPMContext.SaveChanges();

                return Json(new ResultJson { Success = true, Message = "Rol düzenleme işlemi tamamlandı.." });
            }
            catch (Exception ex)
            {
                databaseLogger.Log(ex.ToString(), User.Identity.Name);
                return Json(new ResultJson { Success = false, Message = ex.ToString() });
            }
        }

        public ActionResult DeleteRole(int id)
        {
            var role = diyaPMContext.Roles.FirstOrDefault(x => x.id == id);
            diyaPMContext.Roles.Remove(role);
            diyaPMContext.SaveChanges();

            return RedirectToAction("AddRole");
        }
        #endregion

        #region Users in Roles 
         
        public ActionResult UsersInRoles()
        {
            UsersInRolesViewModel usersInRolesViewModel = new UsersInRolesViewModel();
            usersInRolesViewModel.Roles = diyaPMContext.Roles.ToList();
            usersInRolesViewModel.Users = diyaPMContext.Users.ToList(); 
            
            return View(usersInRolesViewModel);
        }

        [HttpPost]
        public ActionResult UsersInRoles(int _roleid,int _userid)
        {
            if (_roleid.ToString() == ""  || _userid.ToString() == "" )
            {
                return Json(new ResultJson { Success = false, Message = "Rol veya Kullanıcı seçiniz!" });
            }
            try {
                var result = diyaPMContext.UsersInRoles.Where(x=>x.user_id == _userid && x.role_id == _roleid).Any();
                if (result)
                {
                    return Json(new ResultJson { Success = false, Message = "Kullanıcı zaten bu role eklenmiş!" });
                }

                UsersInRole usersInRole = new UsersInRole();
                usersInRole.user_id = _userid;
                usersInRole.role_id = _roleid; 

                diyaPMContext.UsersInRoles.Add(usersInRole);
                diyaPMContext.SaveChanges();

                return Json(new ResultJson { Success = true, Message = "Role ait kullanıcı eklendi.." });
            }
            catch (Exception ex)
            {
                databaseLogger.Log(ex.ToString(), User.Identity.Name);
                return Json(new ResultJson { Success = false, Message = ex.ToString() });
            }
            
        }

        public ActionResult GetUsersInRoles(int id)
        {
            try
            {
                var result = (from u in diyaPMContext.Users
                              join uir in diyaPMContext.UsersInRoles on u.id equals uir.user_id
                              where uir.role_id == id
                              select new
                              {
                                  id = uir.id,
                                  username = u.UserName
                              }).ToList();

                return Json(new { data = result }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                databaseLogger.Log(ex.ToString(), User.Identity.Name);
                return Json(new ResultJson { Success = false, Message = ex.ToString() });
            }
            
        }

        [HttpPost]
        public ActionResult UsersInRoleDel(int id)
        {
            try
            {
                var role = diyaPMContext.UsersInRoles.FirstOrDefault(x => x.id == id);
                diyaPMContext.UsersInRoles.Remove(role);
                diyaPMContext.SaveChanges();

                return Json(new ResultJson { Success = true, Message = "Role ait kullanıcı silindi.." });

            }
            catch (Exception ex)
            { 
                databaseLogger.Log(ex.ToString(), User.Identity.Name);
                return Json(new ResultJson { Success = false, Message = ex.ToString() });
            }
            
        }
        #endregion

        #region Menus in Roles 

        public ActionResult MenusInRoles()
        {
            MenusInRolesViewModel menusInRolesViewModel = new MenusInRolesViewModel();
            menusInRolesViewModel.Roles = diyaPMContext.Roles.ToList();
            menusInRolesViewModel.Menus = diyaPMContext.Menus.ToList();

            return View(menusInRolesViewModel);
        }

        [HttpPost]
        public ActionResult MenusInRoles(int _roleid, int _menuid)
        {
            if (_roleid.ToString() == "" || _menuid.ToString() == "")
            {
                return Json(new ResultJson { Success = false, Message = "Rol veya Menü seçiniz!" });
            }

            try
            {
                var result = diyaPMContext.MenusInRoles.Where(x => x.menu_id == _menuid && x.role_id == _roleid).Any();
                if (result)
                {
                    return Json(new ResultJson { Success = false, Message = "Menü zaten bu role eklenmiş!" });
                }

                MenusInRole menusInRole = new MenusInRole();
                menusInRole.menu_id = _menuid;
                menusInRole.role_id = _roleid;

                diyaPMContext.MenusInRoles.Add(menusInRole);
                diyaPMContext.SaveChanges();

                return Json(new ResultJson { Success = true, Message = "Role ait menü eklendi.." });
            }
            catch (Exception ex)
            {
                databaseLogger.Log(ex.ToString(), User.Identity.Name);
                return Json(new ResultJson { Success = false, Message = ex.ToString() });
            }

        }

        public ActionResult GetMenusInRoles(int id)
        {
            try
            {
                var result = (from m in diyaPMContext.Menus
                              join mir in diyaPMContext.MenusInRoles on m.id equals mir.menu_id
                              where mir.role_id == id
                              orderby m.OrderBy
                              select new
                              {
                                  id = mir.id,
                                  menuname = m.MenuName
                              }).ToList();

                return Json(new { data = result }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                databaseLogger.Log(ex.ToString(), User.Identity.Name);
                return Json(new ResultJson { Success = false, Message = ex.ToString() });
            }

        }

        [HttpPost]
        public ActionResult MenusInRoleDel(int id)
        {
            try
            {
                var menu = diyaPMContext.MenusInRoles.FirstOrDefault(x => x.id == id);
                diyaPMContext.MenusInRoles.Remove(menu);
                diyaPMContext.SaveChanges();

                return Json(new ResultJson { Success = true, Message = "Role ait menü silindi.." });

            }
            catch (Exception ex)
            {
                databaseLogger.Log(ex.ToString(), User.Identity.Name);
                return Json(new ResultJson { Success = false, Message = ex.ToString() });
            }

        }
        #endregion
    }
}