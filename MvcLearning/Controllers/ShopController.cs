using Microsoft.AspNetCore.Mvc;

namespace MvcLearning.Controllers
{
    public class ShopController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
