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
    }
}
