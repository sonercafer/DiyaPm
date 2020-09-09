using DiyaPM.UI.Class;
using DiyaPM.UI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
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

            return RedirectToAction("Add");
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
    }
}