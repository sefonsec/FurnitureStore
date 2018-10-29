using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using FurnitureStore.Models;

namespace FurnitureStore.Controllers
{
    [Authorize]
    public class ItensPedidoController : Controller
    {
        private DataContext db = new DataContext();  

        //
        // GET: /ItensPedido/

        public ActionResult Index(int id)
        {
            ViewData["IdPedido"] = id;
            var itenspedidos = db.ItensPedidos.Include(i => i.Pedido).Include(i => i.Produto);
            return View(itenspedidos.Where(i => i.pedidoID == id).ToList());
        }

        //
        // GET: /ItensPedido/Details/5

        public ActionResult Details(int id)
        {
            ItensPedido itenspedido = db.ItensPedidos.Find(id);
            return View(itenspedido);
        }

        //
        // GET: /ItensPedido/Create

        public ActionResult Create(int id)
        {
            ViewData["IdPedido"] = id;
            ViewBag.pedidoID = new SelectList(db.Pedidos, "id", "id", id);
            ViewBag.produtoID = new SelectList(db.Produtos, "id", "descricao");

            return View();
        }

        //
        // POST: /ItensPedido/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ItensPedido itenspedido)
        {
            if (ModelState.IsValid)
            {
                db.ItensPedidos.Add(itenspedido);
                db.SaveChanges();
                return RedirectToAction("Index" + "/" + itenspedido.pedidoID);
            }

            ViewBag.pedidoID = new SelectList(db.Pedidos, "id", "id", itenspedido.pedidoID);
            ViewBag.produtoID = new SelectList(db.Produtos, "id", "descricao", itenspedido.produtoID);

            return View(itenspedido);
        }

        //
        // GET: /ItensPedido/Edit/5

        public ActionResult Edit(int id)
        {
            ItensPedido itenspedido = db.ItensPedidos.Find(id);

            ViewBag.pedidoID = new SelectList(db.Pedidos, "id", "id", itenspedido.pedidoID);
            ViewBag.produtoID = new SelectList(db.Produtos, "id", "descricao", itenspedido.produtoID);
            
            return View(itenspedido);
        }

        //
        // POST: /ItensPedido/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ItensPedido itenspedido)
        {
            if (ModelState.IsValid)
            {
                db.Entry(itenspedido).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index" + "/" + itenspedido.pedidoID);
            }
            
            ViewBag.pedidoID = new SelectList(db.Pedidos, "id", "id", itenspedido.pedidoID);
            ViewBag.produtoID = new SelectList(db.Produtos, "id", "descricao", itenspedido.produtoID);
            
            return View(itenspedido);
        }

        //
        // GET: /ItensPedido/Delete/5

        public ActionResult Delete(int id)
        {
            ItensPedido itenspedido = db.ItensPedidos.Find(id);
            return View(itenspedido);
        }

        //
        // POST: /ItensPedido/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ItensPedido itenspedido = db.ItensPedidos.Find(id);
            db.ItensPedidos.Remove(itenspedido);
            db.SaveChanges();
            return RedirectToAction("Index" + "/" + itenspedido.pedidoID);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }

        [HttpGet]
        public ActionResult CarregaDadosProduto(int id)
        {
            Produto produto = db.Produtos.Find(id);
            return Json(produto, JsonRequestBehavior.AllowGet);
        }
    }
}