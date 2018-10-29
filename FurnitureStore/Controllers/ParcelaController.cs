using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FurnitureStore.Models;

namespace FurnitureStore.Controllers
{
    [Authorize]
    public class ParcelaController : Controller
    {
        private DataContext db = new DataContext();

        //
        // GET: /Parcela/
        public ActionResult Index()
        {
            return View(db.Parcelas.ToList());
        }

        //
        // GET: /Parcela/Details/5

        public ActionResult Details(int id)
        {
            Parcela parcela = db.Parcelas.Find(id);
            return View(parcela);
        }

        //
        // GET: /Parcela/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Parcela/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Parcela parcela)
        {
            if (ModelState.IsValid)
            {
                db.Parcelas.Add(parcela);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(parcela);
        }

        //
        // GET: /Parcela/Edit/5

        public ActionResult Edit(int id)
        {
            Parcela parcela = db.Parcelas.Find(id);
            return View(parcela);
        }

        //
        // POST: /Parcela/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Parcela parcela)
        {
            if (ModelState.IsValid)
            {
                db.Entry(parcela).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(parcela);
        }

        //
        // GET: /Parcela/Delete/5

        public ActionResult Delete(int id)
        {
            Parcela parcela = db.Parcelas.Find(id);
            return View(parcela);
        }

        //
        // POST: /Parcela/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Parcela parcela = db.Parcelas.Find(id);
            db.Parcelas.Remove(parcela);
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