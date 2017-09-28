using System.Web.Mvc;
using SportsStore.WebUI.Infrastructure.Abstract;
using SportsStore.WebUI.Models;

namespace SportsStore.WebUI.Controllers
{
    public class AccountController : Controller
    {
        private IAuthProvider iauthProvider;

        public AccountController(IAuthProvider auth)
        {
            iauthProvider = auth;
        }
        
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginViewModel logModel, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                if (iauthProvider.Authenticate(logModel.UserName, logModel.PassWord))
                {
                    return Redirect(returnUrl ?? Url.Action("Index", "Admin"));
                }
                else
                {
                    ModelState.AddModelError("", "Incorrect username or password");
                    return View();
                }
            }
            else
            {
                return View();
            }
        }
        // GET: Account
        public ActionResult Index()
        {
            return View();
        }
    }
}