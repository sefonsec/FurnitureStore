using System;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Web.Mvc;
using FurnitureStore.Models;

namespace FurnitureStore.Controllers
{
    [Authorize]
    public class ProdutoController : Controller
    {
        private DataContext db = new DataContext();

        //
        // GET: /Produto/
        public ActionResult Index()
        {
            return View(db.Produtos.ToList());
        }

        [HttpPost, ActionName("Index")]
        [Submit("Buscar")]
        public ActionResult Index_Buscar(string buscarDescricao)
        {          
            var produto = from p in db.Produtos select p;

            if (!string.IsNullOrEmpty(buscarDescricao))
            {
                produto = produto.Where(p => p.descricao.Contains(buscarDescricao));
            }

            return View(produto.ToList());            
        }

        //
        // GET: /Produto/Details/5

        public ActionResult Details(int id)
        {
            Produto produto = db.Produtos.Find(id);
            return View(produto);
        }

        //
        // GET: /Produto/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Produto/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Produto produto)
        {
            if (ModelState.IsValid)
            {
                int idTipoUnidade = 0;
                string descricaoTipoUnidade = string.Empty;

                switch (produto.tipoUnidade.ToString())
                {
                    case "Peca":
                        idTipoUnidade = 1;
                        descricaoTipoUnidade = "Peça";
                        break;

                    case "Kilo":
                        idTipoUnidade = 2;
                        descricaoTipoUnidade = "Kilo";
                        break;

                    case "Metro":
                        idTipoUnidade = 3;
                        descricaoTipoUnidade = "Metro";
                        break;

                    case "Litro":
                        idTipoUnidade = 4;
                        descricaoTipoUnidade = "Litro";
                        break;
                }

                produto.idTipoUnidade = idTipoUnidade;
                produto.descricaoTipoUnidade = descricaoTipoUnidade;

                db.Produtos.Add(produto);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(produto);
        }

        //
        // GET: /Produto/Edit/5

        public ActionResult Edit(int id)
        {
            Produto produto = db.Produtos.Find(id);

            switch (produto.idTipoUnidade)
            {
                case 1:
                    produto.tipoUnidade = TipoUnidade.Peca;
                    break;

                case 2:
                    produto.tipoUnidade = TipoUnidade.Kilo;
                    break;

                case 3:
                    produto.tipoUnidade = TipoUnidade.Metro;
                    break;

                case 4:
                    produto.tipoUnidade = TipoUnidade.Litro;
                    break;
            }

            return View(produto);
        }

        //
        // POST: /Produto/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Produto produto)
        {
            if (ModelState.IsValid)
            {
                int idTipoUnidade = 0;
                string descricaoTipoUnidade = string.Empty;

                switch (produto.tipoUnidade.ToString())
                {
                    case "Peca":
                        idTipoUnidade = 1;
                        descricaoTipoUnidade = "Peça";
                        break;

                    case "Kilo":
                        idTipoUnidade = 2;
                        descricaoTipoUnidade = "Kilo";
                        break;

                    case "Metro":
                        idTipoUnidade = 3;
                        descricaoTipoUnidade = "Metro";
                        break;

                    case "Litro":
                        idTipoUnidade = 4;
                        descricaoTipoUnidade = "Litro";
                        break;
                }

                produto.idTipoUnidade = idTipoUnidade;
                produto.descricaoTipoUnidade = descricaoTipoUnidade;

                db.Entry(produto).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(produto);
        }

        //
        // GET: /Produto/Delete/5

        public ActionResult Delete(int id)
        {
            Produto produto = db.Produtos.Find(id);  
            return View(produto);
        }

        //
        // POST: /Produto/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Produto produto = db.Produtos.Find(id);
            db.Produtos.Remove(produto);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }

        public enum SubmitRequirement
        {
            Equal,
            StartsWith
        }

        public class SubmitAttribute : ActionMethodSelectorAttribute
        {
            private readonly string _submitButtonName;
            private readonly SubmitRequirement _requirement;

            public SubmitAttribute(string submitButtonName) :
                this(SubmitRequirement.Equal, submitButtonName)
            {
            }

            public SubmitAttribute(SubmitRequirement requirement, string _submitButtonName)
            {
                this._submitButtonName = _submitButtonName;
                this._requirement = requirement;
            }

            public override bool IsValidForRequest(ControllerContext controllerContext, MethodInfo methodInfo)
            {
                string buttonName = _submitButtonName;

                try
                {
                    string value = "";
                    switch (this._requirement)
                    {
                        case SubmitRequirement.Equal:
                            {
                                value = controllerContext.HttpContext.Request.Form[buttonName];
                            }
                            break;
                        case SubmitRequirement.StartsWith:
                            {
                                foreach (var formValue in controllerContext.HttpContext.Request.Form.AllKeys)
                                {
                                    if (formValue.StartsWith(buttonName, StringComparison.InvariantCultureIgnoreCase))
                                    {
                                        value = controllerContext.HttpContext.Request.Form[formValue];
                                        break;
                                    }
                                }
                            }
                            break;
                    }
                    if (!String.IsNullOrEmpty(value))
                        return true;
                }
                catch (Exception exc)
                {
                    Debug.WriteLine(exc.Message);
                }

                return false;
            }
        }
    }
}