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
                if(Url.IsLocalUrl(ReturnUrl))
                    return RedirectToAction("Index", "Home");
                else
                    return RedirectToAction(ReturnUrl);
            }

            return View();
        }

        [Authorize]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "Account");
        }

    }
}
