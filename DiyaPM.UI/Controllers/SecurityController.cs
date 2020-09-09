using DiyaPM.UI.Models;
using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Web.Security;
using System.Web.WebPages;

namespace DiyaPM.UI.Controllers
{
    public class SecurityController : Controller
    {
        // GET: Security
        DiyaPMContext _diyaPMContext = new DiyaPMContext();
        [AllowAnonymous]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(User _user)
        {
            if (ModelState.IsValid)
            {
                if (_user.Password == null || _user.UserName == null)
                {
                    ViewBag.Message = "Kullanıcı adı ve/veya şifre boş geçilemez!";
                    return View();
                } 
                
                var user = _diyaPMContext.Users.FirstOrDefault(x => x.UserName == _user.UserName);
                if (user != null)
                {
                    var verifiedPass = Crypto.VerifyHashedPassword(user.Password, _user.Password);
                    if (verifiedPass)
                    {
                        FormsAuthentication.SetAuthCookie(user.UserName, false);
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        ViewBag.Message = "Girmiş olduğunuz şifre yanlış!";
                        return View();
                    }
                }
                else
                {
                    ViewBag.Message = "Kullanıcı adı ve/veya şifre hatalı. Lütfen bilgileri kontrol edin.";
                }
            }
            return View();
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "Security");
        }
    }
}