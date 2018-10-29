using System.Data;
using System.Linq;
using System.Web.Mvc;
using FurnitureStore.Models;

namespace FurnitureStore.Controllers
{
    [Authorize]
    public class VendedorController : Controller
    {
        private DataContext db = new DataContext();

        //
        // GET: /Vendedor/
        public ActionResult Index()
        {
            return View(db.Vendedores.ToList());
        }

        //
        // GET: /Vendedor/Details/5

        public ActionResult Details(int id)
        {
            Vendedor vendedor = db.Vendedores.Find(id);
            return View(vendedor);
        }

        //
        // GET: /Vendedor/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Vendedor/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Vendedor vendedor)
        {
            if (ModelState.IsValid)
            {
                db.Vendedores.Add(vendedor);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(vendedor);
        }

        //
        // GET: /Vendedor/Edit/5

        public ActionResult Edit(int id)
        {
            Vendedor vendedor = db.Vendedores.Find(id);
            return View(vendedor);
        }

        //
        // POST: /Vendedor/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Vendedor vendedor)
        {
            if (ModelState.IsValid)
            {
                db.Entry(vendedor).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(vendedor);
        }

        //
        // GET: /Vendedor/Delete/5

        public ActionResult Delete(int id)
        {
            Vendedor vendedor = db.Vendedores.Find(id);
            return View(vendedor);
        }

        //
        // POST: /Vendedor/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Vendedor vendedor = db.Vendedores.Find(id);
            db.Vendedores.Remove(vendedor);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}