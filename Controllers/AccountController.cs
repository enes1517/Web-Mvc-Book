using Microsoft.AspNetCore.Mvc;

namespace web.Controllers
{
    public class AccountController : Controller
    {
        private const string name = "admin";
        private const string Password = "12345";
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(string Username,string password)
        {
            if (Username==name && password==Password)
            {
                HttpContext.Session.SetString("IsLoggedIn", "true");
                return View("Feedback"); 
            }
         
                ViewBag.ErrorMessage = "Kullanıcı adı yada şifre yanlış.";
                return View();
            

           
        }



    }
}
