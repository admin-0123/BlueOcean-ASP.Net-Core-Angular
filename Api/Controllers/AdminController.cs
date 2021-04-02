using Microsoft.AspNetCore.Mvc;

namespace Virta.Controllers
{
    [Route("Admin")]
    [ApiExplorerSettings(IgnoreApi=true)]
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View("~/Views/Admin/Index.cshtml");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(string email,string password)
        {
            if (ModelState.IsValid)
            {
            }
            return View("~/Views/Admin/Dashboard.cshtml");
        }
    }
}
