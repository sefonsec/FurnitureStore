using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.UI;
using CrystalDecisions.CrystalReports.Engine;
using FurnitureStore.Models;
using FurnitureStore.Models.DTO;
using cs = System.Configuration.ConfigurationManager;

namespace FurnitureStore.Controllers
{
    [Authorize]
    public class PedidoController : Controller
    {
        private DataContext db = new DataContext();

        //
        // GET: /Pedido/
        public ActionResult Index()
        {
            ViewBag.vendedor = (from v in db.Vendedores select v.nome).Distinct();
            ViewBag.cliente = (from c in db.Clientes select c.nome).Distinct();

            var pedidos = db.Pedidos.Include(p => p.Vendedor).Include(p => p.Cliente).Include(p => p.Pagamento).Include(p => p.Parcela);
            return View(pedidos.ToList());
        }

        [HttpPost, ActionName("Index")]
        [Submit("Buscar")]
        public ActionResult Index_Buscar(string vendedor, string cliente)
        {
            ViewBag.vendedor = (from v in db.Vendedores select v.nome).Distinct();
            ViewBag.cliente = (from c in db.Clientes select c.nome).Distinct();

            var vendedores = from v in db.Vendedores where v.nome == vendedor select v;            
            var clientes = from c in db.Clientes where c.nome == cliente select c;                         

            var pedido = from p in db.Pedidos select p;

            if (!string.IsNullOrEmpty(vendedor))
            {
                int vendedorID = 0;

                foreach (var item in vendedores)
                {
                    vendedorID = item.id;
                }

                pedido = pedido.Where(p => p.vendedorID == vendedorID);
            }

            if (!string.IsNullOrEmpty(cliente))
            {
                int clienteID = 0;

                foreach (var item in clientes)
                {
                    clienteID = (int)item.Id;
                }

                pedido = pedido.Where(p => p.clienteID == clienteID);
            }

            return View(pedido.ToList());
        }

        //
        // GET: /Pedido/Details/5

        public ActionResult Details(int id)
        {
            Pedido pedido = db.Pedidos.Find(id);
            return View(pedido);
        }

        //
        // GET: /Pedido/Create

        public ActionResult Create()
        {
            ViewBag.vendedorID = new SelectList(db.Vendedores, "id", "nome");
            ViewBag.clienteID = new SelectList(db.Clientes, "id", "nome");
            ViewBag.pagamentoID = new SelectList(db.Pagamentos, "id", "formaPagamento");
            ViewBag.parcelaID = new SelectList(db.Parcelas, "id", "numeroParcela");

            return View();
        }

        //
        // POST: /Pedido/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Pedido pedido)
        {
            if (ModelState.IsValid)
            {
                int idTipoPedido = 0;
                string descricaoTipoPedido = string.Empty;

                switch (pedido.tipoPedido.ToString())
                {
                    case "Pedido":
                        idTipoPedido = 1;
                        descricaoTipoPedido = "Pedido";
                        break;

                    case "Orçamento":
                        idTipoPedido = 2;
                        descricaoTipoPedido = "Orçamento";
                        break;
                }

                pedido.idTipoPedido = idTipoPedido;
                pedido.descricaoTipoPedido = descricaoTipoPedido;

                db.Pedidos.Add(pedido);
                db.SaveChanges();

                return RedirectToAction("Index");
            }

            ViewBag.vendedorID = new SelectList(db.Vendedores, "id", "nome", pedido.vendedorID);
            ViewBag.clienteID = new SelectList(db.Clientes, "id", "nome", pedido.clienteID);
            ViewBag.pagamentoID = new SelectList(db.Pagamentos, "id", "formaPagamento", pedido.pagamentoID);
            ViewBag.parcelaID = new SelectList(db.Parcelas, "id", "numeroParcela", pedido.parcelaID);
            
            return View(pedido);
        }

        //
        // GET: /Pedido/Edit/5

        public ActionResult Edit(int id)
        {
            Pedido pedido = db.Pedidos.Find(id);

            switch (pedido.idTipoPedido)
            {
                case 1:
                    pedido.tipoPedido = TipoPedido.Pedido;
                    break;

                case 2:
                    pedido.tipoPedido = TipoPedido.Orcamento;
                    break;
            }

            ViewBag.vendedorID = new SelectList(db.Vendedores, "id", "nome", pedido.vendedorID);
            ViewBag.clienteID = new SelectList(db.Clientes, "id", "nome", pedido.clienteID);
            ViewBag.pagamentoID = new SelectList(db.Pagamentos, "id", "formaPagamento", pedido.pagamentoID);
            ViewBag.parcelaID = new SelectList(db.Parcelas, "id", "numeroParcela", pedido.parcelaID);

            return View(pedido);
        }

        //
        // POST: /Pedido/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Pedido pedido)
        {
            if (ModelState.IsValid)
            {
                int idTipoPedido = 0;
                string descricaoTipoPedido = string.Empty;

                switch (pedido.tipoPedido.ToString())
                {
                    case "Pedido":
                        idTipoPedido = 1;
                        descricaoTipoPedido = "Pedido";
                        break;

                    case "Orçamento":
                        idTipoPedido = 2;
                        descricaoTipoPedido = "Orçamento";
                        break;
                }

                pedido.idTipoPedido = idTipoPedido;
                pedido.descricaoTipoPedido = descricaoTipoPedido;

                db.Entry(pedido).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.vendedorID = new SelectList(db.Vendedores, "id", "nome", pedido.vendedorID);
            ViewBag.clienteID = new SelectList(db.Clientes, "id", "nome", pedido.clienteID);
            ViewBag.pagamentoID = new SelectList(db.Pagamentos, "id", "formaPagamento", pedido.pagamentoID);
            ViewBag.parcelaID = new SelectList(db.Parcelas, "id", "numeroParcela", pedido.parcelaID);

            return View(pedido);
        }

        //
        // GET: /Pedido/Delete/5

        public ActionResult Delete(int id)
        {
            Pedido pedido = db.Pedidos.Find(id);
            return View(pedido);
        }

        //
        // POST: /Pedido/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Pedido pedido = db.Pedidos.Find(id);
            db.Pedidos.Remove(pedido);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult ExportarRelatorio(int id)
        {                        
            var pedido = db.Pedidos.Include(p => p.Vendedor)
                    .Include(p => p.Cliente)
                    .Select(s => new PedidoDTO
                    {
                        id = s.id,
                        dataPedido = s.dataPedido,
                        nomeVendedor = s.Vendedor.nome,
                        nomeCliente = s.Cliente.nome,
                        rg = s.Cliente.rg,
                        cpf = s.Cliente.cpf,
                        dataNasc = s.Cliente.dataNasc,
                        endereco = s.Cliente.endereco,
                        numero = s.Cliente.numero,
                        bairro = s.Cliente.bairro,
                        cidade = s.Cliente.cidade,
                        uf = s.Cliente.uf,
                        cep = s.Cliente.cep,
                        banco = s.Cliente.nomeBanco,
                        agencia = s.Cliente.agencia,
                        contaCorrente = s.Cliente.contaCorrente,
                        clienteDesde = s.Cliente.dataClienteDesde
                    }).ToList();           

            var report = new ReportDocument();
            report.Load(Path.Combine(Server.MapPath("~/Reports"), "Pedido.rpt"));
            report.SetDataSource(pedido.Where(p => p.id == id));           

            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();

            try
            {
                Stream stream = report.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
                stream.Seek(0, SeekOrigin.Begin);
                return File(stream, "application/pdf", "DadosPedido.pdf");
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                throw;
            }                       
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
            private readonly ClienteController.SubmitRequirement _requirement;

            public SubmitAttribute(string submitButtonName) :
                this(ClienteController.SubmitRequirement.Equal, submitButtonName)
            {
            }

            public SubmitAttribute(ClienteController.SubmitRequirement requirement, string _submitButtonName)
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
                        case ClienteController.SubmitRequirement.Equal:
                            {
                                value = controllerContext.HttpContext.Request.Form[buttonName];
                            }
                            break;
                        case ClienteController.SubmitRequirement.StartsWith:
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