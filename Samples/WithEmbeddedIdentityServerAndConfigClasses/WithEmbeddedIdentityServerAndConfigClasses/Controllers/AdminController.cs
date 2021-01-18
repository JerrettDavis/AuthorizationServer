using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WithEmbeddedIdentityServerAndConfigClasses.Controllers
{
    // Authorize using the permission name
    [Authorize("AccessAdmin")]
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}