using Microsoft.AspNetCore.Mvc;
using Jegymester.Entities;
using Jegymester.Data;

namespace Jegymester.Controllers
{
    public class RoleController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
