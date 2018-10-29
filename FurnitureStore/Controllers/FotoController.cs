using System;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using FurnitureStore.Models;

namespace FurnitureStore.Controllers
{
    [Authorize]
    public class FotoController : Controller
    {
        private DataContext db = new DataContext(); 
        //
        // GET: /Fotos/

        [HttpGet]
        public ActionResult Index(int id)
        {
            ViewData["IdProduto"] = id;
            var foto = db.Fotos.Include(i => i.Produto);          
            return View(foto.Where(i => i.produtoID == id).ToList());
        }

        public ActionResult Delete(int id)
        {
            Foto foto = db.Fotos.Find(id);
            return View(foto);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Foto foto = db.Fotos.Find(id);
            db.Fotos.Remove(foto);
            db.SaveChanges();
            return RedirectToAction("Index" + "/" + foto.produtoID);
        }

        [HttpPost, ActionName("Index")]
        [Submit("btnSalvarArquivo")]
        public ActionResult btnSalvarArquivo(Produto produto)
        {            
            var file1 = Request.Files["file1"];

            if (!string.IsNullOrEmpty(file1.FileName))
            {
                var foto = new Foto();

                Byte[] binaryData = new Byte[file1.InputStream.Length];
                file1.InputStream.Read(binaryData, 0, (int)file1.InputStream.Length);

                ViewData["IdProduto"] = produto.id;

                foto.produtoID = produto.id;
                foto.nome = file1.FileName.Substring(file1.FileName.LastIndexOf("\\") + 1);
                foto.conteudo = binaryData;
                foto.tamanho = file1.ContentLength;
                foto.tipo = file1.ContentType;

                db.Fotos.Add(foto);
                db.SaveChanges();
            }

            return View("Index", db.Fotos.Where(i => i.produtoID == produto.id).ToList());
        }

        public FileContentResult Display(int id)
        {
            Foto foto = db.Fotos.Find(id);
            if (foto != null)
            {
                Byte[] _bytes = foto.conteudo;                                
                return  new FileContentResult(_bytes, foto.tipo);
            }
            return null;
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

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}
