using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using AuthorizationServer.Web.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AuthorizationServer.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUserStore<IdentityUser> _users;

        public HomeController(IUserStore<IdentityUser> users)
        {
            _users = users;
        }

        public async Task<IActionResult> Index(CancellationToken cancellationToken)
        {
            var tmp = await _users.FindByIdAsync("0c303a4e-5c25-4d10-8396-c74c39c8610a", cancellationToken);
            Debug.WriteLine(tmp?.Id);
            Debug.WriteLine(tmp?.PasswordHash);
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
        }
    }
}