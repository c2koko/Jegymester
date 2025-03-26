using Microsoft.AspNetCore.Mvc;
using Jegymester.DataContext.Entities;
using Jegymester.DataContext.Data;

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
