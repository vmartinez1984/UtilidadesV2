using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Notas.BussinesLayer;

namespace Notas.Mvc.Controllers
{
    public class NotaController : Controller
    {
        private readonly NotaBl notaBl;

        public NotaController(NotaBl notaBl)
        {
            this.notaBl = notaBl;
        }

        // GET: NotaController
        public async Task<ActionResult> Index()
        {
            var lista = await notaBl.ObtenerAsync();

            return View(lista);
        }

        // GET: NotaController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: NotaController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: NotaController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: NotaController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: NotaController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: NotaController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: NotaController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
