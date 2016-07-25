using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using WebApp.Models;
using WebApp.helpers;
using Newtonsoft.Json;

namespace WebApp.Controllers
{
    public class AccountController : Controller
    {

        private UserModule _userAccount;
        private user_models _userModels;
        private Helper _helpers;

        char[] delimiterChars = { ',' };

        [HttpGet]
        public ActionResult Login(string returnUrl)
        {
            if (returnUrl != "/")
            {
                Response.Cookies["returnURL"].Expires = DateTime.Now.AddHours(24);
                Response.Cookies["returnURL"].Value   = returnUrl;
            }
                
            return View();

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(Users user, string ReturnUrl = "")
        {
            using (_userModels = new user_models())
            using (_userAccount = new UserModule())
            {
                var _users = _userAccount.ValidateUser(user.username, user.password);

                if (_users != null)
                {
                    if (ModelState.IsValid && _users != null)
                    {
                        FormsAuthentication.SetAuthCookie(user.username, user.RememberMe);

                        this.setSystemCookies(_users, user.RememberMe);

                    }
                }
                else ModelState.AddModelError("", "Invalid Username or Password");
            }

            return View();
        }

        public void setSystemCookies(Users _users, bool rememberLogin = false)
        {
            using (_helpers = new Helper())
            {
                if (_users != null)
                {

                    _users.RememberMe = rememberLogin;
                    _users.password = _helpers.DecryptString(_users.password);

                    _userModels.users           = _users;
                    _userModels.ip              = Server.HtmlEncode(Request.UserHostAddress);
                    _userModels.host            = Server.HtmlEncode(Request.UserHostName);
                    _userModels.system_config   = _userAccount.SystemConfig();

                    if (!string.IsNullOrEmpty(_users.accessible_page))
                        _userModels.module = _users.accessible_page.Split(delimiterChars);

                    try
                    {
                        _userModels.host = System.Net.Dns.GetHostEntry(Request.UserHostAddress).HostName;
                    }
                    catch (Exception) { }

                    string userIfo = JsonConvert.SerializeObject(_userModels, Formatting.None, new JsonSerializerSettings()
                    {
                        ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
                    });

                    _userModels.user_pages = _userAccount.getUserPages(Convert.ToInt32(_users.user_id));

                    Response.Cookies["userInfo"].Expires = DateTime.Now.AddDays(30);
                    Response.Cookies["userInfo"].Value = _helpers.EncryptString(userIfo);

                    if (Request.Cookies["returnURL"] != null) {
                        
                        string URL = Request.Cookies["returnURL"].Value;

                        Response.Cookies["returnURL"].Expires = DateTime.Now.AddDays(-1);
                        Response.Redirect(URL);
                    }
                    else
                    {
                        Response.Cookies["returnURL"].Expires = DateTime.Now.AddDays(-1);
                        Response.Redirect("/Home/DefaultViewport");
                    }
                }
            }
            
        }

        [Authorize]
        public ActionResult Logout()
        {
            using (_helpers = new Helper())
            {
                string session = _helpers.DecryptString(Request.Cookies["userInfo"].Value);
                var uInfo = _helpers.UserData(session).users;

                if (uInfo.RememberMe)
                {
                    Response.Cookies["username"].Value   = uInfo.username;
                    Response.Cookies["password"].Value   = uInfo.password;
                    Response.Cookies["RememberMe"].Value = uInfo.RememberMe.ToString();
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

                Response.Cookies["userInfo"].Expires = DateTime.Now.AddDays(-1);
                Response.Cookies["activeProject"].Expires = DateTime.Now.AddDays(-1);
                
                FormsAuthentication.SignOut();
                return RedirectToAction("Login", "Account");
            }

        }

        [HttpGet]
        [Authorize]
        public ActionResult Register(Users user, bool newuser = true, int user_id = 0)
        {

            using (_userModels = new user_models())
            using (_userAccount = new UserModule())
            {

                ViewBag.Projects = new string[0];

                _userModels.new_user = newuser;
                _userModels.project_involve = _userAccount.getProjectList();

                if (newuser){
                    
                    _userModels.pages = _userAccount.getWPPages();
                    
                    return View(_userModels);
                }
                else
                {
                    _userModels.users = _userAccount.getUser(user_id).SingleOrDefault();

                    if (!string.IsNullOrEmpty(_userModels.users.accessible_page))
                    {
                        string st = _userModels.users.accessible_page.ToString();
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
