using Microsoft.AspNetCore.Mvc;

namespace RecipeApp.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
