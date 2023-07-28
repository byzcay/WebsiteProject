using Microsoft.AspNetCore.Mvc;

namespace RecipeApp.Controllers
{
    public class CommentController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
