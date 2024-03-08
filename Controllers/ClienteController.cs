using progetto_settimanaleS18L5.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;

namespace progetto_settimanaleS18L5.Controllers
{
    public class ClienteController : Controller
    {
        public ActionResult CreaCliente()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreaCliente(Cliente model)
        {
            if (ModelState.IsValid)
            {
                model.InserisciCliente();
                return RedirectToAction("ListaCliente");
            }
            return View(model);
        }

        public ActionResult DettagliCliente(int IdCliente)
        {
            var cliente = new Cliente().GetClienteByIdCliente(IdCliente);

            if (cliente != null)
            {
                return View(cliente);
            }

            return HttpNotFound();
        }

        [HttpGet]
        public ActionResult ListaCliente()
        {
            var cliente = new Cliente().ListaClienti();
            return View(cliente);
        }

        [HttpGet]
        public ActionResult ModificaCliente(int IdCliente)
        {
            Cliente clienteDaModificare = new Cliente().GetClienteByIdCliente(IdCliente);

            if (clienteDaModificare == null)
            {
                return View("ListaCliente");
            }

            return View(clienteDaModificare);
        }

        [HttpPost]
        public ActionResult ModificaCliente(Cliente model)
        {
            if (ModelState.IsValid)
            {
                model.ModificaCliente();
                return RedirectToAction("ListaCliente");
            }
            return View(model);
        }

        public ActionResult EliminaCliente(int IdCliente)
        {
            var cliente = new Cliente().GetClienteByIdCliente(IdCliente);
            if (cliente != null)
            {
                cliente.EliminaCliente();
            }
            return RedirectToAction("ListaCliente");
        }
    }
}