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
        private dbContext _db;

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

                    var t = Request.Cookies["returnURL"];

                    if (Request.Cookies["returnURL"] != null && Request.Cookies["returnURL"].Value != "")
                    {
                        
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

        [HttpPost]
        public JsonResult PasswordReset(string npassword = null, int user_id = 0, string token = null)
        {
            bool isSuccess = true;
            string message = string.Empty;

            using (_userAccount = new UserModule())
            {
                if (_userAccount.changePassword(npassword, user_id)) {
                    if (!string.IsNullOrEmpty(token)) _userAccount.updateToken(token);
                }
                else {
                    isSuccess = false;
                    message = "<b>Server Error</b>: Unable to reset password! Please contact IT.";
                }
            }

            return Json(new { isSuccess = isSuccess, message = message }, "text/html");
        }

        [HttpGet]
        public ActionResult PasswordReset(int id = 0, string token = null)
        {
            if((string.IsNullOrEmpty(token) == true) || id == 0)
                Response.Redirect("/");

            using (_userAccount = new UserModule())
            {
                tbl_tokens tk = new tbl_tokens();

                var validToken = _userAccount.validateToken(token);
                if (validToken != null && validToken.Any())
                {
                    tk = validToken.SingleOrDefault();

                    if (DateTime.Now.Subtract(Convert.ToDateTime(tk.timestamp)).TotalHours > 24)
                        ViewBag.isExpired = true;

                    if (tk.used >= 1)
                        ViewBag.isUsed = true;
                    
                }
                else ViewBag.isValid = false;

                return View(tk);
            }
        }

        [HttpPost]
        public JsonResult PasswordResetRequest(string email = null)
        {
        
            bool isSuccess = true;
            string message = string.Empty;

            if (!string.IsNullOrEmpty(email)) {

                using (_userAccount = new UserModule())
                using (_userModels = new user_models())
                {
                    
                    var _users = _userAccount.getUser(0, email);

                    if (_users != null && _users.Any())
                    {
                        
                        using (_db = new dbContext())
                        using (_helpers = new Helper())
                        {
                            // add to session as temp
                            _userModels.users = _users.SingleOrDefault();
                            _userModels.system_config = _userAccount.SystemConfig();

                            string userIfo = JsonConvert.SerializeObject(_userModels, Formatting.None, new JsonSerializerSettings()
                            {
                                ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
                            });
                            Response.Cookies["userInfo"].Expires = DateTime.Now.AddDays(30);
                            Response.Cookies["userInfo"].Value = _helpers.EncryptString(userIfo);


                            tbl_tokens tk = new tbl_tokens();
                            
                            tk.module       = "RESET_PASSWORD";
                            tk.user_id      = Convert.ToInt32(_users.SingleOrDefault().user_id);
                            tk.timestamp    = DateTime.Now;
                            tk.token        = Guid.NewGuid().ToString();

                            _db.tokens.Add(tk);
                            if (_db.SaveChanges() > 0)
                            {
                                if (_helpers.sendAnEmail(Request.Cookies["userInfo"].Value, "Online FASTrax change password request",
                                    this.emailHtml(tk.user_id, _users.SingleOrDefault().username, tk.token))){

                                    Response.Cookies["userInfo"].Expires = DateTime.Now.AddDays(-1);
                                    message = "An <b>E-mail</b> with further instructions has been sent. Please check your inbox to proceed.";
                                }
                                else
                                    message = "<b>Internal Server Error! </b> : An error occured while proccessing your request. Please contact system admin!";                                           
                            }
                        }

                    }
                    else {
                        isSuccess = false;
                        message = "<b>Invalid email</b> : User account do not exist!";
                    }
                }
            }

            return Json(new { isSuccess = isSuccess, message = message }, "text/html");

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

        [Authorize]
        public string emailHtml(int user_id = 0, string username = null, string token = null)
        {
            string Url = "http://" + Request.Url.Authority.ToString() + "/Account/PasswordReset?id=" + user_id + "&token=" + token;

            string html = "<div style='background:#f4f4f4;margin:40px 0;padding:40px 0' bgcolor='#f4f4f4'><div class='adM'></div><table width='550' border='0' cellspacing='0' cellpadding='0' style='background:#fff;border:1px solid #ccc;border-radius:5px' bgcolor='#FFFFFF' align='center'><tbody><tr><td style='padding:20px;font-family:Arial,Helvetica,sans-serif;font-size:12px'><span class='im'><h3 style='font-family:Arial,Helvetica,sans-serif;font-size:19px;font-weight:normal;margin-bottom:20px;margin-top:0;color:#333333'><font face='Arial, Helvetica, sans-serif' color='#333333'>Password reset instructions</font></h3><p>A request has been received to reset the password for the following account:</p></span><p><span class='im'><strong>Username</strong>: " + username + "<br></span></p><p>To continue, please visit the following link:</p><p><a href='" + Url + "' target='_blank'>" + Url + "</a></p><span class='im'><p>The request is valid only for 24 hours.</p><p>If you did not make this request, simply ignore this email.</p></span><p></p></td></tr><tr><td style='padding:20px;border-top:1px dotted #ccc'><a href='" + Request.Url.Host + "' target='_blank' data-saferedirecturl=''><img src='' alt='' style='display:block;margin:0;border:none' class='CToWUd'></a></td></tr></tbody></table><div class='yj6qo'></div><div class='adL'></div></div>";

            return html;
        }

    }

}
