using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Cocina_Tech.Models;
using Cocina_Tech.BO;

namespace Cocina_Tech.Controllers
{
    public class EstadoPedidoController : Controller
    {
        // GET: EstadoPedido
        ConexionModel ObjConexion = new ConexionModel();
        EstadoPedidoModel ObjModel = new EstadoPedidoModel();

        public ActionResult EstadoPedido()
        {
            return View(ObjModel.VerTabla());
        }

        public ActionResult Agregar(string Nombre)
        {
            EstadoPedidoBO ObjBO = new EstadoPedidoBO();
            ObjBO.Nombre = Nombre;
            ObjBO.Estado = true;
            ObjModel.AgregarEstado(ObjBO);
            return RedirectToAction("EstadoPedido", "EstadoPedido");
        }

        public ActionResult Eliminar(string IDEstado)
        {
            EstadoPedidoBO ObjBO = new EstadoPedidoBO();
            ObjBO.IDEstado = int.Parse(IDEstado);
            ObjModel.Eliminar(ObjBO);
            return RedirectToAction("EstadoPedido", "EstadoPedido");
        }
    }
}