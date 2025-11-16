using Microsoft.AspNetCore.Mvc;

namespace GestaoObras.Web.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
