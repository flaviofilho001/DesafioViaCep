using Microsoft.AspNetCore.Mvc;

namespace ViaCep.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            if (User.Identity?.IsAuthenticated == true)
            {
                return RedirectToAction("Index", "Enderecos");
            }

            return RedirectToAction("Login", "Account");
        }
    }
}
