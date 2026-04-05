using Microsoft.AspNetCore.Mvc;

namespace BlogV1.Controllers
{
    public class BlogsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
