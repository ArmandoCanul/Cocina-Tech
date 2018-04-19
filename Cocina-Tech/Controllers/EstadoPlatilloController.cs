using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Cocina_Tech.Models;
using System.Data.SqlClient;
using System.Data;
using Cocina_Tech.BO;

namespace Cocina_Tech.Controllers
{
    public class EstadoPlatilloController : Controller
    {
        // GET: EstadoPlatillo
        ConexionModel ObjConexion = new ConexionModel();

        public ActionResult EstadoPlatillo()
        {
            return View("C:\\Users\francisco2\\Desktop\\Jorge\\Cocina-Tech\\Cocina-Tech\\Views\\EstadoPlatillo.cshtml");
        }

        public ActionResult Agregar(string Nombre)
        {
            EstadoPedidoBO ObjBO = new EstadoPedidoBO();
            ObjBO.Nombre = Nombre;
            return RedirectToAction("EstadoPlatillo", "EstadoPlatillo");
        }
    }
}