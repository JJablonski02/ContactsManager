using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ContactsManager.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class HomeController : Controller
    {
        //[Route("admin/home/index")] <- Attribute routing. It's replaced by 'conventional routing' in program.cs file - alternative.
        public IActionResult Index()
        {
            return View();
        }
    }
}
