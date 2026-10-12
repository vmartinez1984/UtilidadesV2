using Microsoft.AspNetCore.Mvc;
using Notas.BussinesLayer;
using Notas.Mvc.Models;
using System.Diagnostics;

namespace Notas.Mvc.Controllers
{
    public class HomeController : Controller
    {
        private readonly NotaBl _notaBl;

        public HomeController(NotaBl notaBl)
        {
            _notaBl = notaBl;
        }

        public async Task<IActionResult> Index()
        {
            var lista = await _notaBl.ObtenerAsync();
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
