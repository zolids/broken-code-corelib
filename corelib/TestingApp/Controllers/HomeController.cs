using System.Web.Mvc;
using AMSCore.Clients.FastraxCore;
using System.Data;

namespace TestingApp.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/
        
        public ActionResult Index()
        {

            login loginClient = new login();

            var users = loginClient.getUsers();

            return View(users);
        }

    }
}
