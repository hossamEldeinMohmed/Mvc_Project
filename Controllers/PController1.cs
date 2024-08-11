using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Mvc_Project.Controllers
{
    public class PController1 : Controller
    {
        // GET: PController1
        public ActionResult Index()
        {
            return View();
        }

        // GET: PController1/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: PController1/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PController1/Create
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

        // GET: PController1/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: PController1/Edit/5
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

        // GET: PController1/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: PController1/Delete/5
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
