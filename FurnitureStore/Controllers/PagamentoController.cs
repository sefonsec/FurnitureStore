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
    public class PagamentoController : Controller
    {
        private DataContext db = new DataContext();

        //
        // GET: /Pagamento/
        public ActionResult Index()
        {
            return View(db.Pagamentos.ToList());
        }

        //
        // GET: /Pagamento/Details/5

        public ActionResult Details(int id)
        {
            Pagamento pagamento = db.Pagamentos.Find(id);
            return View(pagamento);
        }

        //
        // GET: /Pagamento/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Pagamento/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Pagamento pagamento)
        {
            if (ModelState.IsValid)
            {
                db.Pagamentos.Add(pagamento);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(pagamento);
        }

        //
        // GET: /Pagamento/Edit/5

        public ActionResult Edit(int id)
        {
            Pagamento pagamento = db.Pagamentos.Find(id);      
            return View(pagamento);
        }

        //
        // POST: /Pagamento/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Pagamento pagamento)
        {
            if (ModelState.IsValid)
            {
                db.Entry(pagamento).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(pagamento);
        }

        //
        // GET: /Pagamento/Delete/5

        public ActionResult Delete(int id)
        {
            Pagamento pagamento = db.Pagamentos.Find(id);    
            return View(pagamento);
        }

        //
        // POST: /Pagamento/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Pagamento pagamento = db.Pagamentos.Find(id);
            db.Pagamentos.Remove(pagamento);
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