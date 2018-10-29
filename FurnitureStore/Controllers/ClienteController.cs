using System;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Web.Mvc;
using FurnitureStore.Models;

namespace FurnitureStore.Controllers
{
    public class ClienteController : Controller
    {
        private DataContext db = new DataContext();

        //
        // GET: /Cliente/

        [Authorize]
        public ActionResult Index()
        {
            return View(db.Clientes.ToList());
        }
    
        [HttpPost, ActionName("Index")]
        [Submit("Buscar")]
        public ActionResult Index_Buscar(string buscarNome, string buscarEndereco, string buscarNumero, string buscarBairro, string buscarCidade)
        {
            int numero = 0;
            
            if (!string.IsNullOrEmpty(buscarNumero))
                numero = int.Parse(buscarNumero);

            var cliente = from c in db.Clientes select c;

            if (!string.IsNullOrEmpty(buscarNome))
                cliente = cliente.Where(c => c.nome.Contains(buscarNome));

            if (!string.IsNullOrEmpty(buscarEndereco))
                cliente = cliente.Where(c => c.endereco.Contains(buscarEndereco));

            if (numero > 0)                
                cliente = cliente.Where(c => c.numero == numero);

            if (!string.IsNullOrEmpty(buscarBairro))
                cliente = cliente.Where(c => c.bairro.Contains(buscarBairro));

            if (!string.IsNullOrEmpty(buscarCidade))
                cliente = cliente.Where(c => c.cidade.Contains(buscarCidade));

            return View(cliente.ToList());
        }

        //
        // GET: /Cliente/Details/5

        public ActionResult Details(int id)
        {
            Cliente cliente = db.Clientes.Find(id); 
            return View(cliente);
        }

        //
        // GET: /Cliente/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Cliente/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Cliente cliente)
        {
            if (ModelState.IsValid)
            {
                db.Clientes.Add(cliente);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(cliente);
        }

        //
        // GET: /Cliente/Edit/5

        public ActionResult Edit(int id)
        {
            Cliente cliente = db.Clientes.Find(id);
            return View(cliente);
        }

        //
        // POST: /Cliente/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Cliente cliente)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cliente).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(cliente);
        }

        //
        // GET: /Cliente/Delete/5

        public ActionResult Delete(int id)
        {
            Cliente cliente = db.Clientes.Find(id);
            return View(cliente);
        }

        //
        // POST: /Cliente/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Cliente cliente = db.Clientes.Find(id);
            db.Clientes.Remove(cliente);
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