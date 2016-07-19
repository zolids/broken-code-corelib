using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using WebApp.Models;
using WebApp.helpers;

namespace WebApp.Controllers
{
    public class AccountController : Controller
    {

        private UserModule _userAccount;
        private user_models _userModels;

        [HttpGet]
        public ActionResult Login(string returnUrl)
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(Users user, string ReturnUrl = "")
        {
            using (_userAccount = new UserModule())
            {

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

                    return RedirectToAction("DefaultViewport", "Home");

                }

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
        public ActionResult Register(Users user, bool newuser = true, int user_id = 0)
        {

            char[] delimiterChars = {','};

            using (_userModels = new user_models())
            using (_userAccount = new UserModule())
            {
                
                _userModels.user_type = newuser;
                _userModels.project_involve = _userAccount.getProjectList();

                if (newuser){
                    
                    _userModels.pages = _userAccount.getWPPages();
                    
                    return View(_userModels);
                }
                else
                {
                    _userModels.users = _userAccount.getUser(user_id);

                    if (!string.IsNullOrEmpty(_userModels.users.SingleOrDefault().accessible_page))
                    {
                        string st = _userModels.users.SingleOrDefault().accessible_page.ToString();
                        ViewBag.Projects = st.Split(delimiterChars);
                    }
                    
                    _userModels.user_pages = _userAccount.getUserPages(user_id);

                    return View(_userModels);
                }
            }
        }

        [HttpGet]
        [Authorize]
        public ActionResult Settings()
        {
            ViewBag.Title = Constant.GENERAL_SETTINGS;
            
            return View();
        }

        [HttpPost]
        [Authorize]
        public ActionResult Register(Users user)
        {
            return View();
        }


        [HttpGet]
        [Authorize]
        public ActionResult SystemUsers(int user_id = 0)
        {
            using (_userAccount = new UserModule())
            {
                ViewBag.Title = Constant.SYSTEM_USERS;

                return View(_userAccount.getUser());
            } 
        }

    }

}
