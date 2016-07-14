using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using WebApp.Models;

namespace WebApp.Controllers
{
    public class AccountController : Controller
    {

        private UserModule _userAccount;

        [HttpGet]
        public ActionResult Login(string returnUrl)
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(Users user, string ReturnUrl = "")
        {
            _userAccount = new UserModule();

            if (ModelState.IsValid && _userAccount.ValidateUser(user.username, user.password))
            {
                FormsAuthentication.SetAuthCookie(user.username, user.RememberMe);
                
                if (user.RememberMe)
                {
                    Response.Cookies["username"].Expires = DateTime.Now.AddDays(30);
                    Response.Cookies["password"].Expires = DateTime.Now.AddDays(30);
                    Response.Cookies["RememberMe"].Expires = DateTime.Now.AddDays(30);

                    Response.Cookies["username"].Value = user.username;
                    Response.Cookies["password"].Value = user.password;
                    Response.Cookies["RememberMe"].Value = "true";
                }
                else
                {
                    Response.Cookies["username"].Expires = DateTime.Now.AddDays(-1);
                    Response.Cookies["password"].Expires = DateTime.Now.AddDays(-1);
                    Response.Cookies["RememberMe"].Expires = DateTime.Now.AddDays(-1);

                    Response.Cookies["username"].Value = null;
                    Response.Cookies["password"].Value = null;
                    Response.Cookies["RememberMe"].Value = null;
                }

                return RedirectToAction("Index", "Home");

            }

            return View();
        }

        [Authorize]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "Account");
        }

        [HttpGet]
        [Authorize]
        public ActionResult Register(Users user)
        {
            return View();
        }

        [HttpPost]
        [Authorize]
        public ActionResult Register(Users user, bool newuser = true)
        {
            return View();
        }

    }
}
